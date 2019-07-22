﻿using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Events;
using Refit;

namespace ExternDotnetSDK.Clients.Events
{
    public class EventsClient
    {
        private readonly IEventsClientRefit clientRefit;

        public EventsClient(HttpClient client) => clientRefit = RestService.For<IEventsClientRefit>(client);

        public async Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0") 
            => await clientRefit.GetEvents(take, fromId);
    }
}