using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PetminderApp.Api
{
    public class RestClient
    {
        private HttpClient client;

        public RestClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri("http://74.140.176.113:3200/");
            //client.Timeout = TimeSpan.FromMilliseconds(30000);
        }
        ~RestClient()
        {
            client.Dispose();
        }

        public HttpResponseMessage Get(string endpoint, string auth)
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.Method = HttpMethod.Get;
                httpRequestMessage.Headers.TryAddWithoutValidation("token", auth);
                httpRequestMessage.RequestUri = new Uri(client.BaseAddress.OriginalString + endpoint);

                var responseBody = client.SendAsync(httpRequestMessage).Result;
                return responseBody;
            }
        }

        public HttpResponseMessage Login(string uri, string username, string password)
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.Method = HttpMethod.Get;
                httpRequestMessage.Headers.TryAddWithoutValidation("username", username);
                httpRequestMessage.Headers.TryAddWithoutValidation("password", password);
                httpRequestMessage.RequestUri = new Uri(client.BaseAddress.OriginalString + uri);

                var responseBody = client.SendAsync(httpRequestMessage).Result;
                return responseBody;
            }
        }

        public HttpResponseMessage Post(string endpoint, string request, string auth, string body)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                //httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
                requestMessage.Method = HttpMethod.Post;
                requestMessage.RequestUri = new Uri(client.BaseAddress.OriginalString + endpoint);
                requestMessage.Headers.TryAddWithoutValidation("token", auth);
                requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");

                try
                {
                    var response = client.SendAsync(requestMessage).Result;
                    return response;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    }
}
