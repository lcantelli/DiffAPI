using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiffAPI.Service
{
    /// <summary>
    /// Decodes and Deserealizes JSON
    /// </summary>
    public class EncodeService : IEncodeService
    {
        /// <summary>
        /// Auxiliar method to decode from string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string DecodeFrom(string input)
        {
            var data = Convert.FromBase64String(input);
            
            return Encoding.UTF8.GetString(data);
        }
        
        /// <summary>
        /// Deserializes JSON from base64 encoded string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>'
        public JObject DeserializeJsonFrom(string input)
        {
            return JsonConvert.DeserializeObject<JObject>(DecodeFrom(input));
        }
    }
}