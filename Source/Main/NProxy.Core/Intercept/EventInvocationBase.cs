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
    /// Represents a event invocation base.
    /// </summary>
    internal abstract class EventInvocationBase : MethodInvocationBase, IEventInvocation
    {
        /// <summary>
        /// The event name.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventInvocationBase"/> class.
        /// </summary>
        /// <param name="name">The event name.</param>
        /// <param name="current">The target object.</param>
        /// <param name="methodInfo">The declaring method information.</param>
        /// <param name="isOverride">A value indicating whether the method is an override.</param>
        /// <param name="parameters">The parameters.</param>
        protected EventInvocationBase(string name, object target, MethodInfo methodInfo, bool isOverride, object[] parameters)
            : base(target, methodInfo, isOverride, parameters)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            _name = name;
        }

        #region IEventInvocation Members

        /// <inheritdoc/>
        public EventInfo Event
        {
            get { return DeclaringType.GetEvent(_name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic); }
        }

        #endregion
    }
}