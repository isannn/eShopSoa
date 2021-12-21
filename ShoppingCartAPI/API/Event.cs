using System;

namespace ShoppingCartAPI.API
{
    public class Event
    {
        public long SequenceNumber { get; set; }

        public Event(object sequenceNumber, in DateTimeOffset utcNow, string eventName, object content)
        {
            throw new NotImplementedException();
        }
    }
}