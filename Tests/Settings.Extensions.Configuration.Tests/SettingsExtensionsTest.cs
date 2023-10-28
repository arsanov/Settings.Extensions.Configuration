using Xunit;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Settings.Extensions.Configuration;
using FluentAssertions;

namespace Tests
{
    public class SettingsExtensionsTest
    {
        public class TestSettings
        {
            public string P1 { get; set; }
            public int? MissingValue { get; set; }

            public NestedType Nested { get; set; }


            public class NestedType
            {
                public int N1 { get; set; }
            }
        }

        [Fact]
        public void Test1()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                { "TestSettings:P1", "MyValue" },
                { "TestSettings:Nested:N1", "12" },
            });

            var configuration = configurationBuilder.Build();

            var services = new ServiceCollection();

            services.AddSettings<TestSettings>(configuration);

            var provider = services.BuildServiceProvider();

            var settings = provider.GetRequiredService<TestSettings>();

            settings.P1.Should().Be("MyValue");
            settings.Nested.N1.Should().Be(12);
            settings.MissingValue.Should().BeNull();
        }
    }
}