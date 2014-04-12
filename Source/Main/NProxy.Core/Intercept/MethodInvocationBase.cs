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
using NProxy.Core;
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