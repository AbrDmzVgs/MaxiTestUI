using HttpClientService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpClientService
{
    public class BaseClientService
    {
        private readonly HttpClientServices _httpClient;
        private static TokenModel _tokenModel;
        private readonly string _clientId;
        private readonly string _endPoint;
        private readonly string _secret;
        private readonly string _scope;
        private static string _caller;
        private bool _refreshToken;

        public BaseClientService(string baseUrl, string secret, string endPoint, string clientId, string scope, IDictionary<string, string> headerParameters = null, string caller = null)
        {
            _httpClient = new HttpClientServices(baseUrl, headerParameters);
            _secret = secret;
            _endPoint = endPoint;
            _clientId = clientId;
            _scope = scope;
            _refreshToken = caller != _caller;
            _caller = caller;
        }

        public async Task<string> GetAsync(string queryString, string endPoint)
        {
            //var token = await GetToken();

            return await _httpClient.GetAsync(queryString, endPoint, null);
        }

        public async Task<string> GetAsyncWithErrors(string queryString, string endPoint)
        {
            //var token = await GetToken();

            return await _httpClient.GetAsyncWithErrors(queryString, endPoint, null);
        }

        public async Task<byte[]> GetByteAsync(string queryString, string endPoint)
        {
            //var token = await GetToken();

            return await _httpClient.GetByteAsync(queryString, endPoint, null);
        }

        public async Task<string> PostAsync(Object resource, string endPoint, string dateTimeFormat = null)
        {
            //var token = await GetToken();

            return await _httpClient.PostAsync(resource, endPoint, null, dateTimeFormat);
        }

        public async Task<string> PostAsyncWithErrors(Object resource, string endPoint, string dateTimeFormat = null)
        {
            //var token = await GetToken();

            return await _httpClient.PostAsyncWithErrors(resource, endPoint, null, dateTimeFormat);
        }

        public async Task<string> PutAsync(Object resource, string endPoint, string dateTimeFormat = null)
        {
            //var token = await GetToken();

            return await _httpClient.PutAsync(resource, endPoint, null, dateTimeFormat);
        }

        public async Task<string> PutAsyncWithErrors(Object resource, string endPoint, string dateTimeFormat = null)
        {
            //var token = await GetToken();

            return await _httpClient.PutAsyncWithErrors(resource, endPoint, null, dateTimeFormat);
        }

        public async Task<TokenModel> GetToken()
        {
            if (_refreshToken || _tokenModel == null || string.IsNullOrEmpty(_tokenModel.access_token) || _tokenModel.expire.AddSeconds(30) < DateTime.Now)
            {
                var tokenService = new TokenService(_secret, _endPoint, _clientId, _scope);

                _tokenModel = await tokenService.GetToken();

                _refreshToken = false;
            }

            return _tokenModel;
        }
    }
}