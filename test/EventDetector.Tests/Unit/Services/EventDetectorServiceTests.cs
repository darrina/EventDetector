using EventDetector.Extensions;
using EventDetector.Interfaces;
using EventDetector.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace EventDetector.Tests.Unit.Services
{
    [Trait("Category", "Unit")]
    public class EventDetectorServiceTests
    {
        private readonly IEventDetectorService sut;

        public EventDetectorServiceTests()
        {
            sut = Startup.ConfigureServices(Startup.BuildConfiguration())
                .GetService<IEventDetectorService>();
        }

        [Fact]
        public void GivenNullEvents_ItShouldReturnFalse()
        {
            var result = sut.IsOrdered(null);

            Assert.False(result);
        }

        [Fact]
        public void GivenNoEvents_ItShouldReturnFalse()
        {
            var result = sut.IsOrdered(Array.Empty<Event>());

            Assert.False(result);
        }

        [Fact]
        public void GivenOrderedEvents_ItShouldReturnTrue()
        {
            var result = sut.IsOrdered(new[] { "P1", "D1", "P2", "D2" }.ToEvents());

            Assert.True(result);
        }

        [Fact]
        public void GivenOutOfOrderEvents_ItShouldReturnFalse()
        {
            var result = sut.IsOrdered(new[] { "P1", "D1", "D2", "P2" }.ToEvents());

            Assert.False(result);
        }
    }
}
