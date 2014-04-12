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

using System.Reflection;
using NProxy.Core.Intercept;

namespace NProxy.Core.Test
{
    internal sealed class CountingInterceptor : IMemberInterceptor
    {
        public int InvocationCount { get; private set; }

        private object Handle(IInvocation invocation)
        {
            InvocationCount++;

            return null;
        }

        #region IEventInterceptor Members

        public object Add(IEventInvocation invocation)
        {
            return Handle(invocation);
        }

        public object Remove(IEventInvocation invocation)
        {
            return Handle(invocation);
        }

        public object Raise(IEventInvocation invocation)
        {
            return Handle(invocation);
        }

        public object Other(IEventInvocation invocation)
        {
            return Handle(invocation);
        }

        #endregion

        #region IPropertyInterceptor Members

        public object Get(IPropertyInvocation invocation)
        {
            return Handle(invocation);
        }

        public object Set(IPropertyInvocation invocation)
        {
            return Handle(invocation);
        }

        #endregion

        #region IMethodInterceptor Members

        public object Invoke(IMethodInvocation invocation)
        {
            return Handle(invocation);
        }

        #endregion
    }
}