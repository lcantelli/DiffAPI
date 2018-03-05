using Newtonsoft.Json.Linq;

namespace DiffAPI.Service
{
    /// <summary>
    /// Encode Service Interface
    /// Documentation on Implementation
    /// </summary>
    public interface IEncodeService
    {
        JObject DeserializeJsonFrom(string input);
    }
}