using System.ComponentModel.DataAnnotations;

namespace DiffAPI.Models
{
    public class Json
    {
        [Required]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public int Id { get; set; }
        public string JsonId { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
    }
}