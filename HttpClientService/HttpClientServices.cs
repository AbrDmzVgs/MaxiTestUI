using HttpClientService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientService
{
    public class HttpClientServices
    {
        private IDictionary<string, string> _headerParameters;
        private string _serviceUrl;

        public HttpClientServices(string serviceUrl, IDictionary<string, string> headerParameters = null)
        {
            _serviceUrl = serviceUrl;
            _headerParameters = headerParameters;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public async Task<byte[]> GetByteAsync(string queryString, string endpoint, TokenModel tokenModel = null)
        {
            using (var client = GetClient(tokenModel))
            {
                try
                {
                    var requestUri = UrlPathCombine(queryString, endpoint);

                    var resp = await client.GetAsync(requestUri);

                    resp.EnsureSuccessStatusCode();

                    return await resp.Content.ReadAsByteArrayAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<string> GetAsync(string queryString, string endpoint, TokenModel tokenModel = null)
        {
            using (var client = GetClient(tokenModel))
            {
                try
                {
                    var requestUri = UrlPathCombine(queryString, endpoint);

                    var resp = await client.GetAsync(requestUri);

                    resp.EnsureSuccessStatusCode();

                    return await resp.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<string> GetAsyncWithErrors(string queryString, string endpoint, TokenModel tokenModel = null)
        {
            using (var client = GetClient(tokenModel))
            {
                try
                {
                    var requestUri = UrlPathCombine(queryString, endpoint);

                    var resp = await client.GetAsync(requestUri);

                    if (resp.StatusCode != HttpStatusCode.OK && resp.StatusCode != HttpStatusCode.BadRequest)
                        resp.EnsureSuccessStatusCode();

                    return await resp.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<string> PostAsync(object resource, string endpoint, TokenModel tokenModel = null, string dateTimeFormat = null)
        {
            using (var client = GetClient(tokenModel))
            {
                try
                {
                    var httpContent = new StringContent(SerializeObject(resource, dateTimeFormat), Encoding.UTF8, "application/json");

                    var resp = await client.PostAsync(endpoint, httpContent);

                    resp.EnsureSuccessStatusCode();

                    return await resp.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<string> PostAsyncWithErrors(object resource, string endpoint, TokenModel tokenModel = null, string dateTimeFormat = null)
        {
            using (var client = GetClient(tokenModel))
            {
                try
                {
                    var httpContent = new StringContent(SerializeObject(resource, dateTimeFormat), Encoding.UTF8, "application/json");

                    var resp = await client.PostAsync(endpoint, httpContent);

                    if (resp.StatusCode != HttpStatusCode.OK && resp.StatusCode != HttpStatusCode.BadRequest)
                        resp.EnsureSuccessStatusCode();

                    return await resp.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<string> PostAsyncWithErrors(MultipartFormDataContent formData, string endpoint, TokenModel tokenModel = null)
        {
            using (var client = GetClient(tokenModel))
            {
                try
                {
                    var resp = await client.PostAsync(endpoint, formData);

                    if (resp.StatusCode != HttpStatusCode.OK && resp.StatusCode != HttpStatusCode.BadRequest)
                        resp.EnsureSuccessStatusCode();

                    return await resp.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<string> PutAsync(object resource, string endpoint, TokenModel tokenModel = null, string dateTimeFormat = null)
        {
            using (var client = GetClient(tokenModel))
            {
                try
                {
                    var httpContent = new StringContent(SerializeObject(resource, dateTimeFormat), Encoding.UTF8, "application/json");

                    var resp = await client.PutAsync(endpoint, httpContent);

                    resp.EnsureSuccessStatusCode();

                    return await resp.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<string> PutAsyncWithErrors(object resource, string endpoint, TokenModel tokenModel = null, string dateTimeFormat = null)
        {
            using (var client = GetClient(tokenModel))
            {
                try
                {
                    var httpContent = new StringContent(SerializeObject(resource, dateTimeFormat), Encoding.UTF8, "application/json");

                    var resp = await client.PutAsync(endpoint, httpContent);

                    if (resp.StatusCode != HttpStatusCode.OK && resp.StatusCode != HttpStatusCode.BadRequest)
                        resp.EnsureSuccessStatusCode();

                    return await resp.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private HttpClient GetClient(TokenModel tokenModel)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(_serviceUrl)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(tokenModel.token_type, tokenModel.access_token);

            if (_headerParameters != null)
            {
                foreach (var parameter in _headerParameters)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(parameter.Key, parameter.Value);
                }
            }

            return client;
        }

        private string UrlPathCombine(string queryString, string endpoint)
        {
            while (endpoint.EndsWith("/"))
            {
                endpoint = endpoint.Remove(endpoint.Length - 1);
            }

            while (queryString.EndsWith("/"))
            {
                queryString = queryString.Remove(queryString.Length - 1);
            }

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                return string.Format("{0}/{1}", endpoint, queryString);
            }

            return endpoint;
        }

        private string SerializeObject(object resource, string dateTimeFormat = null)
        {
            return string.IsNullOrEmpty(dateTimeFormat) ? JsonConvert.SerializeObject(resource) : JsonConvert.SerializeObject(resource, new IsoDateTimeConverter { DateTimeFormat = dateTimeFormat });
        }
    }
}