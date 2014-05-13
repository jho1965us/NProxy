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

using System.Reflection;

namespace NProxy.Core.Intercept
{
    /// <summary>
    /// Defines a joinpoint.
    /// </summary>
    public interface IJoinpoint
    {
        /// <summary>
        /// Returns the static part of this joinpoint.
        /// </summary>
        MemberInfo StaticPart { get; }

        /// <summary>
        /// Returns the object that holds the current joinpoint's static part. 
        /// </summary>
        object This { get; }

        /// <summary>
        /// Proceeds to the next interceptor in the chain.
        /// </summary>
        /// <returns>See the children interfaces' proceed definition.</returns>
        object Proceed();

        /// <summary>
        /// Proceeds to the next interceptor in the chain.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>See the children interfaces' proceed definition.</returns>
        object Proceed(object target);
    }
}