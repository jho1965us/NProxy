//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using NProxy.Core.Intercept;
using NProxy.Core.Internal;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Reflection.Emit;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy type builder.
    /// </summary>
    internal sealed class ProxyTypeBuilder : ITypeBuilder
    {
        /// <summary>
        /// The <c>IEventInterceptor.Add</c> method information.
        /// </summary>
        private static readonly MethodInfo EventInterceptorAddMethodInfo = typeof (IEventInterceptor).GetMethod(
            "Add",
            BindingFlags.Public | BindingFlags.Instance,
            typeof (IEventInvocation));

        /// <summary>
        /// The <c>IEventInterceptor.Remove</c> method information.
        /// </summary>
        private static readonly MethodInfo EventInterceptorRemoveMethodInfo = typeof (IEventInterceptor).GetMethod(
            "Remove",
            BindingFlags.Public | BindingFlags.Instance,
            typeof (IEventInvocation));

        /// <summary>
        /// The <c>IEventInterceptor.Raise</c> method information.
        /// </summary>
        private static readonly MethodInfo EventInterceptorRaiseMethodInfo = typeof (IEventInterceptor).GetMethod(
            "Raise",
            BindingFlags.Public | BindingFlags.Instance,
            typeof (IEventInvocation));

        /// <summary>
        /// The <c>IEventInterceptor.Other</c> method information.
        /// </summary>
        private static readonly MethodInfo EventInterceptorOtherMethodInfo = typeof (IEventInterceptor).GetMethod(
            "Other",
            BindingFlags.Public | BindingFlags.Instance,
            typeof (IEventInvocation));

        /// <summary>
        /// The <c>IPropertyInterceptor.Get</c> method information.
        /// </summary>
        private static readonly MethodInfo PropertyInterceptorGetMethodInfo = typeof (IPropertyInterceptor).GetMethod(
            "Get",
            BindingFlags.Public | BindingFlags.Instance,
            typeof (IPropertyInvocation));

        /// <summary>
        /// The <c>IPropertyInterceptor.Set</c> method information.
        /// </summary>
        private static readonly MethodInfo PropertyInterceptorSetMethodInfo = typeof (IPropertyInterceptor).GetMethod(
            "Set",
            BindingFlags.Public | BindingFlags.Instance,
            typeof (IPropertyInvocation));

        /// <summary>
        /// The <c>IMethodInterceptor.Invoke</c> method information.
        /// </summary>
        private static readonly MethodInfo MethodInterceptorInvokeMethodInfo = typeof (IMethodInterceptor).GetMethod(
            "Invoke",
            BindingFlags.Public | BindingFlags.Instance,
            typeof (IMethodInvocation));

        /// <summary>
        /// The invocation type repository.
        /// </summary>
        private readonly IInvocationTypeRepository _invocationTypeRepository;

        /// <summary>
        /// The parent type.
        /// </summary>
        private readonly Type _parentType;

        /// <summary>
        /// The type builder.
        /// </summary>
        private readonly TypeBuilder _typeBuilder;

        /// <summary>
        /// The interceptor field information.
        /// </summary>
        private readonly FieldInfo _interceptorFieldInfo;

        /// <summary>
        /// The interface types.
        /// </summary>
        private readonly HashSet<Type> _interfaceTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTypeBuilder"/> class.
        /// </summary>
        /// <param name="invocationTypeRepository">The invocation type repository.</param>
        /// <param name="parentType">The parent type.</param>
        public ProxyTypeBuilder(IInvocationTypeRepository invocationTypeRepository, Type parentType)
        {
            if (invocationTypeRepository == null)
                throw new ArgumentNullException("invocationTypeRepository");

            if (parentType == null)
                throw new ArgumentNullException("parentType");

            if (parentType.IsSealed)
                throw new ArgumentException(Resources.ParentTypeMustNotBeSealed, "parentType");

            if (parentType.IsGenericTypeDefinition)
                throw new ArgumentException(Resources.ParentTypeMustNotBeAGenericTypeDefinition, "parentType");

            _invocationTypeRepository = invocationTypeRepository;
            _parentType = parentType;

            _typeBuilder = invocationTypeRepository.DefineType("Proxy", parentType);

            _interceptorFieldInfo = _typeBuilder.DefineField(
                "_interceptor",
                typeof (IMemberInterceptor),
                FieldAttributes.Private | FieldAttributes.InitOnly);

            _interfaceTypes = new HashSet<Type>();
        }

        /// <summary>
        /// Defines an event based on the specified event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="isExplicit">A value indicating whether the specified event should be implemented explicitly.</param>
        /// <returns>The event builder.</returns>
        private void BuildInterceptedEvent(EventInfo eventInfo, bool isExplicit)
        {
            // Define event.
            var eventName = isExplicit ? eventInfo.GetFullName() : eventInfo.Name;

            var eventBuilder = _typeBuilder.DefineEvent(
                eventName,
                eventInfo.Attributes,
                eventInfo.EventHandlerType);

            // Build event add method.
            var addMethodInfo = eventInfo.GetAddMethod(true);

            if (addMethodInfo != null)
            {
                var invocationType = _invocationTypeRepository.GetType(eventInfo, addMethodInfo);
                var addMethodBuilder = BuildInterceptedMethod(addMethodInfo, isExplicit, invocationType, EventInterceptorAddMethodInfo);

                eventBuilder.SetAddOnMethod(addMethodBuilder);
            }

            // Build event remove method.
            var removeMethodInfo = eventInfo.GetRemoveMethod(true);

            if (removeMethodInfo != null)
            {
                var invocationType = _invocationTypeRepository.GetType(eventInfo, removeMethodInfo);
                var removeMethodBuilder = BuildInterceptedMethod(removeMethodInfo, isExplicit, invocationType, EventInterceptorRemoveMethodInfo);

                eventBuilder.SetRemoveOnMethod(removeMethodBuilder);
            }

            // Build event raise method.
            var raiseMethodInfo = eventInfo.GetRaiseMethod(true);

            if (raiseMethodInfo != null)
            {
                var invocationType = _invocationTypeRepository.GetType(eventInfo, raiseMethodInfo);
                var methodBuilder = BuildInterceptedMethod(raiseMethodInfo, isExplicit, invocationType, EventInterceptorRaiseMethodInfo);

                eventBuilder.SetRaiseMethod(methodBuilder);
            }

            // Build event other methods.
            var otherMethodInfos = eventInfo.GetOtherMethods(true);

            if (otherMethodInfos != null)
            {
                foreach (var otherMethodInfo in otherMethodInfos)
                {
                    var invocationType = _invocationTypeRepository.GetType(eventInfo, otherMethodInfo);
                    var methodBuilder = BuildInterceptedMethod(raiseMethodInfo, isExplicit, invocationType, EventInterceptorOtherMethodInfo);

                    eventBuilder.AddOtherMethod(methodBuilder);
                }
            }
        }

        /// <summary>
        /// Defines a property based on the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="isExplicit">A value indicating whether the specified property should be implemented explicitly.</param>
        /// <returns>The property builder.</returns>
        private void BuildInterceptedProperty(PropertyInfo propertyInfo, bool isExplicit)
        {
            // Define property.
            var propertyName = isExplicit ? propertyInfo.GetFullName() : propertyInfo.Name;
            var parameterTypes = propertyInfo.GetIndexParameterTypes();

            var propertyBuilder = _typeBuilder.DefineProperty(
                propertyName,
                propertyInfo.Attributes,
                CallingConventions.HasThis,
                propertyInfo.PropertyType,
                null,
                null,
                parameterTypes,
                null,
                null);

            // Build property get method.
            var getMethodInfo = propertyInfo.GetGetMethod(true);

            if (getMethodInfo != null)
            {
                var invocationType = _invocationTypeRepository.GetType(propertyInfo, getMethodInfo);
                var methodBuilder = BuildInterceptedMethod(getMethodInfo, isExplicit, invocationType, PropertyInterceptorGetMethodInfo);

                propertyBuilder.SetGetMethod(methodBuilder);
            }

            // Build property set method.
            var setMethodInfo = propertyInfo.GetSetMethod(true);

            if (setMethodInfo != null)
            {
                var invocationType = _invocationTypeRepository.GetType(propertyInfo, setMethodInfo);
                var methodBuilder = BuildInterceptedMethod(setMethodInfo, isExplicit, invocationType, PropertyInterceptorSetMethodInfo);

                propertyBuilder.SetSetMethod(methodBuilder);
            }
        }

        /// <summary>
        /// Builds an intercepted method based on the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="isExplicit">A value indicating whether the specified method should be implemented explicitly.</param>
        /// <param name="invocationType">The invocation type.</param>
        /// <param name="interceptorMethodInfo">The interceptor method information.</param>
        /// <returns>The intercepted method builder.</returns>
        private MethodBuilder BuildInterceptedMethod(MethodInfo methodInfo, bool isExplicit, Type invocationType, MethodInfo interceptorMethodInfo)
        {
            var isOverride = IsOverrideMember(methodInfo);

            if (isOverride && !methodInfo.CanOverride())
                throw new InvalidOperationException(String.Format(Resources.MethodNotOverridable, methodInfo.Name));

            // Define method.
            var methodBuilder = _typeBuilder.DefineMethod(methodInfo, isExplicit, isOverride);

            // Define generic parameters.
            var genericParameterTypes = methodBuilder.DefineGenericParameters(methodInfo);

            // Define parameters.
            methodBuilder.DefineParameters(methodInfo, genericParameterTypes);

            // Only define method override if method is implemented explicitly and is an override.
            if (isExplicit && isOverride)
                _typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);

            // Implement method.
            var ilGenerator = methodBuilder.GetILGenerator();

            // Load arguments.
            var parameterTypes = methodInfo.MapGenericParameterTypes(genericParameterTypes);
            var parametersLocalBuilder = ilGenerator.NewArray(typeof (object), parameterTypes.Length);

            LoadArguments(ilGenerator, parameterTypes, parametersLocalBuilder);

            // Load interceptor.
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, _interceptorFieldInfo);

            // Get invocation constructor.
            var invocationConstructorInfo = GetInvocationConstructor(invocationType, genericParameterTypes);

            // Create and load invocation.
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(isOverride ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);
            ilGenerator.Emit(OpCodes.Newobj, invocationConstructorInfo);

            // Call invocation handler method.
            ilGenerator.EmitCall(interceptorMethodInfo);

            // Restore by reference arguments.
            RestoreByReferenceArguments(ilGenerator, parameterTypes, parametersLocalBuilder);

            // Handle return value.
            var returnType = methodInfo.MapGenericReturnType(genericParameterTypes);

            if (returnType.IsVoid())
                ilGenerator.Emit(OpCodes.Pop);
            else
                ilGenerator.EmitUnbox(returnType);

            ilGenerator.Emit(OpCodes.Ret);

            return methodBuilder;
        }

        /// <summary>
        /// Returns an invocation constructor information for the specified method.
        /// </summary>
        /// <param name="invocationType">The invocation type.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <returns>The invocation constructor information.</returns>
        private ConstructorInfo GetInvocationConstructor(Type invocationType, Type[] genericParameterTypes)
        {
            var constructorInfo = invocationType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, typeof (object), typeof (bool), typeof (object[]));

            if (!invocationType.IsGenericTypeDefinition)
                return constructorInfo;

            var genericType = invocationType.MakeGenericType(genericParameterTypes);

            return TypeBuilder.GetConstructor(genericType, constructorInfo);
        }

        /// <summary>
        /// Loads the arguments onto the stack.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parametersLocalBuilder">The parameters local builder.</param>
        private static void LoadArguments(ILGenerator ilGenerator, IList<Type> parameterTypes, LocalBuilder parametersLocalBuilder)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);
                ilGenerator.EmitLoadValue(index);
                ilGenerator.EmitLoadArgument(index + 1);

                if (parameterType.IsByRef)
                {
                    var elementType = parameterType.GetElementType();

                    ilGenerator.EmitLoadIndirect(parameterType);
                    ilGenerator.EmitBox(elementType);
                }
                else
                {
                    ilGenerator.EmitBox(parameterType);
                }

                ilGenerator.Emit(OpCodes.Stelem_Ref);
            }
        }

        /// <summary>
        /// Restores the by reference arguments.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parametersLocalBuilder">The parameters local builder.</param>
        private static void RestoreByReferenceArguments(ILGenerator ilGenerator, IList<Type> parameterTypes, LocalBuilder parametersLocalBuilder)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var argumentType = parameterTypes[index];

                if (!argumentType.IsByRef)
                    continue;

                var elementType = argumentType.GetElementType();

                ilGenerator.EmitLoadArgument(index + 1);
                ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);
                ilGenerator.EmitLoadValue(index);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                ilGenerator.EmitUnbox(elementType);
                ilGenerator.EmitStoreIndirect(argumentType);
            }
        }

        /// <summary>
        /// Returns a value indicating whether the specified member should be overridden.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>A value indicating whether the specified member should be overridden.</returns>
        private bool IsOverrideMember(MemberInfo memberInfo)
        {
            var declaringType = memberInfo.DeclaringType;

            if (declaringType.IsInterface)
                return _interfaceTypes.Contains(declaringType);

            return declaringType.IsAssignableFrom(_parentType);
        }

        /// <summary>
        /// Returns a value indicating whether the specified member should be implemented explicitly.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>A value indicating whether the specified member should be implemented explicitly.</returns>
        private bool IsExplicitMember(MemberInfo memberInfo)
        {
            var declaringType = memberInfo.DeclaringType;

            // Implement interface members always explicitly.
            return declaringType.IsInterface && _interfaceTypes.Contains(declaringType);
        }

        #region ITypeBuilder Members

        /// <inheritdoc/>
        public void AddCustomAttribute(ConstructorInfo constructorInfo, params object[] arguments)
        {
            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            _typeBuilder.SetCustomAttribute(constructorInfo, arguments);
        }

        /// <inheritdoc/>
        public void AddInterface(Type interfaceType)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format(Resources.TypeNotAnInterfaceType, interfaceType), "interfaceType");

            if (interfaceType.IsGenericTypeDefinition)
                throw new ArgumentException(String.Format(Resources.InterfaceTypeMustNotBeAGenericTypeDefinition, interfaceType), "interfaceType");

            _typeBuilder.AddInterfaceImplementation(interfaceType);

            _interfaceTypes.Add(interfaceType);
        }

        /// <inheritdoc/>
        public void BuildConstructor(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            // Define constructor.
            var constructorBuilder = _typeBuilder.DefineConstructor(
                constructorInfo,
                new[] {typeof (IMemberInterceptor)},
                new[] {"interceptor"});

            // Implement constructor.
            var ilGenerator = constructorBuilder.GetILGenerator();
            var parameterInfos = constructorInfo.GetParameters();

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load arguments.
            ilGenerator.EmitLoadArguments(2, parameterInfos.Length);

            // Call parent constructor.
            ilGenerator.Emit(OpCodes.Call, constructorInfo);

            // Check for null interceptor.
            var interceptorNotNullLabel = ilGenerator.DefineLabel();

            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Brtrue, interceptorNotNullLabel);
            ilGenerator.ThrowException(typeof (ArgumentNullException), "interceptor");
            ilGenerator.MarkLabel(interceptorNotNullLabel);

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load interceptor.
            ilGenerator.Emit(OpCodes.Ldarg_1);

            // Store interceptor.
            ilGenerator.Emit(OpCodes.Stfld, _interceptorFieldInfo);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <inheritdoc/>
        public bool IsConcreteEvent(EventInfo eventInfo)
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            var methodInfos = eventInfo.GetAccessorMethods();

            return methodInfos.All(IsConcreteMethod);
        }

        /// <inheritdoc/>
        public void BuildEvent(EventInfo eventInfo)
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            var isExplicit = IsExplicitMember(eventInfo);

            BuildInterceptedEvent(eventInfo, isExplicit);
        }

        /// <inheritdoc/>
        public bool IsConcreteProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var methodInfos = propertyInfo.GetAccessorMethods();

            return methodInfos.All(IsConcreteMethod);
        }

        /// <inheritdoc/>
        public void BuildProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var isExplicit = IsExplicitMember(propertyInfo);

            BuildInterceptedProperty(propertyInfo, isExplicit);
        }

        /// <inheritdoc/>
        public bool IsConcreteMethod(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            return !methodInfo.IsAbstract && IsOverrideMember(methodInfo);
        }

        /// <inheritdoc/>
        public void BuildMethod(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            var isExplicit = IsExplicitMember(methodInfo);
            var invocationType = _invocationTypeRepository.GetType(methodInfo);

            BuildInterceptedMethod(methodInfo, isExplicit, invocationType, MethodInterceptorInvokeMethodInfo);
        }

        /// <inheritdoc/>
        public Type CreateType()
        {
            return _typeBuilder.CreateType();
        }

        #endregion
    }
}