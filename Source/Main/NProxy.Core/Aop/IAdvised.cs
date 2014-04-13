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