﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KeApiOpenSdk.Clients.Common.Logging;
using KeApiOpenSdk.Clients.Common.RequestSenders;
using KeApiOpenSdk.Clients.Common.ResponseMessages;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Clients.Common
{
    internal class InnerCommonClient
    {
        protected readonly ILogger Logger;
        protected readonly IRequestSender RequestSender;

        public InnerCommonClient(ILogger logger, IRequestSender requestSender)
        {
            Logger = logger;
            RequestSender = requestSender;
        }

        public async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object contentDto = null,
            TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendAsync(method, uriPath, uriQueryParams, contentDto, timeout);
            return JsonConvert.DeserializeObject<TResult>(await response.TryGetResponseAsync(Logger));
        }

        public async Task SendRequestAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object contentDto = null,
            TimeSpan? timeout = null)
        {
            var response = await RequestSender.SendAsync(method, uriPath, uriQueryParams, contentDto, timeout);
            await response.TryGetResponseAsync(Logger);
        }
    }
}