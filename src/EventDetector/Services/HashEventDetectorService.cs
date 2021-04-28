using EventDetector.Interfaces;
using EventDetector.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace EventDetector.Services
{
    public class HashEventDetectorService : IEventDetectorService
    {
        private readonly HashSet<int> publishedEvents = new();
        private readonly ILogger logger;

        public HashEventDetectorService(ILogger<HashEventDetectorService> logger)
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

            return events.All(@event =>
                @event switch
                {
                    Event e when e.Type == EventTypes.Publish
                        => publishedEvents.Add(e.Group),
                    Event e when e.Type == EventTypes.Deliver
                        => publishedEvents.Contains(e.Group),

                    _ => false
                }
            );
        }
    }
}