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
using System.Collections.Generic;
using System.Reflection;
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test
{
    [TestFixture]
    public sealed class ProxyRepositoryTestFixture
    {
        private ProxyTypeBuilderFactory _proxyTypeBuilderFactory;

        private ProxyRepository _proxyRepository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _proxyTypeBuilderFactory = new ProxyTypeBuilderFactory(true);
            _proxyRepository = new ProxyRepository(_proxyTypeBuilderFactory, DefaultInterceptionFilter.Instance);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _proxyTypeBuilderFactory.SaveAssembly("NProxy.Dynamic.dll");
        }

        #region Interface Tests

        [Test]
        public void CreateProxyFromInterfaceWithEnumArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IEnumArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {EnumType.Two});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IEnumParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(EnumType.Two);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {"Two"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericJaggedArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericJaggedArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {new[] {"Two"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRankArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRankArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[,] {{"Two", "Two"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericListParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericListParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new List<string> {"Two"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericParameter>(Type.EmptyTypes, interceptor);

            proxy.Method("Two");

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IIntArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {2});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IIntParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(2);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IStringArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {"2"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IStringParameter>(Type.EmptyTypes, interceptor);

            proxy.Method("2");

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IStructArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {new StructType {Integer = 2, String = "2"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IStructParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new StructType {Integer = 2, String = "2"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IEnumArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {EnumType.Two};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IEnumRefParameter>(Type.EmptyTypes, interceptor);
            var value = EnumType.Two;

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericJaggedArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericJaggedArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {new[] {"Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRankArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRankArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[,] {{"Two", "Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericListRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericListRefParameter>(Type.EmptyTypes, interceptor);
            var value = new List<string> {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRefParameter>(Type.EmptyTypes, interceptor);
            var value = "Two";

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IIntArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {2};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IIntRefParameter>(Type.EmptyTypes, interceptor);
            var value = 2;

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IStringArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {"2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IStringRefParameter>(Type.EmptyTypes, interceptor);
            var value = "2";

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IStructArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {new StructType {Integer = 2, String = "2"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IStructRefParameter>(Type.EmptyTypes, interceptor);
            var value = new StructType {Integer = 2, String = "2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {EnumType.Two}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IEnumArrayOutParameter>(Type.EmptyTypes, interceptor);
            EnumType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {EnumType.Two}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IEnumOutParameter>(Type.EmptyTypes, interceptor);
            EnumType value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {"Two"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericArrayOutParameter>(Type.EmptyTypes, interceptor);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericJaggedArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {new[] {"Two"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericJaggedArrayOutParameter>(Type.EmptyTypes, interceptor);
            string[][] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRankArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[,] {{"Two", "Two"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRankArrayOutParameter>(Type.EmptyTypes, interceptor);
            string[,] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericListOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new List<string> {"Two"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericListOutParameter>(Type.EmptyTypes, interceptor);
            List<string> value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {"Two"}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericOutParameter>(Type.EmptyTypes, interceptor);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {2}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IIntArrayOutParameter>(Type.EmptyTypes, interceptor);
            int[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {2}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IIntOutParameter>(Type.EmptyTypes, interceptor);
            int value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {"2"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IStringArrayOutParameter>(Type.EmptyTypes, interceptor);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {"2"}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IStringOutParameter>(Type.EmptyTypes, interceptor);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {new StructType {Integer = 2, String = "2"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IStructArrayOutParameter>(Type.EmptyTypes, interceptor);
            StructType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new StructType {Integer = 2, String = "2"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IStructOutParameter>(Type.EmptyTypes, interceptor);
            StructType value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {EnumType.Two});

            // Act
            var proxy = _proxyRepository.CreateProxy<IEnumArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(EnumType.Two);

            // Act
            var proxy = _proxyRepository.CreateProxy<IEnumReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {"Two"});

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericJaggedArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {new[] {"Two"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericJaggedArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRankJaggedArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[,] {{"Two", "Two"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRankArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericListReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new List<string> {"Two"});

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericListReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor("Two");

            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {2});

            // Act
            var proxy = _proxyRepository.CreateProxy<IIntArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(2);

            // Act
            var proxy = _proxyRepository.CreateProxy<IIntReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {"2"});

            // Act
            var proxy = _proxyRepository.CreateProxy<IStringArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor("2");

            // Act
            var proxy = _proxyRepository.CreateProxy<IStringReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {new StructType {Integer = 2, String = "2"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<IStructArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new StructType {Integer = 2, String = "2"});

            // Act
            var proxy = _proxyRepository.CreateProxy<IStructReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithVoidReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(null);

            // Act
            var proxy = _proxyRepository.CreateProxy<IVoidReturnValue>(Type.EmptyTypes, interceptor);

            proxy.Method();
        }

        #endregion

        #region Abstract Class Tests

        [Test]
        public void CreateProxyFromAbstractClassWithEnumArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumArrayParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {EnumType.Two});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(EnumType.Two);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericArrayParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {"Two"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericJaggedArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericJaggedArrayParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {new[] {"Two"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRankArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRankArrayParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new[,] {{"Two", "Two"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericListParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericListParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new List<string> {"Two"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method("Two");

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IntArrayParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {2});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IntParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(2);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StringArrayParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {"2"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StringParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method("2");

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StructArrayParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {new StructType {Integer = 2, String = "2"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StructParameterBase>(Type.EmptyTypes, interceptor);

            proxy.Method(new StructType {Integer = 2, String = "2"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumArrayRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new[] {EnumType.Two};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = EnumType.Two;

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericArrayRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new[] {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericJaggedArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericJaggedArrayRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new[] {new[] {"Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRankArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRankArrayRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new[,] {{"Two", "Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericListRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericListRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new List<string> {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = "Two";

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IntArrayRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new[] {2};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IntRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = 2;

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StringArrayRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new[] {"2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StringRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = "2";

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StructArrayRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new[] {new StructType {Integer = 2, String = "2"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StructRefParameterBase>(Type.EmptyTypes, interceptor);
            var value = new StructType {Integer = 2, String = "2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {EnumType.Two}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumArrayOutParameterBase>(Type.EmptyTypes, interceptor);
            EnumType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {EnumType.Two}};

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumOutParameterBase>(Type.EmptyTypes, interceptor);
            EnumType value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {"Two"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericArrayOutParameterBase>(Type.EmptyTypes, interceptor);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericJaggedArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {new[] {"Two"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericJaggedArrayOutParameterBase>(Type.EmptyTypes, interceptor);
            string[][] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRankArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[,] {{"Two", "Two"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRankArrayOutParameterBase>(Type.EmptyTypes, interceptor);
            string[,] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericListOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new List<string> {"Two"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericListOutParameterBase>(Type.EmptyTypes, interceptor);
            List<string> value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {"Two"}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericOutParameterBase>(Type.EmptyTypes, interceptor);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {2}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IntArrayOutParameterBase>(Type.EmptyTypes, interceptor);
            int[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {2}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IntOutParameterBase>(Type.EmptyTypes, interceptor);
            int value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {"2"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<StringArrayOutParameterBase>(Type.EmptyTypes, interceptor);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {"2"}};

            // Act
            var proxy = _proxyRepository.CreateProxy<StringOutParameterBase>(Type.EmptyTypes, interceptor);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {new StructType {Integer = 2, String = "2"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<StructArrayOutParameterBase>(Type.EmptyTypes, interceptor);
            StructType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new StructType {Integer = 2, String = "2"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<StructOutParameterBase>(Type.EmptyTypes, interceptor);
            StructType value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {EnumType.Two});

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumArrayReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(EnumType.Two);

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {"Two"});

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericArrayReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericJaggedArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {new[] {"Two"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericJaggedArrayReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRankArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[,] {{"Two", "Two"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRankArrayReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericListReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new List<string> {"Two"});

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericListReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor("Two");

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {2});

            // Act
            var proxy = _proxyRepository.CreateProxy<IntArrayReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(2);

            // Act
            var proxy = _proxyRepository.CreateProxy<IntReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {"2"});

            // Act
            var proxy = _proxyRepository.CreateProxy<StringArrayReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor("2");

            // Act
            var proxy = _proxyRepository.CreateProxy<StringReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {new StructType {Integer = 2, String = "2"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<StructArrayReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new StructType {Integer = 2, String = "2"});

            // Act
            var proxy = _proxyRepository.CreateProxy<StructReturnValueBase>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithVoidReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(null);

            // Act
            var proxy = _proxyRepository.CreateProxy<VoidReturnValueBase>(Type.EmptyTypes, interceptor);

            proxy.Method();
        }

        #endregion

        #region Class Tests

        [Test]
        public void CreateProxyFromClassWithEnumArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {EnumType.Two});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(EnumType.Two);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromClassWithGenericArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {"Two"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericJaggedArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericJaggedArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {new[] {"Two"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRankArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRankArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[,] {{"Two", "Two"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericListParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericListParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new List<string> {"Two"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericParameter>(Type.EmptyTypes, interceptor);

            proxy.Method("Two");

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromClassWithIntArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IntArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {2});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromClassWithIntParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IntParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(2);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromClassWithStringArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StringArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {"2"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromClassWithStringParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StringParameter>(Type.EmptyTypes, interceptor);

            proxy.Method("2");

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromClassWithStructArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StructArrayParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new[] {new StructType {Integer = 2, String = "2"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromClassWithStructParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StructParameter>(Type.EmptyTypes, interceptor);

            proxy.Method(new StructType {Integer = 2, String = "2"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {EnumType.Two};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumRefParameter>(Type.EmptyTypes, interceptor);
            var value = EnumType.Two;

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromClassWithGenericArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericJaggedArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericJaggedArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {new[] {"Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRankArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRankArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[,] {{"Two", "Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericListRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericListRefParameter>(Type.EmptyTypes, interceptor);
            var value = new List<string> {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRefParameter>(Type.EmptyTypes, interceptor);
            var value = "Two";

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromClassWithIntArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IntArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {2};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromClassWithIntRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<IntRefParameter>(Type.EmptyTypes, interceptor);
            var value = 2;

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromClassWithStringArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StringArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {"2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromClassWithStringRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StringRefParameter>(Type.EmptyTypes, interceptor);
            var value = "2";

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromClassWithStructArrayRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StructArrayRefParameter>(Type.EmptyTypes, interceptor);
            var value = new[] {new StructType {Integer = 2, String = "2"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromClassWithStructRefParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<StructRefParameter>(Type.EmptyTypes, interceptor);
            var value = new StructType {Integer = 2, String = "2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {EnumType.Two}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumArrayOutParameter>(Type.EmptyTypes, interceptor);
            EnumType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {EnumType.Two}};

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumOutParameter>(Type.EmptyTypes, interceptor);
            EnumType value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromClassWithGenericArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {"Two"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericArrayOutParameter>(Type.EmptyTypes, interceptor);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericJaggedArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {new[] {"Two"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericJaggedArrayOutParameter>(Type.EmptyTypes, interceptor);
            string[][] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRankArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[,] {{"Two", "Two"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRankArrayOutParameter>(Type.EmptyTypes, interceptor);
            string[,] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericListOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new List<string> {"Two"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericListOutParameter>(Type.EmptyTypes, interceptor);
            List<string> value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {"Two"}};

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericOutParameter>(Type.EmptyTypes, interceptor);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromClassWithIntArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {2}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IntArrayOutParameter>(Type.EmptyTypes, interceptor);
            int[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromClassWithIntOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {2}};

            // Act
            var proxy = _proxyRepository.CreateProxy<IntOutParameter>(Type.EmptyTypes, interceptor);
            int value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromClassWithStringArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {"2"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<StringArrayOutParameter>(Type.EmptyTypes, interceptor);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromClassWithStringOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {"2"}};

            // Act
            var proxy = _proxyRepository.CreateProxy<StringOutParameter>(Type.EmptyTypes, interceptor);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromClassWithStructArrayOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new[] {new StructType {Integer = 2, String = "2"}}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<StructArrayOutParameter>(Type.EmptyTypes, interceptor);
            StructType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromClassWithStructOutParameterTest()
        {
            // Arrange
            var interceptor = new SetParametersMethodInterceptor {Parameters = new object[] {new StructType {Integer = 2, String = "2"}}};

            // Act
            var proxy = _proxyRepository.CreateProxy<StructOutParameter>(Type.EmptyTypes, interceptor);
            StructType value;

            proxy.Method(out value);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {EnumType.Two});

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(EnumType.Two);

            // Act
            var proxy = _proxyRepository.CreateProxy<EnumReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromClassWithGenericArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {"Two"});

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericJaggedArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {new[] {"Two"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericJaggedArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRankArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[,] {{"Two", "Two"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericRankArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericListReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new List<string> {"Two"});

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericListReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor("Two");

            // Act
            var proxy = _proxyRepository.CreateProxy<GenericReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromClassWithIntArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {2});

            // Act
            var proxy = _proxyRepository.CreateProxy<IntArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromClassWithIntReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(2);

            // Act
            var proxy = _proxyRepository.CreateProxy<IntReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromClassWithStringArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {"2"});

            // Act
            var proxy = _proxyRepository.CreateProxy<StringArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromClassWithStringReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor("2");

            // Act
            var proxy = _proxyRepository.CreateProxy<StringReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromClassWithStructArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {new StructType {Integer = 2, String = "2"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<StructArrayReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromClassWithStructReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new StructType {Integer = 2, String = "2"});

            // Act
            var proxy = _proxyRepository.CreateProxy<StructReturnValue>(Type.EmptyTypes, interceptor);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromClassWithVoidReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(null);

            // Act
            var proxy = _proxyRepository.CreateProxy<VoidReturnValue>(Type.EmptyTypes, interceptor);

            proxy.Method();
        }

        #endregion

        #region Delegate Tests

        [Test]
        public void CreateProxyFromDelegateWithEnumArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<Action<EnumType[]>>(Type.EmptyTypes, interceptor);

            proxy(new[] {EnumType.Two});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromDelegateWithEnumParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<Action<EnumType>>(Type.EmptyTypes, interceptor);

            proxy(EnumType.Two);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromDelegateWithIntArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<Action<int[]>>(Type.EmptyTypes, interceptor);

            proxy(new[] {2});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromDelegateWithIntParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<Action<int>>(Type.EmptyTypes, interceptor);

            proxy(2);

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromDelegateWithStringArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<Action<string[]>>(Type.EmptyTypes, interceptor);

            proxy(new[] {"2"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromDelegateWithStringParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<Action<string>>(Type.EmptyTypes, interceptor);

            proxy("2");

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromDelegateWithStructArrayParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<Action<StructType[]>>(Type.EmptyTypes, interceptor);

            proxy(new[] {new StructType {Integer = 2, String = "2"}});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromDelegateWithStructParameterTest()
        {
            // Arrange
            var interceptor = new GetParametersInterceptor();

            // Act
            var proxy = _proxyRepository.CreateProxy<Action<StructType>>(Type.EmptyTypes, interceptor);

            proxy(new StructType {Integer = 2, String = "2"});

            // Assert
            Assert.That(interceptor.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromDelegateWithEnumArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {EnumType.Two});

            // Act
            var proxy = _proxyRepository.CreateProxy<Func<EnumType[]>>(Type.EmptyTypes, interceptor);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromDelegateWithEnumReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(EnumType.Two);

            // Act
            var proxy = _proxyRepository.CreateProxy<Func<EnumType>>(Type.EmptyTypes, interceptor);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromDelegateWithIntArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {2});

            // Act
            var proxy = _proxyRepository.CreateProxy<Func<int[]>>(Type.EmptyTypes, interceptor);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromDelegateWithIntReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(2);

            // Act
            var proxy = _proxyRepository.CreateProxy<Func<int>>(Type.EmptyTypes, interceptor);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromDelegateWithStringArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {"2"});

            // Act
            var proxy = _proxyRepository.CreateProxy<Func<string[]>>(Type.EmptyTypes, interceptor);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromDelegateWithStringReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor("2");

            // Act
            var proxy = _proxyRepository.CreateProxy<Func<string>>(Type.EmptyTypes, interceptor);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromDelegateWithStructArrayReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new[] {new StructType {Integer = 2, String = "2"}});

            // Act
            var proxy = _proxyRepository.CreateProxy<Func<StructType[]>>(Type.EmptyTypes, interceptor);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromDelegateWithStructReturnValueTest()
        {
            // Arrange
            var interceptor = new SetReturnValueInterceptor(new StructType {Integer = 2, String = "2"});

            // Act
            var proxy = _proxyRepository.CreateProxy<Func<StructType>>(Type.EmptyTypes, interceptor);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        #endregion

        #region Target Object Tests

        [Test]
        public void CreateProxyFromInterfaceAndNullTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IIntParameter>(Type.EmptyTypes, new TargetInterceptor(_ => null));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromInterfaceAndInvalidTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IIntParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new StringParameter()));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromInterfaceAndProxyTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IIntParameter>(Type.EmptyTypes, new TargetInterceptor(p => p));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromAbstractClassAndNullTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IntParameterBase>(Type.EmptyTypes, new TargetInterceptor(p => null));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromAbstractClassAndInvalidTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IntParameterBase>(Type.EmptyTypes, new TargetInterceptor(p => new StringParameter()));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromAbstractClassAndProxyTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IntParameterBase>(Type.EmptyTypes, new TargetInterceptor(p => p));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromClassAndNullTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IntParameter>(Type.EmptyTypes, new TargetInterceptor(p => null));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromClassAndInvalidTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IntParameter>(Type.EmptyTypes, new TargetInterceptor(p => new StringParameter()));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromClassAndProxyTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IntParameter>(Type.EmptyTypes, new TargetInterceptor(p => p));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromDelegateAndNullTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<Action<int>>(Type.EmptyTypes, new TargetInterceptor(p => null));

            // Assert
            Assert.Throws<TargetException>(() => proxy(default(int)));
        }

        [Test]
        public void CreateProxyFromDelegateAndInvalidTargetTest()
        {
            // Arrange
            // Act
            Action<string> target = s => { };
            var proxy = _proxyRepository.CreateProxy<Action<int>>(Type.EmptyTypes, new TargetInterceptor(p => target));

            // Assert
            Assert.Throws<TargetException>(() => proxy(default(int)));
        }

        [Test]
        public void CreateProxyFromDelegateAndProxyTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<Action<int>>(Type.EmptyTypes, new TargetInterceptor(p => p));

            // Assert
            Assert.Throws<TargetException>(() => proxy(default(int)));
        }

        [Test]
        public void CreateProxyFromDelegateAndTargetTest()
        {
            // Arrange
            // Act
            Action<int> target = i => { };
            var proxy = _proxyRepository.CreateProxy<Action<int>>(Type.EmptyTypes, new TargetInterceptor(p => target));

            // Assert
            Assert.DoesNotThrow(() => proxy(default(int)));
        }

        [Test]
        public void CreateProxyFromInterfaceAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IIntParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new IntParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericArrayParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericArrayParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericArrayParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(new int[0]));
        }

        [Test]
        public void CreateProxyWithGenericJaggedArrayParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericJaggedArrayParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericJaggedArrayParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(new int[0][]));
        }

        [Test]
        public void CreateProxyWithGenericRankArrayParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRankArrayParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericRankArrayParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(new int[0, 0]));
        }

        [Test]
        public void CreateProxyWithGenericListParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericListParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericListParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(new List<int>()));
        }

        [Test]
        public void CreateProxyWithGenericParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericArrayRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericArrayRefParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericArrayRefParameter()));
            var value = new int[0];

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0]));
        }

        [Test]
        public void CreateProxyWithGenericJaggedArrayRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericJaggedArrayRefParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericJaggedArrayRefParameter()));
            var value = new int[0][];

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0][]));
        }

        [Test]
        public void CreateProxyWithGenericRankArrayRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRankArrayRefParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericRankArrayRefParameter()));
            var value = new int[0, 0];

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0, 0]));
        }

        [Test]
        public void CreateProxyWithGenericListRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericListRefParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericListRefParameter()));
            var value = new List<int>();

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(new List<int>()));
        }

        [Test]
        public void CreateProxyWithGenericRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRefParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericRefParameter()));
            int value = default(int);

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericArrayOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericArrayOutParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericArrayOutParameter()));
            int[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0]));
        }

        [Test]
        public void CreateProxyWithGenericJaggedArrayOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericJaggedArrayOutParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericJaggedArrayOutParameter()));
            int[][] value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0][]));
        }

        [Test]
        public void CreateProxyWithGenericRankArrayOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRankArrayOutParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericRankArrayOutParameter()));
            int[,] value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0, 0]));
        }

        [Test]
        public void CreateProxyWithGenericListOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericListOutParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericListOutParameter()));
            List<int> value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(new List<int>()));
        }

        [Test]
        public void CreateProxyWithGenericOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericOutParameter>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericOutParameter()));
            int value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericArrayReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericArrayReturnValue>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericArrayReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(new int[0]));
        }

        [Test]
        public void CreateProxyWithGenericJaggedArrayReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericJaggedArrayReturnValue>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericJaggedArrayReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(new int[0][]));
        }

        [Test]
        public void CreateProxyWithGenericRankArrayReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericRankArrayReturnValue>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericRankArrayReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(new int[0, 0]));
        }

        [Test]
        public void CreateProxyWithGenericListReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericListReturnValue>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericListReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(new List<int>()));
        }

        [Test]
        public void CreateProxyWithGenericReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericReturnValue>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericInterfaceAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGeneric<int>>(Type.EmptyTypes, new TargetInterceptor(_ => new Generic<int>()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithTwoGenericEventInterfacesAndTargetTest()
        {
            // Arrange
            // Act
            var target = new IntStringGenericEvent();
            var proxy = _proxyRepository.CreateProxy<IGenericEvent<int>>(new[] {typeof (IGenericEvent<string>)}, new TargetInterceptor(_ => target));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IGenericEvent<int>>());
            Assert.That(proxy, Is.InstanceOf<IGenericEvent<string>>());

            proxy.Event += i => { };
            ((IGenericEvent<string>) proxy).Event += s => { };
        }

        [Test]
        public void CreateProxyWithTwoAdditionalGenericEventInterfacesAndTargetTest()
        {
            // Arrange
            // Act
            var target = new IntStringGenericEvent();
            var proxy = _proxyRepository.CreateProxy<object>(new[] {typeof (IGenericEvent<int>), typeof (IGenericEvent<string>)}, new TargetInterceptor(_ => target));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IGenericEvent<int>>());
            Assert.That(proxy, Is.InstanceOf<IGenericEvent<string>>());

            ((IGenericEvent<int>) proxy).Event += i => { };
            ((IGenericEvent<string>) proxy).Event += s => { };
        }

        [Test]
        public void CreateProxyWithTwoGenericPropertyInterfacesAndTargetTest()
        {
            // Arrange
            // Act
            var target = new IntStringGenericProperty();
            var proxy = _proxyRepository.CreateProxy<IGenericProperty<int>>(new[] {typeof (IGenericProperty<string>)}, new TargetInterceptor(_ => target));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IGenericProperty<int>>());
            Assert.That(proxy, Is.InstanceOf<IGenericProperty<string>>());

            proxy.Property = 2;
            ((IGenericProperty<string>) proxy).Property = "2";

            Assert.That(proxy.Property, Is.EqualTo(2));
            Assert.That(((IGenericProperty<string>) proxy).Property, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyWithTwoAdditionalGenericPropertyInterfacesAndTargetTest()
        {
            // Arrange
            // Act
            var target = new IntStringGenericProperty();
            var proxy = _proxyRepository.CreateProxy<object>(new[] {typeof (IGenericProperty<int>), typeof (IGenericProperty<string>)}, new TargetInterceptor(_ => target));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IGenericProperty<int>>());
            Assert.That(proxy, Is.InstanceOf<IGenericProperty<string>>());

            ((IGenericProperty<int>) proxy).Property = 2;
            ((IGenericProperty<string>) proxy).Property = "2";

            Assert.That(((IGenericProperty<int>) proxy).Property, Is.EqualTo(2));
            Assert.That(((IGenericProperty<string>) proxy).Property, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyWithTwoGenericMethodInterfacesAndTargetTest()
        {
            // Arrange
            // Act
            var target = new IntStringGenericMethod();
            var proxy = _proxyRepository.CreateProxy<IGenericMethod<int>>(new[] {typeof (IGenericMethod<string>)}, new TargetInterceptor(_ => target));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IGenericMethod<int>>());
            Assert.That(proxy, Is.InstanceOf<IGenericMethod<string>>());

            Assert.DoesNotThrow(() => proxy.Method());
            Assert.DoesNotThrow(() => ((IGenericMethod<string>) proxy).Method());
        }

        [Test]
        public void CreateProxyWithTwoAdditionalGenericMethodInterfacesAndTargetTest()
        {
            // Arrange
            // Act
            var target = new IntStringGenericMethod();
            var proxy = _proxyRepository.CreateProxy<object>(new[] {typeof (IGenericMethod<int>), typeof (IGenericMethod<string>)}, new TargetInterceptor(_ => target));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IGenericMethod<int>>());
            Assert.That(proxy, Is.InstanceOf<IGenericMethod<string>>());

            Assert.DoesNotThrow(() => ((IGenericMethod<int>) proxy).Method());
            Assert.DoesNotThrow(() => ((IGenericMethod<string>) proxy).Method());
        }

        [Test]
        public void CreateProxyWithGenericAbstractClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<GenericBase<int>>(Type.EmptyTypes, new TargetInterceptor(_ => new Generic<int>()));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<Generic<int>>(Type.EmptyTypes, new TargetInterceptor(_ => new Generic<int>()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericParameterInterfaceAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IGenericParameter<string>>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericParameter<string>()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(string), default(int)));
        }

        [Test]
        public void CreateProxyWithGenericParameterAbstractClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<GenericParameterBase<string>>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericParameter<string>()));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(string), default(int)));
        }

        [Test]
        public void CreateProxyWithGenericParameterClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<GenericParameter<string>>(Type.EmptyTypes, new TargetInterceptor(_ => new GenericParameter<string>()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(string), default(int)));
        }

        #endregion

        #region Additional Interface Tests

        [Test]
        public void CreateProxyWithAdditionalInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IBase>(new[] {typeof (IOther)}, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IOther>());
        }

        [Test]
        public void CreateProxyWithExtendedInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IBase>(new[] {typeof (IOne)}, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IOne>());
        }

        [Test]
        public void CreateProxyWithPartialInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IOne>(new[] {typeof (IBase)}, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IOne>());
            Assert.That(proxy, Is.InstanceOf<IBase>());
        }

        [Test]
        public void CreateProxyWithEquivalentInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IBase>(new[] {typeof (IBase)}, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
        }

        [Test]
        public void CreateProxyWithSimilarInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IOne>(new[] {typeof (ITwo)}, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IOne>());
            Assert.That(proxy, Is.InstanceOf<ITwo>());
        }

        [Test]
        public void CreateProxyWithDuplicateInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IOneTwo>(new[] {typeof (IOne), typeof (ITwo)}, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IOne>());
            Assert.That(proxy, Is.InstanceOf<ITwo>());
            Assert.That(proxy, Is.InstanceOf<IOneTwo>());
        }

        [Test]
        public void CreateProxyWithHiddenInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IBase>(new[] {typeof (IHideBase)}, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IHideBase>());
        }

        [Test]
        public void CreateProxyWithNestedGenericInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IBase>(new[] {typeof (Class<int>.INested<string>), typeof (Class<string>.INested<int>)}, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<Class<int>.INested<string>>());
            Assert.That(proxy, Is.InstanceOf<Class<string>.INested<int>>());
        }

        [Test]
        public void CreateProxyWithoutAdditionalInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyRepository.CreateProxy<IBase>(Type.EmptyTypes, new TargetInterceptor(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
        }

        #endregion
    }
}