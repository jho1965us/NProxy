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
using NProxy.Core.Intercept;

namespace NProxy.Core.Benchmark
{
    internal sealed class NProxyInterceptor : IMemberInterceptor
    {
        private readonly object _target;

        public NProxyInterceptor(object target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            _target = target;
        }

        #region IEventInterceptor Members

        public object Add(IEventInvocation invocation)
        {
            return invocation.Proceed(_target);
        }

        public object Remove(IEventInvocation invocation)
        {
            return invocation.Proceed(_target);
        }

        public object Raise(IEventInvocation invocation)
        {
            return invocation.Proceed(_target);
        }

        public object Other(IEventInvocation invocation)
        {
            return invocation.Proceed(_target);
        }

        #endregion

        #region IPropertyInterceptor Members

        public object Get(IPropertyInvocation invocation)
        {
            return invocation.Proceed(_target);
        }

        public object Set(IPropertyInvocation invocation)
        {
            return invocation.Proceed(_target);
        }

        #endregion

        #region IMethodInterceptor Members

        public object Invoke(IMethodInvocation invocation)
        {
            return invocation.Proceed(_target);
        }

        #endregion
    }
}