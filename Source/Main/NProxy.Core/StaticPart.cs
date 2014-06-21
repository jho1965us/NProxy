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
    /// Represents a static part.
    /// </summary>
    internal sealed class StaticPart : IStaticPart
    {
        /// <summary>
        /// The member information.
        /// </summary>
        private readonly MemberInfo _memberInfo;

        /// <summary>
        /// The method kind.
        /// </summary>
        private readonly MethodKind _methodKind;

        /// <summary>
        /// The method information.
        /// </summary>
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// Initailizes a new instance of the <see cref="StaticPart"/> class.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="methodKind">The method kind.</param>
        /// <param name="methodInfo">The method information.</param>
        public StaticPart(MemberInfo memberInfo, MethodKind methodKind, MethodInfo methodInfo)
        {
            _memberInfo = memberInfo;
            _methodKind = methodKind;
            _methodInfo = methodInfo;
        }

        #region IStaticPart Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _memberInfo.DeclaringType; }
        }

        /// <inheritdoc/>
        public string DeclaringName
        {
            get { return _memberInfo.Name; }
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