using EventDetector.Interfaces;
using EventDetector.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace EventDetector.Services
{
    public class LinqEventDetectorService : IEventDetectorService
    {
        private readonly ILogger logger;

        public LinqEventDetectorService(ILogger<HashEventDetectorService> logger)
        {
            this.logger = logger;
        }

        public bool IsOrdered(IEnumerable<Event> events)
        {
            if ((events?.Count() ?? 0) == 0)
            {
                logger.LogError("Event is null or empty");
                return false;
            }

            return events
                .GroupBy(e => e.Group, e => e.Type)
                .All(g => g.FirstOrDefault() == EventTypes.Publish);
        }
    }
}