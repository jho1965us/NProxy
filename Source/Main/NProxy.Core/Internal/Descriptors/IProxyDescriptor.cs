﻿//
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

namespace NProxy.Core.Internal.Descriptors
{
    /// <summary>
    /// Defines a proxy descriptor.
    /// </summary>
    internal interface IProxyDescriptor
    {
        /// <summary>
        /// Returns the declaring type.
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// Returns the parent type.
        /// </summary>
        Type ParentType { get; }

        /// <summary>
        /// Dispatches to the specific visit method for each member.
        /// </summary>
        /// <param name="proxyDescriptorVisitor">The proxy descriptor visitor.</param>
        void Accept(IProxyDescriptorVisitor proxyDescriptorVisitor);

        /// <summary>
        /// Returns the proxy instance for the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The proxy instance for the specified instance.</returns>
        object GetProxyInstance(object instance);

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="proxyType">The proxy type.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The instance.</returns>
        object CreateInstance(Type proxyType, object[] arguments);
    }
}