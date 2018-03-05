using System.Collections.Generic;

namespace DiffAPI.Models
{
    /// <summary>
    /// Model used from DiffService to return the result from a sucessfull diff 
    /// </summary>
    public class DiffResult
    {
        //Given ID that stores both sides to be compared
        public string Id { get; set; }
        
        //Message result. E.g.: Objects are the same, or X incosnsistencies were found. 
        public string Message { get; set; }
        
        //Inconsistencies are stored in this list
        public List<string> Inconsistencies { get; set; }
    }
}