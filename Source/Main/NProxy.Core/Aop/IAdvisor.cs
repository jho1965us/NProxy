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

namespace NProxy.Core.Aop
{
    /// <summary>
    /// Defines an advisor.
    /// </summary>
    public interface IAdvisor
    {
        /// <summary>
        /// Return whether this advice is associated with a particular instance
        /// (for example, creating a mixin) or shared with all instances of the
        /// advised class obtained from the same factory.
        /// </summary>
        bool IsPerInstance { get; }

        /// <summary>
        ///  Return the advice part of this aspect.
        /// </summary>
        IAdvice Advice { get; }
    }
}