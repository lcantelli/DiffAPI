using Newtonsoft.Json.Linq;

namespace DiffAPI.Service
{
    public interface IEncodeService
    {
        JObject DeserializeJsonFrom(string input);
    }
}