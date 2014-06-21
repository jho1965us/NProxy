//
// Copyright © Martin Tamme
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Reflection.Emit;

namespace NProxy.Core
{
    /// <summary>
    /// Represents an invocation type factory.
    /// </summary>
    internal sealed class InvocationTypeFactory : IInvocationTypeFactory
    {
        /// <summary>
        /// The <see cref="RuntimeStaticPart"/> constructor information.
        /// </summary>
        private static readonly ConstructorInfo RuntimeStaticPartConstructorInfo = typeof (RuntimeStaticPart).GetConstructor(
            BindingFlags.Public | BindingFlags.Instance,
            typeof (string), typeof (MethodKind), typeof (RuntimeTypeHandle), typeof (RuntimeMethodHandle));

        /// <summary>
        /// The <see cref="InvocationBase"/> constructor information.
        /// </summary>
        private static readonly ConstructorInfo InvocationBaseConstructorInfo = typeof (InvocationBase).GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            typeof (IStaticPart), typeof (bool), typeof (object), typeof (object[]));

        /// <summary>
        /// The <see cref="InvocationBase.InvokeBase(object,object[])"/> method information.
        /// </summary>
        private static readonly MethodInfo InvocationBaseInvokeBaseMethodInfo = typeof (InvocationBase).GetMethod(
            "InvokeBase",
            BindingFlags.NonPublic | BindingFlags.Instance,
            typeof (object), typeof (object[]));

        /// <summary>
        /// The <see cref="InvocationBase.InvokeVirtual(object,object[])"/> method information.
        /// </summary>
        private static readonly MethodInfo InvocationBaseInvokeVirtualMethodInfo = typeof (InvocationBase).GetMethod(
            "InvokeVirtual",
            BindingFlags.NonPublic | BindingFlags.Instance,
            typeof (object), typeof (object[]));

        /// <summary>
        /// The type repository.
        /// </summary>
        private readonly IInvocationTypeRepository _typeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationTypeFactory"/> class.
        /// </summary>
        /// <param name="typeRepository">The type repository.</param>
        public InvocationTypeFactory(IInvocationTypeRepository typeRepository)
        {
            if (typeRepository == null)
                throw new ArgumentNullException("typeRepository");

            _typeRepository = typeRepository;
        }

