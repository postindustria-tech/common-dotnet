using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiftyOne.Common.Services
{
    /// <summary>
    /// Default implementation of IHttpClientWrapper.
    /// This is a simple wrapper which just calls straight to
    /// the client.
    /// </summary>
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public Task<HttpResponseMessage> GetAsync(
            string requestUri,
            CancellationToken cancellationToken)
        {
            return _client.GetAsync(requestUri, cancellationToken);
        }

        public Task<HttpResponseMessage> PostAsync(
            string uri,
            HttpContent content,
            CancellationToken cancellationToken)
        {
            return _client.PostAsync(uri, content, cancellationToken);
        }
    }
}
