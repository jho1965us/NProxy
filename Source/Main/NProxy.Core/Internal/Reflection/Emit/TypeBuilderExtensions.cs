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
using System.Reflection;
using System.Reflection.Emit;

namespace NProxy.Core.Internal.Reflection.Emit
{
    /// <summary>
    /// Provides <see cref="TypeBuilder"/> extension methods.
    /// </summary>
    internal static class TypeBuilderExtensions
    {
        /// <summary>
        /// Defines the generic type parameters.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="genericTypes">The generic types.</param>
        /// <returns>The generic parameter types.</returns>
        /// <remarks>
        /// Custom attributes are not considered by this method.
        /// </remarks>
        public static Type[] DefineGenericParameters(this TypeBuilder typeBuilder, Type[] genericTypes)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (genericTypes == null)
                throw new ArgumentNullException("genericTypes");

            if (genericTypes.Length == 0)
                return Type.EmptyTypes;

            var genericParameterNames = Array.ConvertAll(genericTypes, t => t.Name);
            var genericParameterBuilders = typeBuilder.DefineGenericParameters(genericParameterNames);

            foreach (var genericParameterBuilder in genericParameterBuilders)
            {
                var genericType = genericTypes[genericParameterBuilder.GenericParameterPosition];

                // Set generic parameter attributes.
                genericParameterBuilder.SetGenericParameterAttributes(genericType.GenericParameterAttributes);

                // Set generic parameter constraints.
                var constraints = genericType.GetGenericParameterConstraints();

                if (constraints.Length == 0)
                    continue;

                var interfaceConstraints = new List<Type>();

                foreach (var constraint in constraints)
                {
                    if (constraint.IsInterface)
                        interfaceConstraints.Add(constraint);
                    else
                        genericParameterBuilder.SetBaseTypeConstraint(constraint);
                }

                genericParameterBuilder.SetInterfaceConstraints(interfaceConstraints.ToArray());
            }

            return Array.ConvertAll(genericParameterBuilders, b => (Type) b);
        }

        /// <summary>
        /// Defines a constructor.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="methodAttributes">The method attributes.</param>
        /// <param name="callingConvention">The calling convention.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parameterNames">The parameter names.</param>
        /// <returns>The constructor builder.</returns>
        public static ConstructorBuilder DefineConstructor(this TypeBuilder typeBuilder,
            MethodAttributes methodAttributes,
            CallingConventions callingConvention,
            Type[] parameterTypes,
            string[] parameterNames)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (parameterTypes == null)
                throw new ArgumentNullException("parameterTypes");

            if (parameterNames == null)
                throw new ArgumentNullException("parameterNames");

            if (parameterTypes.Length != parameterNames.Length)
                throw new ArgumentException(Resources.NumberOfParameterTypesAndNamesMustBeEqual);

            // Define constructor.
            var constructorBuilder = typeBuilder.DefineConstructor(
                methodAttributes,
                callingConvention,
                parameterTypes);

            // Define constructor parameters.
            constructorBuilder.DefineParameters(parameterNames);

