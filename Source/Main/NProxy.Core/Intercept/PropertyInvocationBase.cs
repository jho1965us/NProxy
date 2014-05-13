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