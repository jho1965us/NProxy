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