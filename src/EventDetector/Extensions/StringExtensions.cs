using EventDetector.Models;
using System.Collections.Generic;
using System.Linq;


namespace EventDetector.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<Event> ToEvents(this IEnumerable<string> eventData)
            => eventData.Select(d => new Event(d));
    }
}