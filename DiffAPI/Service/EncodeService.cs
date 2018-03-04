using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiffAPI.Service
{
    public class EncodeService : IEncodeService
    {
        public string DecodeFrom(string input)
        {
            var data = Convert.FromBase64String(input);
            
            return Encoding.UTF8.GetString(data);
        }
        public JObject DeserializeJsonFrom(string input)
        {
            return JsonConvert.DeserializeObject<JObject>(DecodeFrom(input));
        }
    }
}