﻿//
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
using NProxy.Core.Intercept;

namespace NProxy.Core
{
    /// <summary>
    /// Provides <see cref="IProxyRepository"/> extension methods.
    /// </summary>
    public static class ProxyRepositoryExtensions
    {
        /// <summary>
        /// Returns a proxy factory.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyRepository">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <returns>The proxy factory.</returns>
        public static IProxyFactory<T> GetProxyFactory<T>(this IProxyRepository proxyRepository, IEnumerable<Type> interfaceTypes) where T : class
        {
            var proxyFactory = proxyRepository.GetProxyFactory(typeof (T), interfaceTypes);

            return new ProxyFactory<T>(proxyFactory);
        }

        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <param name="proxyRepository">The proxy factory.</param>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="interceptor">The interceptor.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        public static object CreateProxy(this IProxyRepository proxyRepository,
            Type declaringType,
            IEnumerable<Type> interfaceTypes,
            IMemberInterceptor interceptor,
            params object[] arguments)
        {
            if (proxyRepository == null)
                throw new ArgumentNullException("proxyRepository");

            var proxyFactory = proxyRepository.GetProxyFactory(declaringType, interfaceTypes);

            return proxyFactory.CreateProxy(interceptor, arguments);
        }

        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyRepository">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="interceptor">The interceptor.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        public static T CreateProxy<T>(this IProxyRepository proxyRepository,
            IEnumerable<Type> interfaceTypes,
            IMemberInterceptor interceptor,
            params object[] arguments) where T : class
        {
            if (proxyRepository == null)
                throw new ArgumentNullException("proxyRepository");

            var proxyFactory = proxyRepository.GetProxyFactory<T>(interfaceTypes);

            return proxyFactory.CreateProxy(interceptor, arguments);
        }
    }
}