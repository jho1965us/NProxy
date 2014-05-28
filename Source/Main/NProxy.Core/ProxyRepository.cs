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
using NProxy.Core.Internal.Caching;
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Reflection.Emit;

namespace NProxy.Core
{
    /// <summary>
    /// Represents the proxy factory.
    /// </summary>
    public sealed class ProxyRepository : IProxyRepository
    {
        /// <summary>
        /// The type builder factory.
        /// </summary>
        private readonly ITypeBuilderFactory _typeBuilderFactory;

        /// <summary>
        /// The interception filter.
        /// </summary>
        private readonly IInterceptionFilter _interceptionFilter;

        /// <summary>
        /// The proxy factory cache.
        /// </summary>
        private readonly ICache<IProxyDefinition, IProxyFactory> _proxyFactoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyRepository"/> class.
        /// </summary>
        public ProxyRepository()
            : this(DefaultInterceptionFilter.Instance)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyRepository"/> class.
        /// </summary>
        /// <param name="interceptionFilter">The interception filter.</param>
        public ProxyRepository(IInterceptionFilter interceptionFilter)
            : this(new ProxyTypeBuilderFactory(false), interceptionFilter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyRepository"/> class.
        /// </summary>
        /// <param name="typeBuilderFactory">The type builder factory.</param>
        /// <param name="interceptionFilter">The interception filter.</param>
        internal ProxyRepository(ITypeBuilderFactory typeBuilderFactory, IInterceptionFilter interceptionFilter)
        {
            if (typeBuilderFactory == null)
                throw new ArgumentNullException("typeBuilderFactory");

            if (interceptionFilter == null)
                throw new ArgumentNullException("interceptionFilter");

            _typeBuilderFactory = typeBuilderFactory;
            _interceptionFilter = interceptionFilter;

            _proxyFactoryCache = new LockOnWriteCache<IProxyDefinition, IProxyFactory>();
        }

        /// <summary>
        /// Creates a proxy definition for the specified declaring type and interface types.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns>The proxy definition.</returns>
        private static IProxyDefinition CreateProxyDefinition(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType.IsDelegate())
                return new DelegateProxyDefinition(declaringType, interfaceTypes);

            if (declaringType.IsInterface)
                return new InterfaceProxyDefinition(declaringType, interfaceTypes);

            return new ClassProxyDefinition(declaringType, interfaceTypes);
        }

        /// <summary>
        /// Creates a proxy factory.
        /// </summary>
        /// <param name="proxyDefinition">The proxy definition.</param>
        /// <returns>The proxy factory.</returns>
        private IProxyFactory CreateProxyFactory(IProxyDefinition proxyDefinition)
        {
            var typeBuilder = _typeBuilderFactory.CreateBuilder(proxyDefinition.ParentType);
            var proxyFactoryBuilder = new ProxyFactoryBuilder(typeBuilder, _interceptionFilter);

            return proxyFactoryBuilder.CreateProxyFactory(proxyDefinition);
        }

        #region IProxyRepository Members

        /// <inheritdoc/>
        public IProxyFactory GetProxyFactory(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            // Create proxy definition.
            var proxyDefinition = CreateProxyDefinition(declaringType, interfaceTypes);

            // Get or create proxy factory.
            return _proxyFactoryCache.GetOrAdd(proxyDefinition, CreateProxyFactory);
        }

        #endregion
    }
}