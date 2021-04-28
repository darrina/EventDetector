using EventDetector.Interfaces;
using EventDetector.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace EventDetector.Services
{
    public abstract class BaseEventDetectorService : IEventDetectorService
    {
        protected readonly ILogger logger;

        protected abstract bool ValidateOrder(IEnumerable<Event> events);

        protected BaseEventDetectorService(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsOrdered(IEnumerable<Event> events)
        {
            return
                Validate(events) &&
                ValidateOrder(events);
        }

        private bool Validate(IEnumerable<Event> events)
        {
            var isValid = true;

            if ((events?.Count() ?? 0) == 0)
            {
                logger.LogError("Event is null or empty");
                isValid = false;
            }

            return isValid;
        }
    }
}