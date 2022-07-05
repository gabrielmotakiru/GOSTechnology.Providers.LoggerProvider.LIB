using FluentAssertions;
using GOSTechnology.Providers.LoggerProvider.LIB;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace GOSTechnology.Providers.LoggerProvider.Tests
{
    /// <summary>
    /// DependencyInjectionTest.
    /// </summary>
    [TestFixture]
    public class DependencyInjectionTest
    {
        /// <summary>
        /// _builder.
        /// </summary>
        private IServiceCollection _builder;

        /// <summary>
        /// SetUp.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this._builder = new ServiceCollection();
        }

        /// <summary>
        /// ShouldSuccessAddLoggerProvider. 
        /// </summary>
        [Test]
        public void ShouldSuccessAddLoggerProvider()
        {
            var result = DependencyInjection.AddLoggerProvider(this._builder);
            result.Should().NotBeNull();
        }
    }
}
