using HttpClientService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientService
{
    public class TokenService
    {
        private string _clientId;
        private string _identityServerClientSecret;
        private string _identityServerUrl;
        private string _scope;

        public TokenService(string identityServerClientSecret, string identityServerUrl, string clientId, string scope)
        {
            _identityServerClientSecret = identityServerClientSecret;
            _identityServerUrl = identityServerUrl;
            _clientId = clientId;
            _scope = scope;
        }

        public async Task<TokenModel> GetToken()
        {
            var client = new HttpClient();
            var values = new List<KeyValuePair<string, string>>();

            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            values.Add(new KeyValuePair<string, string>("client_id", _clientId));
            values.Add(new KeyValuePair<string, string>("client_secret", _identityServerClientSecret));
            values.Add(new KeyValuePair<string, string>("scope", _scope));

            var req = new HttpRequestMessage(HttpMethod.Post, _identityServerUrl) { Content = new FormUrlEncodedContent(values) };

            var resp = await client.SendAsync(req);

            var content = await resp.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TokenModel>(content);
        }
    }
}