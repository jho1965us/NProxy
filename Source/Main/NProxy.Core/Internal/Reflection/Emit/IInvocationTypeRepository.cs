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

namespace NProxy.Core.Internal.Reflection.Emit
{
    /// <summary>
    /// Defines an invocation type repository.
    /// </summary>
    internal interface IInvocationTypeRepository : ITypeRepository
    {
        /// <summary>
        /// Returns an invocation type for the specified method.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The invocation type.</returns>
        Type GetInvocationType(EventInfo eventInfo, MethodInfo methodInfo);

        /// <summary>
        /// Returns an invocation type for the specified method.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The invocation type.</returns>
        Type GetInvocationType(PropertyInfo propertyInfo, MethodInfo methodInfo);

        /// <summary>
        /// Returns an invocation type for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The invocation type.</returns>
        Type GetInvocationType(MethodInfo methodInfo);
    }
}