using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nancy;

namespace ShoppingCartAPI.API
{
    public class EventsFeedModule: NancyModule
    {
        public EventsFeedModule(IEventStore eventStore): base("/events")
        {
            Get("/", _ =>
            {
                if (long.TryParse(this.Request.Query.start.Value, out long firstEventSequenceNumber))
                {
                    firstEventSequenceNumber = 0;
                }

                if (long.TryParse(Request.Query.end.Value, out long lastEventSequenceNumber))
                {
                    lastEventSequenceNumber = long.MaxValue;
                }

                return eventStore.GetEvents(firstEventSequenceNumber, lastEventSequenceNumber);
            });
        }
    }
}
