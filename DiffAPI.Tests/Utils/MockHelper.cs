using System.Threading.Tasks;
using DiffAPI.Controllers.v1;
using DiffAPI.Models;
using DiffAPI.Repository;
using DiffAPI.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace DiffAPI.Tests.Utils
{
    /// <summary>
    /// Returns mocked Diff Controller and basic JSON object to be used on unit tests
    /// </summary>
    public class MockHelper
    {
        public DiffController GetMockedDiffController(Json json)
        {
            var jsonId = "99";

            var mockRepository = new Mock<IJsonRepository>();
            mockRepository.Setup(x => x.GetById(jsonId)).Returns(Task.FromResult(json));
            mockRepository.Setup(x => x.AddOrUpdate(It.IsAny<Json>()));
            mockRepository.Setup(x => x.SaveChanges());

            var diffService = new DiffService();
            var encodeService = new EncodeService();
            var logger = new Logger<DiffController>(new LoggerFactory());
            
            var controller = new DiffController(mockRepository.Object,diffService,encodeService,logger);
            
            return controller;
        }

        public Json GetModelHelper()
        {
            return new Json
            {
                Id = 1,
                JsonId = "99",
//                {"Name":"Lucas"}
                Left = "eyJOYW1lIjoiTHVjYXMifQ==",
//                {"Name":"Robert"}
                Right = "eyJOYW1lIjoiUm9iZXJ0In0="
            };
        }
    }
}
