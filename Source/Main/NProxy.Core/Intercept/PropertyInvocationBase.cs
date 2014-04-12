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
using System.Reflection;

namespace NProxy.Core.Intercept
{
    /// <summary>
    /// Represents a property invocation base.
    /// </summary>
    internal abstract class PropertyInvocationBase : MethodInvocationBase, IPropertyInvocation
    {
        /// <summary>
        /// The property name.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyInvocationBase"/> class.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="current">The currently executing object.</param>
        /// <param name="methodInfo">The declaring method information.</param>
        /// <param name="isOverride">A value indicating whether the method is an override.</param>
        /// <param name="parameters">The parameters.</param>
        protected PropertyInvocationBase(string name, object current, MethodInfo methodInfo, bool isOverride, object[] parameters)
            : base(current, methodInfo, isOverride, parameters)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            _name = name;
        }

        #region IPropertyInvocation Members

        /// <inheritdoc/>
        public PropertyInfo Property
        {
            get { return DeclaringType.GetProperty(_name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic); }
        }

        #endregion
    }
}