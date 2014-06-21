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

namespace NProxy.Core
{
    /// <summary>
    /// Represents a runtime static part.
    /// </summary>
    internal sealed class RuntimeStaticPart : IStaticPart
    {
        /// <summary>
        /// The declaring name.
        /// </summary>
        private readonly string _declaringName;

        /// <summary>
        /// The method kind.
        /// </summary>
        private readonly MethodKind _methodKind;

        /// <summary>
        /// The method information.
        /// </summary>
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// The declaring type.
        /// </summary>
        private readonly Type _declaringType;

        /// <summary>
        /// Initailizes a new instance of the <see cref="RuntimeStaticPart"/> class.
        /// </summary>
        /// <param name="declaringName">The declaring name.</param>
        /// <param name="methodKind">The method kind.</param>
        /// <param name="typeHandle">The runtime type handle.</param>
        /// <param name="methodHandle">The runtime method handle.</param>
        public RuntimeStaticPart(string declaringName, MethodKind methodKind, RuntimeTypeHandle typeHandle, RuntimeMethodHandle methodHandle)
        {
            if (declaringName == null)
                throw new ArgumentNullException("declaringName");

            _declaringName = declaringName;
            _methodKind = methodKind;
            _methodInfo = (MethodInfo) MethodBase.GetMethodFromHandle(methodHandle, typeHandle);

            _declaringType = _methodInfo.DeclaringType;
        }

        #region IStaticPart Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _declaringType; }
        }

        /// <inheritdoc/>
        public string DeclaringName
        {
            get { return _declaringName; }
        }

        /// <inheritdoc/>
        public MethodKind MethodKind
        {
            get { return _methodKind; }
        }

        /// <inheritdoc/>
        public MethodInfo Method
        {
            get { return _methodInfo; }
        }

        #endregion
    }
}