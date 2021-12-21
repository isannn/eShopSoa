using System.Collections.Generic;
using ShoppingCartAPI.Model;

namespace ShoppingCartAPI.API
{
    public interface IEventStore
    {
        void Raise(string addproductitem, object content);
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
    }
}