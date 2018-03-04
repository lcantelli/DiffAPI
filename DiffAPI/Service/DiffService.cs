using System.Collections.Generic;
using DiffAPI.Models;
using Newtonsoft.Json.Linq;

namespace DiffAPI.Service
{
    public class DiffService : IDiffService
    {
        public DiffResult ProcessDiff(Json jsonById)
        {
            var diffList = new List<string>();
            var encodeService = new EncodeService();
            var message = "";

            var leftSide = encodeService.DeserializeJsonFrom(jsonById.Left);
            var righSide = encodeService.DeserializeJsonFrom(jsonById.Right);

            var matchesSize = leftSide.Count == righSide.Count;
            var matchesContent = JToken.DeepEquals(leftSide, righSide);

            if (!matchesSize)
            {
                message = "Lenght of both JSONs doesn't match.";
            }

            if (matchesContent && matchesSize)
            {
                message = "Objects are the same";
            }

            if (!matchesContent && matchesSize)
            {
                var counter = 0;
                
                foreach (var property in leftSide)
                {
                    var targetProp = righSide.Property(property.Key);

                    if (JToken.DeepEquals(property.Value, targetProp.Value)) continue;

                    diffList.Add($"Property '{property.Key}' changed! From: {property.Value} - To: {targetProp.Value}");
                    counter += 1;
                }

                message = $"Found {counter} inconsistencies between jsons";
            }

            var diffResult = new DiffResult
            {
                Message = message,
                Inconsistencies = diffList,
                Id = jsonById.JsonId
            };

            return diffResult;
        }
    }
}