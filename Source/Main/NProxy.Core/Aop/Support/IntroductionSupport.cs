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
using System.Linq;
using NProxy.Core.Intercept;
using System.Collections.Generic;

namespace NProxy.Core.Aop.Support
{
    /// <summary>
    /// Represents an introduction support.
    /// </summary>
    public class IntroductionSupport : IIntroduction
    {
        /// <summary>
        /// The published interface types.
        /// </summary>
        private readonly HashSet<Type> _publishedInterfaceTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntroductionSupport"/> class.
        /// </summary>
        public IntroductionSupport()
        {
            _publishedInterfaceTypes = new HashSet<Type>();
        }

        /// <summary>
        /// Publish all interfaces that the given delegate implements at the proxy level.
        /// </summary>
        /// <param name="obj">The delegate object.</param>
        protected void ImplementInterfacesOnObject(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var type = obj.GetType();
            var interfaceTypes = type.GetInterfaces();

            foreach (var interfaceType in interfaceTypes)
            {
                _publishedInterfaceTypes.Add(interfaceType);
            }
        }

        /// <summary>
        /// Is this method on an introduced interface?
        /// </summary>
        /// <param name="invocation">The method invocation.</param>
        /// <returns>Whether the invoked method is on an introduced interface.</returns>
        protected bool IsMethodOnIntroducedInterface(IMethodInvocation invocation)
        {
            if (invocation == null)
                throw new ArgumentNullException("invocation");

            var declaringType = invocation.Method.DeclaringType;

            return _publishedInterfaceTypes.Contains(declaringType);
        }

        /// <summary>
        /// Check whether the specified interfaces is a published introduction interface.
        /// </summary>
        /// <param name="interfaceType">The interface types.</param>
        /// <returns>Whether the interface is part of this introduction.</returns>
        public bool ImplementsInterface(Type interfaceType)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            return _publishedInterfaceTypes.Contains(interfaceType);
        }

        /// <summary>
        /// Suppress the specified interface, which may have been autodetected due to the delegate implementing it.
        /// Call this method to exclude internal interfaces from being visible at the proxy level.
        /// Does nothing if the interface is not implemented by the delegate.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        public void SuppressInterface(Type interfaceType)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            _publishedInterfaceTypes.Remove(interfaceType);
        }

        #region IIntroduction Members

        /// <inheritdoc/>
        public Type[] Interfaces
        {
            get { return _publishedInterfaceTypes.ToArray(); }
        }

        #endregion
    }
}