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
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test
{
    [TestFixture]
    internal sealed class ProxyFactoryTestFixture
    {
        private ProxyRepository _proxyRepository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _proxyRepository = new ProxyRepository();
        }

        [Test]
        public void AdaptProxyInterfaceProxyTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<IIntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyFactory.CreateProxy(new TargetInterceptor(_ => null));

            // Act
            var value = proxyFactory.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyAbstractClassProxyTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<IntParameterBase>(new[] {typeof (IStringParameter)});
            var proxy = proxyFactory.CreateProxy(new TargetInterceptor(_ => null));

            // Act
            var value = proxyFactory.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyClassProxyTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<IntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyFactory.CreateProxy(new TargetInterceptor(_ => null));

            // Act
            var value = proxyFactory.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyDelegateProxyTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = proxyFactory.CreateProxy(new TargetInterceptor(_ => null));

            // Act
            var value = proxyFactory.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyInterfaceProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<IIntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyFactory.CreateProxy(new TargetInterceptor(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyFactory.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyAbstractClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<IntParameterBase>(new[] {typeof (IStringParameter)});
            var proxy = proxyFactory.CreateProxy(new TargetInterceptor(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyFactory.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<IntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyFactory.CreateProxy(new TargetInterceptor(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyFactory.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyDelegateProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = proxyFactory.CreateProxy(new TargetInterceptor(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyFactory.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyNonProxyTest()
        {
            // Arrange
            var proxyFactory = _proxyRepository.GetProxyFactory<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = new StringParameter();

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyFactory.AdaptProxy<IStringParameter>(proxy));
        }
    }
}