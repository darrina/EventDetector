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
            if (eventData?.Length != 2)
            {
                throw new ArgumentException($"eventData must contain 2 characters: {eventData}", nameof(eventData));
            }

            Type = (eventData?[0]) switch
            {
                char c when c == 'P' => EventTypes.Publish,
                char c when c == 'D' => EventTypes.Deliver,

                _ => throw new ArgumentException(
                    $"{nameof(eventData)} first character must be P or D, found: {eventData}",
                    nameof(eventData))
            };

            Group = (eventData?[1]) switch
            {
                char c when int.TryParse($"{c}", out int group) => group,

                _ => throw new ArgumentException(
                    $"{nameof(eventData)} second character must be a valid integer, found: {eventData}",
                    nameof(eventData))
            };
        }
    }
}