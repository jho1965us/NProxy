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