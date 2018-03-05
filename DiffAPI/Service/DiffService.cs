using System.Collections.Generic;
using DiffAPI.Models;
using Newtonsoft.Json.Linq;

namespace DiffAPI.Service
{
    
    /// <summary>
    /// Process differences between JSON
    /// </summary>
    public class DiffService : IDiffService
    {
        
        /// <summary>
        /// Receives a JSON and process its differences 
        /// </summary>
        /// <param name="json"></param>
        /// <returns>Object containing analysis result</returns>
        public DiffResult ProcessDiff(Json json)
        {
            var inconsistenciesList = new List<string>();
            var encodeService = new EncodeService();
            string message;

            //gets JSONs from both sides
            var leftSide = encodeService.DeserializeJsonFrom(json.Left);
            var righSide = encodeService.DeserializeJsonFrom(json.Right);

            //checks if size and content matches
            var matchesSize = leftSide.Count == righSide.Count;
            var matchesContent = JToken.DeepEquals(leftSide, righSide);

            
            //if sizes are different, sets message and skip
            if (!matchesSize)
            {
                message = "Lenght of both JSONs doesn't match.";
            }
            //if contents are equal and size matches, sets message and skip
            else if (matchesContent)
            {
                message = "Objects are the same";
            }
            //if sizes are equal and contents are different, process inconsistencies
            else
            {
                var counter = 0;
                
                //compare sides each by each
                foreach (var property in leftSide)
                {
                    var targetProp = righSide.Property(property.Key);

                    if (JToken.DeepEquals(property.Value, targetProp.Value)) continue;
                    
                    //add inconsistencies to list
                    inconsistenciesList.Add($"Property '{property.Key}' changed! From: {property.Value} - To: {targetProp.Value}");
                    counter += 1;
                }

                message = $"Found {counter} inconsistencies between jsons";
            }
            
            //build result object using message and inconsistencies (if any) 
            var diffResult = new DiffResult
            {
                Message = message,
                Inconsistencies = inconsistenciesList,
                Id = json.JsonId
            };

            return diffResult;
        }
    }
}