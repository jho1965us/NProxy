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
using NProxy.Core.Intercept;
using NProxy.Core.Internal;
using NProxy.Core.Internal.Definitions;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy factory.
    /// </summary>
    internal class ProxyFactory : IProxyFactory
    {
        /// <summary>
        /// The proxy definition.
        /// </summary>
        private readonly IProxyDefinition _proxyDefinition;

        /// <summary>
        /// The implementation type.
        /// </summary>
        private readonly Type _implementationType;

        /// <summary>
        /// The event informations.
        /// </summary>
        private readonly ICollection<EventInfo> _eventInfos;

        /// <summary>
        /// The property informations.
        /// </summary>
        private readonly ICollection<PropertyInfo> _propertyInfos;

        /// <summary>
        /// The method informations.
        /// </summary>
        private readonly ICollection<MethodInfo> _methodInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        /// <param name="proxyDefinition">The proxy definition.</param>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="eventInfos">The event informations.</param>
        /// <param name="propertyInfos">The property informations.</param>
        /// <param name="methodInfos">The method informations.</param>
        public ProxyFactory(IProxyDefinition proxyDefinition, Type implementationType, ICollection<EventInfo> eventInfos, ICollection<PropertyInfo> propertyInfos, ICollection<MethodInfo> methodInfos)
        {
            if (proxyDefinition == null)
                throw new ArgumentNullException("proxyDefinition");

            if (implementationType == null)
                throw new ArgumentNullException("implementationType");

            if (eventInfos == null)
                throw new ArgumentNullException("eventInfos");

            if (propertyInfos == null)
                throw new ArgumentNullException("propertyInfos");

            if (methodInfos == null)
                throw new ArgumentNullException("methodInfos");

            _proxyDefinition = proxyDefinition;
            _implementationType = implementationType;
            _eventInfos = eventInfos;
            _propertyInfos = propertyInfos;
            _methodInfos = methodInfos;
        }

        #region IProxyFactory Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _proxyDefinition.DeclaringType; }
        }

        /// <inheritdoc/>
        public Type ParentType
        {
            get { return _proxyDefinition.ParentType; }
        }

        /// <inheritdoc/>
        public Type ImplementationType
        {
            get { return _implementationType; }
        }

        /// <inheritdoc/>
        public IEnumerable<Type> ImplementedInterfaces
        {
            get { return _proxyDefinition.ImplementedInterfaces; }
        }

        /// <inheritdoc/>
        public IEnumerable<EventInfo> InterceptedEvents
        {
            get { return _eventInfos; }
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyInfo> InterceptedProperties
        {
            get { return _propertyInfos; }
        }

        /// <inheritdoc/>
        public IEnumerable<MethodInfo> InterceptedMethods
        {
            get { return _methodInfos; }
        }

        /// <inheritdoc/>
        public object AdaptProxy(Type interfaceType, object proxy)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format(Resources.TypeNotAnInterfaceType, interfaceType), "interfaceType");

            var instance = _proxyDefinition.UnwrapProxy(proxy);
            var instanceType = instance.GetType();

            if ((instanceType != _implementationType) || !interfaceType.IsAssignableFrom(instanceType))
                throw new InvalidOperationException(Resources.CannotAdaptProxy);

            return instance;
        }

        /// <inheritdoc/>
        public object CreateProxy(IMemberInterceptor interceptor, params object[] arguments)
        {
            if (interceptor == null)
                throw new ArgumentNullException("interceptor");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var constructorArguments = new List<object> {interceptor};

            constructorArguments.AddRange(arguments);

            return _proxyDefinition.CreateProxy(_implementationType, constructorArguments.ToArray());
        }

        #endregion
    }

    /// <summary>
    /// Represents a proxy factory.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    internal sealed class ProxyFactory<T> : IProxyFactory<T> where T : class
    {
        /// <summary>
        /// The proxy factory.
        /// </summary>
        private readonly IProxyFactory _proxyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory{T}"/> class.
        /// </summary>
        /// <param name="proxyFactory">The proxy factory.</param>
        public ProxyFactory(IProxyFactory proxyFactory)
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            _proxyFactory = proxyFactory;
        }

        #region IProxyFactory Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _proxyFactory.DeclaringType; }
        }

        /// <inheritdoc/>
        public Type ParentType
        {
            get { return _proxyFactory.ParentType; }
        }

        /// <inheritdoc/>
        public Type ImplementationType
        {
            get { return _proxyFactory.ImplementationType; }
        }

        /// <inheritdoc/>
        public IEnumerable<Type> ImplementedInterfaces
        {
            get { return _proxyFactory.ImplementedInterfaces; }
        }

        /// <inheritdoc/>
        public IEnumerable<EventInfo> InterceptedEvents
        {
            get { return _proxyFactory.InterceptedEvents; }
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyInfo> InterceptedProperties
        {
            get { return _proxyFactory.InterceptedProperties; }
        }

        /// <inheritdoc/>
        public IEnumerable<MethodInfo> InterceptedMethods
        {
            get { return _proxyFactory.InterceptedMethods; }
        }

        /// <inheritdoc/>
        public object AdaptProxy(Type interfaceType, object proxy)
        {
            return _proxyFactory.AdaptProxy(interfaceType, proxy);
        }

        /// <inheritdoc/>
        object IProxyFactory.CreateProxy(IMemberInterceptor interceptor, params object[] arguments)
        {
            return _proxyFactory.CreateProxy(interceptor, arguments);
        }

        #endregion

        #region IProxyFactory<T> Members

        /// <inheritdoc/>
        public T CreateProxy(IMemberInterceptor interceptor, params object[] arguments)
        {
            return (T) _proxyFactory.CreateProxy(interceptor, arguments);
        }

        #endregion
    }
}