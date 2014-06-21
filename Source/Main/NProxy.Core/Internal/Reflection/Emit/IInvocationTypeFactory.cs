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

namespace NProxy.Core.Internal.Reflection.Emit
{
    /// <summary>
    /// Defines an invocation type factory.
    /// </summary>
    internal interface IInvocationTypeFactory
    {
        /// <summary>
        /// Creates a new invocation type for the specified static part.
        /// </summary>
        /// <param name="staticPart">The static part.</param>
        /// <returns>The new invocation type.</returns>
        Type CreateType(IStaticPart staticPart);
    }
}