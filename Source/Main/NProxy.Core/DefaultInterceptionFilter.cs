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

namespace NProxy.Core
{
    /// <summary>
    /// Represents an interception filter that accepts all members except the destructor method.
    /// </summary>
    internal sealed class DefaultInterceptionFilter : IInterceptionFilter
    {
        /// <summary>
        /// The immutable singleton instance.
        /// </summary>
        public static readonly DefaultInterceptionFilter Instance = new DefaultInterceptionFilter();

        /// <summary>
        /// The name of the destructor method.
        /// </summary>
        private const string DestructorMethodName = "Finalize";

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultInterceptionFilter"/> class.
        /// </summary>
        private DefaultInterceptionFilter()
        {
        }

        #region IInterceptionFilter Members

        /// <inheritdoc/>
        public bool AcceptEvent(EventInfo eventInfo)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool AcceptProperty(PropertyInfo propertyInfo)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool AcceptMethod(MethodInfo methodInfo)
        {
            // Don't intercept the destructor method.
            if (methodInfo.DeclaringType != typeof (object))
                return true;

            return !String.Equals(methodInfo.Name, DestructorMethodName);
        }

        #endregion
    }
}