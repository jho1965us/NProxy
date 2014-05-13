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