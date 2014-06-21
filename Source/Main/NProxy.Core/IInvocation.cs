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

namespace NProxy.Core
{
    /// <summary>
    /// Defines an invocation.
    /// </summary>
    public interface IInvocation
    {
        /// <summary>
        /// Returns the static part of this invocation.
        /// </summary>
        IStaticPart StaticPart { get; }

        /// <summary>
        /// Returns the object that holds the current invocation's static part. 
        /// </summary>
        object This { get; }

        /// <summary>
        /// Returns the parameters as an object array.
        /// </summary>
        object[] Parameters { get; }

        /// <summary>
        /// Proceeds to the next interceptor in the chain.
        /// </summary>
        /// <returns>The return value.</returns>
        object Proceed();

        /// <summary>
        /// Proceeds to the next interceptor in the chain.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>The return value.</returns>
        object Proceed(object target);
    }
}