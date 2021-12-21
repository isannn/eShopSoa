using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartAPI.API
{
    public class EventStore : IEventStore
    {
        private EventStoreDb _database;
        public EventStore()
        {
            _database = new EventStoreDb();
        }
        public void Raise(string eventName, object content)
        {
            
            var sequenceNumber = _database.NextSequenceNumber();
            _database.Add(new Event(
                sequenceNumber,
                DateTimeOffset.UtcNow,
                eventName,
                content));
        }

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            return _database.Where(e =>
                e.SequenceNumber >= firstEventSequenceNumber && e.SequenceNumber <= lastEventSequenceNumber)
                .OrderBy(e => e.SequenceNumber);
        }
    }

    internal class EventStoreDb : List<Event>
    {
        private long _currentSequenceNumber;
        public EventStoreDb()
        {
            _currentSequenceNumber = 1;
        }
        public long NextSequenceNumber()
        {
            return _currentSequenceNumber++;
        }
    }
}