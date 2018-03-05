using System.ComponentModel.DataAnnotations;

namespace DiffAPI.Models
{
    /// <summary>
    /// Database object, used on migrations and respository
    /// </summary>
    public class Json
    {
        [Required]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // auto-generated ID
        public int Id { get; set; }
        
        //ID provided by user
        public string JsonId { get; set; }
        //Left JSON
        public string Left { get; set; }
        //Right JSON
        public string Right { get; set; }
    }
}