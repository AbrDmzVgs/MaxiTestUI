using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientService.Models
{
    public class TokenModel
    {
        public string access_token { get; set; }
        private int expire_in_xSeconds;
        public int expires_in
        {
            get
            {
                return this.expire_in_xSeconds;
            }
            set
            {
                this.expire_in_xSeconds = value;
                this.expire = DateTime.Now.AddSeconds(value);
            }
        }
        public string token_type { get; set; }
        public DateTime expire;
    }
}
