using System.Collections.Generic;

namespace DiffAPI.Models
{
    public class DiffResult
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public List<string> Inconsistencies { get; set; }
    }
}