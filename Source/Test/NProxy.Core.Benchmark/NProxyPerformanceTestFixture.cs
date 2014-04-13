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
using System.Diagnostics;
using System.Reflection;
using NProxy.Core.Benchmark.Reporting;
using NProxy.Core.Benchmark.Types;
using NUnit.Framework;

namespace NProxy.Core.Benchmark
{
    [TestFixture]
    public sealed class NProxyPerformanceTestFixture
    {
        private static readonly AssemblyName AssemblyName;

        static NProxyPerformanceTestFixture()
        {
            var type = typeof (ProxyRepository);

            AssemblyName = type.Assembly.GetName();
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Ensure all classes are loaded and initialized.
            var interceptor = new NProxyInterceptor(new Standard());
            var proxyFactory = new ProxyRepository();

            proxyFactory.CreateProxy<IStandard>(Type.EmptyTypes, interceptor);
        }

        [TestCase(1000)]
        public void ProxyGenerationTest(int iterations)
        {
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyFactory = new ProxyRepository();

                stopwatch.Start();

                proxyFactory.GetProxyFactory<IStandard>(Type.EmptyTypes);

                stopwatch.Stop();
            }

            Report.Instance.Write(AssemblyName, Scenario.ProxyGeneration, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000)]
        public void ProxyGenerationWithGenericParameterTest(int iterations)
        {
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyFactory = new ProxyRepository();

                stopwatch.Start();

                proxyFactory.GetProxyFactory<IGeneric>(Type.EmptyTypes);

                stopwatch.Stop();
            }

            Report.Instance.Write(AssemblyName, Scenario.ProxyGenerationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationTest(int iterations)
        {
            var interceptor = new NProxyInterceptor(new Standard());
            var proxyFactory = new ProxyRepository();
            var stopwatch = new Stopwatch();
            var proxyFactory = proxyFactory.GetProxyFactory<IStandard>(Type.EmptyTypes);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy(interceptor);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.ProxyInstantiation, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationWithGenericParameterTest(int iterations)
        {
            var interceptor = new NProxyInterceptor(new Generic());
            var proxyFactory = new ProxyRepository();
            var stopwatch = new Stopwatch();
            var proxyFactory = proxyFactory.GetProxyFactory<IGeneric>(Type.EmptyTypes);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy(interceptor);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.ProxyInstantiationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void MethodInvocationTest(int iterations)
        {
            var interceptor = new NProxyInterceptor(new Standard());
            var proxyFactory = new ProxyRepository();
            var proxy = proxyFactory.CreateProxy<IStandard>(Type.EmptyTypes, interceptor);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.MethodInvocation, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void MethodInvocationWithGenericParameterTest(int iterations)
        {
            var interceptor = new NProxyInterceptor(new Generic());
            var proxyFactory = new ProxyRepository();
            var proxy = proxyFactory.CreateProxy<IGeneric>(Type.EmptyTypes, interceptor);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.MethodInvocationWithGenericParameter, iterations, stopwatch.Elapsed);
        }
    }
}