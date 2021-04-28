using EventDetector.Models;
using System.Collections.Generic;


namespace EventDetector.Interfaces
{
    public interface IEventDetectorService
    {
        bool IsOrdered(IEnumerable<Event> events);
    }
}