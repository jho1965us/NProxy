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

namespace NProxy.Core.Aop
{
    /// <summary>
    /// Defines a method matcher.
    /// </summary>
    public interface IMethodMatcher
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsRuntime { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        bool Matches(MethodInfo methodInfo, Type targetType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="targetType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool Matches(MethodInfo methodInfo, Type targetType, Object[] parameters);
    }
}