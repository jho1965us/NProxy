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
using NProxy.Core.Internal;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a method invocation base.
    /// </summary>
    internal abstract class InvocationBase : IInvocation
    {
        /// <summary>
        /// The static part.
        /// </summary>
        private readonly IStaticPart _staticPart;

        /// <summary>
        /// A value indicating whether the method is an override.
        /// </summary>
        private readonly bool _isOverride;

        /// <summary>
        /// The target object.
        /// </summary>
        private readonly object _target;

        /// <summary>
        /// The parameters.
        /// </summary>
        private readonly object[] _parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationBase"/> class.
        /// </summary>
        /// <param name="staticPart">The static part.</param>
        /// <param name="isOverride">A value indicating whether the method is an override.</param>
        /// <param name="target">The target object.</param>
        /// <param name="parameters">The parameters.</param>
        protected InvocationBase(IStaticPart staticPart, bool isOverride, object target, object[] parameters)
        {
            if (staticPart == null)
                throw new ArgumentNullException("staticPart");

            if (target == null)
                throw new ArgumentNullException("target");

            if (parameters == null)
                throw new ArgumentNullException("parameters");

            _staticPart = staticPart;
            _isOverride = isOverride;
            _target = target;
            _parameters = parameters;
        }

        /// <summary>
        /// Invokes the base method represented by the current instance.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The return value.</returns>
        protected virtual object InvokeBase(object target, object[] parameters)
        {
            throw new TargetException(Resources.MethodNotImplemented);
        }

        /// <summary>
        /// Invokes the virtual method represented by the current instance.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The return value.</returns>
        protected abstract object InvokeVirtual(object target, object[] parameters);

        #region IInvocation Members

        /// <inheritdoc/>
        public IStaticPart StaticPart
        {
            get { return _staticPart; }
        }

        /// <inheritdoc/>
        public object This
        {
            get { return _target; }
        }

        /// <inheritdoc/>
        public object[] Parameters
        {
            get { return _parameters; }
        }

        /// <inheritdoc/>
        public object Proceed()
        {
            if (_isOverride)
                return InvokeBase(_target, _parameters);

            throw new TargetException(Resources.MethodNotImplemented);
        }

        /// <inheritdoc/>
        public object Proceed(object target)
        {
            // Invoke base method when target equals current object.
            if (ReferenceEquals(target, _target))
            {
                if (_isOverride)
                    return InvokeBase(target, _parameters);

                throw new TargetException(Resources.MethodNotImplemented);
            }

            if (target == null)
                throw new TargetException(Resources.MethodRequiresATargetObject);

            // Check target type.
            var targetType = target.GetType();

            if (!_staticPart.DeclaringType.IsAssignableFrom(targetType))
                throw new TargetException(Resources.MethodNotDeclaredOrInherited);

            // Invoke method on target object.
            return InvokeVirtual(target, _parameters);
        }

        #endregion
    }
}