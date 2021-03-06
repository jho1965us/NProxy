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

namespace NProxy.Core.Internal.Caching
{
    /// <summary>
    /// Defines a cache.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    internal interface ICache<TKey, TValue>
    {
        /// <summary>
        /// Gets or adds a value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>The value.</returns>
        TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);
    }
}