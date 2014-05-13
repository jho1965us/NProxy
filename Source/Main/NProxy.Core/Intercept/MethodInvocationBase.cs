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

namespace NProxy.Core.Intercept
{
    /// <summary>
    /// Represents a method invocation base.
    /// </summary>
    internal abstract class MethodInvocationBase : IMethodInvocation
    {
        /// <summary>
        /// The currently executing object.
        /// </summary>
        private readonly object _current;

        /// <summary>
        /// The declaring method information.
        /// </summary>
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// A value indicating whether the method is an override.
        /// </summary>
        private readonly bool _isOverride;

        /// <summary>
        /// The parameters.
        /// </summary>
        private readonly object[] _parameters;

        /// <summary>
        /// The declaring type.
        /// </summary>
        protected readonly Type DeclaringType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodInvocationBase"/> class.
        /// </summary>
        /// <param name="current">The currently executing object.</param>
        /// <param name="methodInfo">The declaring method information.</param>
        /// <param name="isOverride">A value indicating whether the method is an override.</param>
        /// <param name="parameters">The parameters.</param>
        protected MethodInvocationBase(object current, MethodInfo methodInfo, bool isOverride, object[] parameters)
        {
            if (current == null)
                throw new ArgumentNullException("current");

            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            if (parameters == null)
                throw new ArgumentNullException("parameters");

            _current = current;
            _methodInfo = methodInfo;
            _isOverride = isOverride;
            _parameters = parameters;

            DeclaringType = methodInfo.DeclaringType;
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

        #region IMethodInvocation Members

        /// <inheritdoc/>
        public MethodInfo Method
        {
            get { return _methodInfo; }
        }

        #endregion

        #region IInvocation Members

        /// <inheritdoc/>
        public object[] Parameters
        {
            get { return _parameters; }
        }

        #endregion

        #region IJoinpoint Members

        /// <inheritdoc/>
        public MemberInfo StaticPart
        {
            get { return _methodInfo; }
        }

        /// <inheritdoc/>
        public object This
        {
            get { return _current; }
        }

        /// <inheritdoc/>
        public object Proceed()
        {
            if (_isOverride)
                return InvokeBase(_current, _parameters);

            throw new TargetException(Resources.MethodNotImplemented);
        }

        /// <inheritdoc/>
        public object Proceed(object target)
        {
            // Invoke base method when target equals current object.
            if (ReferenceEquals(target, _current))
            {
                if (_isOverride)
                    return InvokeBase(target, _parameters);

                throw new TargetException(Resources.MethodNotImplemented);
            }

            if (target == null)
                throw new TargetException(Resources.MethodRequiresATargetObject);

            // Check target type.
            var targetType = target.GetType();

            if (!DeclaringType.IsAssignableFrom(targetType))
                throw new TargetException(Resources.MethodNotDeclaredOrInherited);

            // Invoke method on target object.
            return InvokeVirtual(target, _parameters);
        }

        #endregion
    }
}