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

using System.Collections.Generic;

namespace NProxy.Core.Aop
{
    /// <summary>
    /// Defines a
    /// </summary>
    public interface IAdvised
    {
        /// <summary>
        /// Adds the specified advice at the end of the advice (interceptor) chain.
        /// </summary>
        /// <param name="advice">The advice.</param>
        void AddAdvice(IAdvice advice);

        /// <summary>
        /// Removes the specified advice at the end of the advice (interceptor) chain.
        /// </summary>
        /// <param name="advice">The advice.</param>
        void RemoveAdvice(IAdvice advice);

        /// <summary>
        /// Adds the specified advisor at the end of the advisor chain.
        /// </summary>
        /// <param name="advisor"></param>
        void AddAdvisor(IAdvisor advisor);

        /// <summary>
        /// Removes the specified advisor at the end of the advisor chain.
        /// </summary>
        /// <param name="advisor"></param>
        void RemoveAdvisor(IAdvisor advisor);

        /// <summary>
        /// Returns the advisors.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAdvisor> GetAdvisors();
    }
}