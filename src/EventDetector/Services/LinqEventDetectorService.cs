using EventDetector.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace EventDetector.Services
{
    public class LinqEventDetectorService : BaseEventDetectorService
    {
        public LinqEventDetectorService(ILogger<LinqEventDetectorService> logger)
            : base(logger) {}

        protected override bool ValidateOrder(IEnumerable<Event> events) => events
            .GroupBy(e => e.Group, e => e.Type)
            .All(g => g.FirstOrDefault() == EventTypes.Publish);
    }
}