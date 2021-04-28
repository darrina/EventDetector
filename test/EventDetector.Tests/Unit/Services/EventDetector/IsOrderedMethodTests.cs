using EventDetector.Extensions;
using EventDetector.Interfaces;
using EventDetector.Models;
using EventDetector.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EventDetector.Tests.Unit.Services.EventDetectorService
{
    [Trait("Category", "Unit")]
    public class IsOrderedMethodTests
    {
        private readonly IEnumerable<IEventDetectorService> sut;

        public IsOrderedMethodTests()
        {
            sut = Startup.ConfigureServices(Startup.BuildConfiguration())
                .GetServices<IEventDetectorService>();
        }

        [Theory]
        [InlineData(typeof(HashEventDetectorService))]
        [InlineData(typeof(LinqEventDetectorService))]
        public void GivenValidOrderedEvents_ItShouldReturnTrue(Type implementation)
        {
            var result = sut
                .First(s => s.GetType() == implementation)
                .IsOrdered(new[] { "P11", "D11", "P22", "D22" }.ToEvents());

            Assert.True(result);
        }

        [Theory]
        [InlineData(typeof(HashEventDetectorService))]
        [InlineData(typeof(LinqEventDetectorService))]
        public void GivenValidUnorderEvents_ItShouldReturnFalse(Type implementation)
        {
            var result = sut
                .First(s => s.GetType() == implementation)
                .IsOrdered(new[] { "P11", "D11", "D22", "P22" }.ToEvents());

            Assert.False(result);
        }

        [Theory]
        [InlineData(typeof(HashEventDetectorService))]
        [InlineData(typeof(LinqEventDetectorService))]
        public void GivenNullEvents_ItShouldReturnFalse(Type implementation)
        {
            var result = sut
                .First(s => s.GetType() == implementation)
                .IsOrdered(null);

            Assert.False(result);
        }

        [Theory]
        [InlineData(typeof(HashEventDetectorService))]
        [InlineData(typeof(LinqEventDetectorService))]
        public void GivenNoEvents_ItShouldReturnFalse(Type implementation)
        {
            var result = sut
                .First(s => s.GetType() == implementation)
                .IsOrdered(Array.Empty<Event>());

            Assert.False(result);
        }

        [Theory]
        [InlineData(typeof(HashEventDetectorService))]
        [InlineData(typeof(LinqEventDetectorService))]
        public void GivenEmptyEventData_ItShouldThrowAnArgumentException(Type implementation)
        {
            var emptyEventData = "";

            Assert.Throws<ArgumentException>(() => sut
                .First(s => s.GetType() == implementation)
                .IsOrdered(new[] { emptyEventData }.ToEvents()));
        }

        [Theory]
        [InlineData(typeof(HashEventDetectorService))]
        [InlineData(typeof(LinqEventDetectorService))]
        public void GivenInvalidEventData_ItShouldThrowAnArgumentException(Type implementation)
        {
            var invalidEventData = "P";

            Assert.Throws<ArgumentException>(() => sut
                .First(s => s.GetType() == implementation)
                .IsOrdered(new[] { invalidEventData }.ToEvents()));
        }

        [Theory]
        [InlineData(typeof(HashEventDetectorService))]
        [InlineData(typeof(LinqEventDetectorService))]
        public void GivenInvalidEventType_ItShouldThrowAnArgumentException(Type implementation)
        {
            var invalidEventType = "X1";

            Assert.Throws<ArgumentException>(() => sut
                .First(s => s.GetType() == implementation)
                .IsOrdered(new[] { invalidEventType }.ToEvents()));
        }

        [Theory]
        [InlineData(typeof(HashEventDetectorService))]
        [InlineData(typeof(LinqEventDetectorService))]
        public void GivenInvalidEventGroup_ItShouldThrowAnArgumentException(Type implementation)
        {
            var invalidEventGroup = "Pd";
            var exception = Assert.Throws<ArgumentException>(() => sut
                .First(s => s.GetType() == implementation)
                .IsOrdered(new[] { invalidEventGroup }.ToEvents()));
        }
    }
}
