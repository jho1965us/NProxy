﻿//
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

namespace NProxy.Core.Aop
{
    /// <summary>
    /// Defines a dynamic introduction advice.
    /// </summary>
    public interface IDynamicIntroductionAdvice : IAdvice
    {
        /// <summary>
        /// Does this introduction advice implement the given interface?
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        /// <returns>Whether the advice implements the specified interface.</returns>
        bool ImplementsInterface(Type interfaceType);
    }
}