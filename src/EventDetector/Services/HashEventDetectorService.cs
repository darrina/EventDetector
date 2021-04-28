using EventDetector.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace EventDetector.Services
{
    public class HashEventDetectorService : BaseEventDetectorService
    {
        private readonly HashSet<int> publishedEvents = new();

        public HashEventDetectorService(ILogger<HashEventDetectorService> logger)
            : base (logger) {}

        protected override bool ValidateOrder(IEnumerable<Event> events) => events
            .All(@event => @event switch
            {
                Event e when e.Type == EventTypes.Publish
                    => publishedEvents.Add(e.Group),
                Event e when e.Type == EventTypes.Deliver
                    => publishedEvents.Contains(e.Group),

                _ => false
            });
    }
}