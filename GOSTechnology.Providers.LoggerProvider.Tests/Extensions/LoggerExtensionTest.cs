using FluentAssertions;
using GOSTechnology.Providers.LoggerProvider.LIB;
using NUnit.Framework;
using Serilog;
using System;

namespace GOSTechnology.Providers.LoggerProvider.Tests
{
    /// <summary>
    /// LoggerExtensionTest.
    /// </summary>
    [TestFixture]
    public class LoggerExtensionTest
    {
        /// <summary>
        /// ShouldSuccessConfigureElasticsearch.
        /// </summary>
        [Test]
        public void ShouldSuccessConfigureElasticsearch()
        {
            ILogger result = LoggerExtension.ConfigureElasticsearch();
            result.Should().NotBeNull();
        }

        /// <summary>
        /// ShouldSuccessGetEnvironmentVariable.
        /// </summary>
        [Test]
        public void ShouldSuccessGetEnvironmentVariable()
        {
            String result = LoggerExtension.GetEnvironmentVariable();
            result.Should().NotBeNullOrWhiteSpace();
        }

        /// <summary>
        /// ShouldSuccessGetElasticsearchConnectionString.
        /// </summary>
        [Test]
        public void ShouldSuccessGetElasticsearchConnectionString()
        {
            String result = LoggerExtension.GetUriElasticsearch();
            result.Should().NotBeNullOrWhiteSpace();
        }

        /// <summary>
        /// ShouldSuccessGetDate.
        /// </summary>
        [Test]
        public void ShouldSuccessGetDate()
        {
            DateTime result = LoggerExtension.GetDate();
            result.Should().BeAfter(DateTime.MinValue);
        }
    }
}