            return constructorBuilder;
        }

        /// <summary>
        /// Defines the constructor parameters.
        /// </summary>
        /// <param name="constructorBuilder">The constructor builder.</param>
        /// <param name="parameterNames">The parameter names.</param>
        private static void DefineParameters(this ConstructorBuilder constructorBuilder, IEnumerable<string> parameterNames)
        {
            var position = 1;

            // Define additional constructor parameters.
            foreach (var parameterName in parameterNames)
            {
                constructorBuilder.DefineParameter(position++, ParameterAttributes.None, parameterName);
            }
        }

        /// <summary>
        /// Defines a constructor based on the specified constructor.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <param name="additionalParameterTypes">The additional parameter types.</param>
        /// <param name="additionalParameterNames">The additional parameter names.</param>
        /// <returns>The constructor builder.</returns>
        public static ConstructorBuilder DefineConstructor(this TypeBuilder typeBuilder,
            ConstructorInfo constructorInfo,
            IEnumerable<Type> additionalParameterTypes,
            IEnumerable<string> additionalParameterNames)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            if (additionalParameterTypes == null)
                throw new ArgumentNullException("additionalParameterTypes");

            if (additionalParameterNames == null)
                throw new ArgumentNullException("additionalParameterNames");

            var methodAttributes = MethodAttributes.Public |
                                   constructorInfo.Attributes & (MethodAttributes.HideBySig |
                                                                 MethodAttributes.SpecialName |
                                                                 MethodAttributes.ReservedMask);
            var parameterTypes = new List<Type>();

            parameterTypes.AddRange(additionalParameterTypes);

            var parentParameterTypes = constructorInfo.GetParameterTypes();

            parameterTypes.AddRange(parentParameterTypes);

            // Define constructor.
            var constructorBuilder = typeBuilder.DefineConstructor(
                methodAttributes,
                constructorInfo.CallingConvention,
                parameterTypes.ToArray());

            // Define constructor parameters.
            constructorBuilder.DefineParameters(constructorInfo, additionalParameterNames);

            return constructorBuilder;
        }

        /// <summary>
        /// Defines the constructor parameters based on the specified constructor.
        /// </summary>
        /// <param name="constructorBuilder">The constructor builder.</param>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <param name="additionalParameterNames">The additional parameter names.</param>
        private static void DefineParameters(this ConstructorBuilder constructorBuilder,
            ConstructorInfo constructorInfo,
            IEnumerable<string> additionalParameterNames)
        {
            var position = 1;

            // Define additional constructor parameters.
            foreach (var additionalParameterName in additionalParameterNames)
            {
                constructorBuilder.DefineParameter(position++, ParameterAttributes.None, additionalParameterName);
            }

            // Define base constructor parameters.
            var parameterInfos = constructorInfo.GetParameters();

            foreach (var parameterInfo in parameterInfos)
            {
                constructorBuilder.DefineParameter(parameterInfo.Position + position, parameterInfo.Attributes, parameterInfo.Name);
            }
        }

        /// <summary>
        /// Defines a method based on the specified method.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="isExplicit">A value indicating whether the specified method should be implemented explicitly.</param>
        /// <param name="isOverride">A value indicating whether the specified method should be overridden.</param>
        /// <returns>The method builder.</returns>
        public static MethodBuilder DefineMethod(this TypeBuilder typeBuilder,
            MethodInfo methodInfo,
            bool isExplicit,
            bool isOverride)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            // Define method attributes.
            var methodAttributes = methodInfo.Attributes & (MethodAttributes.HideBySig |
                                                            MethodAttributes.SpecialName |
                                                            MethodAttributes.ReservedMask);

            if (isExplicit)
                methodAttributes |= MethodAttributes.Private;
            else
                methodAttributes |= methodInfo.Attributes & MethodAttributes.MemberAccessMask;

            if (isOverride)
            {
                methodAttributes |= MethodAttributes.Virtual;

                var declaringType = methodInfo.DeclaringType;

                if (declaringType.IsInterface)
                    methodAttributes |= MethodAttributes.NewSlot;
                else
                    methodAttributes |= MethodAttributes.ReuseSlot;
            }

            var methodName = isExplicit ? methodInfo.GetFullName() : methodInfo.Name;

            // Define method.
            return typeBuilder.DefineMethod(
                methodName,
                methodAttributes,
                methodInfo.CallingConvention);
        }

        /// <summary>
        /// Sets the specified custom attribute.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <param name="arguments">The constructor arguments.</param>
        public static void SetCustomAttribute(this TypeBuilder typeBuilder, ConstructorInfo constructorInfo, object[] arguments)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var customAttributeBuilder = new CustomAttributeBuilder(constructorInfo, arguments);

            typeBuilder.SetCustomAttribute(customAttributeBuilder);
        }
    }
}