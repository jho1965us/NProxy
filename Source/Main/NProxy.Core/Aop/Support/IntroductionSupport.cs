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

using System;
using NProxy.Core.Aop;
using NProxy.Core.Intercept;
using System.Collections.Generic;

namespace NProxy.Core.Aop.Support
{
	public class IntroductionSupport : IIntroduction
	{
		private readonly HashSet<Type> _publishedInterfaceTypes;

		public IntroductionSupport()
		{
			_publishedInterfaceTypes = new HashSet<Type>();
		}

		/// <summary>
		/// Publish all interfaces that the given delegate implements at the proxy level.
		/// </summary>
		/// <param name="obj">The delegate object.</param>
		protected void ImplementInterfacesOnObject(object obj)
		{
		}

		/// <summary>
		/// Is this method on an introduced interface?
		/// </summary>
		/// <param name="invocation">The method invocation.</param>
		/// <returns>Whether the invoked method is on an introduced interface.</returns>
		protected bool IsMethodOnIntroducedInterface(IMethodInvocation invocation)
		{
			return false;
		}

		/// <summary>
		/// Check whether the specified interfaces is a published introduction interface.
		/// </summary>
		/// <param name="interfaceType">The interface types.</param>
		/// <returns>Whether the interface is part of this introduction.</returns>
		public bool ImplementsInterface(Type interfaceType)
		{
			return false;
		}

		/// <summary>
		/// Suppress the specified interface, which may have been autodetected due to the delegate implementing it.
		/// Call this method to exclude internal interfaces from being visible at the proxy level.
		/// Does nothing if the interface is not implemented by the delegate.
		/// </summary>
		/// <param name="interfaceType">The interface type.</param>
		public void SuppressInterface(Type interfaceType)
		{
		}

		#region IIntroduction Members

		public Type[] Interfaces
		{
			get
			{
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
}