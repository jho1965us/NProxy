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