        /// <summary>
        /// Builds the type initializer.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="staticPart">The static part.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <param name="staticPartFieldInfo">The static part field information.</param>
        private static void BuildTypeInitializer(TypeBuilder typeBuilder,
            IStaticPart staticPart,
            Type[] genericParameterTypes,
            FieldInfo staticPartFieldInfo)
        {
            // Define type initializer.
            var typeInitializer = typeBuilder.DefineConstructor(
                MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.SpecialName,
                CallingConventions.Standard,
                null);

            // Implement type initializer.
            var ilGenerator = typeInitializer.GetILGenerator();

            // Create and load the static part.
            var targetMethodInfo = staticPart.Method.MapGenericMethod(genericParameterTypes);
            var declaringType = targetMethodInfo.DeclaringType;

            ilGenerator.Emit(OpCodes.Ldstr, staticPart.DeclaringName);
            ilGenerator.EmitLoadValue((int) staticPart.MethodKind);
            ilGenerator.Emit(OpCodes.Ldtoken, declaringType);
            ilGenerator.Emit(OpCodes.Ldtoken, targetMethodInfo);
            ilGenerator.Emit(OpCodes.Newobj, RuntimeStaticPartConstructorInfo);

            // Store static part.
            ilGenerator.Emit(OpCodes.Stsfld, staticPartFieldInfo);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Builds the constructor.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="staticPartFieldInfo">The static part field information.</param>
        /// <returns>The constructor information.</returns>
        private static void BuildConstructor(TypeBuilder typeBuilder, FieldInfo staticPartFieldInfo)
        {
            // Define constructor.
            var constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                InvocationBaseConstructorInfo.CallingConvention,
                new[] {typeof (bool), typeof (object), typeof (object[])},
                new[] {"isOverride", "target", "parameters"});

            // Implement constructor.
            var ilGenerator = constructorBuilder.GetILGenerator();

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load static part.
            ilGenerator.Emit(OpCodes.Ldsfld, staticPartFieldInfo);

            // Load override flag.
            ilGenerator.Emit(OpCodes.Ldarg_1);

            // Load target object.
            ilGenerator.Emit(OpCodes.Ldarg_2);

            // Load parameters.
            ilGenerator.Emit(OpCodes.Ldarg_3);

            // Call parent constructor.
            ilGenerator.Emit(OpCodes.Call, InvocationBaseConstructorInfo);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Builds the invoke method.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <param name="isVirtual">A value indicating whether the method should be called virtually.</param>
        private static void BuildInvokeMethod(TypeBuilder typeBuilder, MethodInfo methodInfo, Type[] genericParameterTypes, bool isVirtual)
        {
            var invokeMethodInfo = isVirtual ? InvocationBaseInvokeVirtualMethodInfo : InvocationBaseInvokeBaseMethodInfo;

            // Define method.
            var methodBuilder = typeBuilder.DefineMethod(invokeMethodInfo, false, true);

            methodBuilder.DefineParameters(invokeMethodInfo);

            // Implement method.
            var ilGenerator = methodBuilder.GetILGenerator();

            // Load target object.
            ilGenerator.Emit(OpCodes.Ldarg_1);

            // Load parameter values.
            var parameterTypes = methodInfo.MapGenericParameterTypes(genericParameterTypes);
            var parameterValueLocalBuilders = new LocalBuilder[parameterTypes.Length];

            LoadParameterValues(ilGenerator, 2, parameterTypes, parameterValueLocalBuilders);

            // Call target method.
            var targetMethodInfo = methodInfo.MapGenericMethod(genericParameterTypes);

            ilGenerator.Emit(isVirtual ? OpCodes.Callvirt : OpCodes.Call, targetMethodInfo);

            // Restore by reference parameter values.
            RestoreByReferenceParameterValues(ilGenerator, 2, parameterTypes, parameterValueLocalBuilders);

            // Handle return value.
            var returnType = methodInfo.MapGenericReturnType(genericParameterTypes);

            if (returnType.IsVoid())
                ilGenerator.Emit(OpCodes.Ldnull);
            else
                ilGenerator.EmitBox(returnType);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Loads the parameter values onto the stack.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="argumentIndex">The argument index.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parameterValueLocalBuilders">The parameter value local builders.</param>
        private static void LoadParameterValues(ILGenerator ilGenerator, int argumentIndex, IList<Type> parameterTypes, IList<LocalBuilder> parameterValueLocalBuilders)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                ilGenerator.EmitLoadArgument(argumentIndex);
                ilGenerator.EmitLoadValue(index);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                if (parameterType.IsByRef)
                {
                    var elementType = parameterType.GetElementType();
                    var parameterLocalBuilder = ilGenerator.DeclareLocal(elementType);

                    ilGenerator.EmitUnbox(elementType);
                    ilGenerator.Emit(OpCodes.Stloc, parameterLocalBuilder);
                    ilGenerator.Emit(OpCodes.Ldloca, parameterLocalBuilder);

                    parameterValueLocalBuilders[index] = parameterLocalBuilder;
                }
                else
                {
                    ilGenerator.EmitUnbox(parameterType);
                }
            }
        }

        /// <summary>
        /// Restores the by reference parameter values.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="argumentIndex">The argument index.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parameterValueLocalBuilders">The parameter value local builders.</param>
        private static void RestoreByReferenceParameterValues(ILGenerator ilGenerator, int argumentIndex, IList<Type> parameterTypes, IList<LocalBuilder> parameterValueLocalBuilders)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                if (!parameterType.IsByRef)
                    continue;

                var parameterLocalBuilder = parameterValueLocalBuilders[index];
                var elementType = parameterType.GetElementType();

                ilGenerator.EmitLoadArgument(argumentIndex);
                ilGenerator.EmitLoadValue(index);
                ilGenerator.Emit(OpCodes.Ldloc, parameterLocalBuilder);
                ilGenerator.EmitBox(elementType);
                ilGenerator.Emit(OpCodes.Stelem_Ref);
            }
        }

        #region IInvocationTypeFactory Members

        /// <inheritdoc/>
        public Type CreateType(IStaticPart staticPart)
        {
            if (staticPart == null)
                throw new ArgumentNullException("staticPart");

            // Define type.
            var typeBuilder = _typeRepository.DefineType("Invocation", typeof (InvocationBase));

            // Define generic parameters.
            var genericParameterTypes = typeBuilder.DefineGenericParameters(staticPart.Method.GetGenericArguments());

            // Define static part static field.
            var staticFieldInfo = typeBuilder.DefineField(
                "StaticPart",
                typeof (IStaticPart),
                FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly);

            // Build type initializer.
            BuildTypeInitializer(typeBuilder, staticPart, genericParameterTypes, staticFieldInfo);

            // Build constructor.
            BuildConstructor(typeBuilder, staticFieldInfo);

            // Build base invoke method only for non abstract and overrideable methods.
            if (!staticPart.Method.IsAbstract && staticPart.Method.CanOverride())
                BuildInvokeMethod(typeBuilder, staticPart.Method, genericParameterTypes, false);

            // Build virtual invoke method.
            BuildInvokeMethod(typeBuilder, staticPart.Method, genericParameterTypes, true);

            return typeBuilder.CreateType();
        }

        #endregion
    }
}