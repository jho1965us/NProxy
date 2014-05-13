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

using NProxy.Core.Intercept;

namespace NProxy.Core.Test
{
    internal sealed class SetReturnValueInterceptor : IMemberInterceptor
    {
        private readonly object _returnValue;

        public SetReturnValueInterceptor(object returnValue)
        {
            _returnValue = returnValue;
        }

        private object HandleInvocation(IInvocation invocation)
        {
            return _returnValue;
        }

        #region IEventInterceptor Members

        public object Add(IEventInvocation invocation)
        {
            return HandleInvocation(invocation);
        }

        public object Remove(IEventInvocation invocation)
        {
            return HandleInvocation(invocation);
        }

        public object Raise(IEventInvocation invocation)
        {
            return HandleInvocation(invocation);
        }

        public object Other(IEventInvocation invocation)
        {
            return HandleInvocation(invocation);
        }

        #endregion

        #region IPropertyInterceptor Members

        public object Get(IPropertyInvocation invocation)
        {
            return HandleInvocation(invocation);
        }

        public object Set(IPropertyInvocation invocation)
        {
            return HandleInvocation(invocation);
        }

        #endregion

        #region IMethodInterceptor Members

        public object Invoke(IMethodInvocation invocation)
        {
            return HandleInvocation(invocation);
        }

        #endregion
    }
}