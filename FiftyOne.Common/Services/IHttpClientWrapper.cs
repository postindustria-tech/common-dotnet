using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiftyOne.Common.Services
{
    /// <summary>
    /// Wrapper for an HttpClient instance.
    /// An implementation can do various things, such as
    /// limit the number of concurrent requests.
    /// </summary>
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// Calls <see cref="HttpClient.GetAsync(string, CancellationToken)"/>.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken);

        /// <summary>
        /// Calls <see cref="HttpClient.PostAsync(string, HttpContent, CancellationToken)"/>.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, CancellationToken cancellationToken);
    }
}
