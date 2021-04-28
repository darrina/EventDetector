using System;

namespace EventDetector.Models
{
    public enum EventTypes
    {
        Publish,
        Deliver
    }

    public class Event
    {
        public EventTypes Type { get; }
        public int Group { get; }

        public Event(string eventData)
        {
            if (string.IsNullOrEmpty(eventData))
            {
                throw new ArgumentException($"must not be null or empty", nameof(eventData));
            }

            if (eventData.Length < 2)
            {
                throw new ArgumentException($"must be at least 2 characters: {eventData}", nameof(eventData));
            }

            Type = eventData[0] switch
            {
                char c when c == 'P' => EventTypes.Publish,
                char c when c == 'D' => EventTypes.Deliver,

                _ => throw new ArgumentException(
                    $"prefix character '{eventData[0]}' must be one of: 'P','D'",
                    nameof(eventData))
            };

            Group = eventData[1..] switch
            {
                string s when int.TryParse(s, out int group) => group,

                _ => throw new ArgumentException(
                    $"suffix must be an integer: {eventData[1..]}",
                    nameof(eventData))
            };
        }
    }
}