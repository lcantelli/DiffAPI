using System.Linq;
using System.Threading.Tasks;
using DiffAPI.Models;
using DiffAPI.Tests.Utils;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DiffAPI.Tests.Integrations
{
    public class IntegratedTest
    {
        private static readonly MockHelper Mock = new MockHelper();
        private readonly Json _modelHelper = Mock.GetModelHelper();

        [Fact]
        public async Task Should_Return_Inconsistencies_Successfully()
        {
            //Arrange
            var mockedDiffController = Mock.GetMockedDiffController(new Json { Id = 99, Left = _modelHelper.Left, Right = _modelHelper.Right, JsonId = _modelHelper.JsonId });
            await mockedDiffController.LeftJson(_modelHelper.JsonId, _modelHelper.Left);
            await mockedDiffController.RightJson(_modelHelper.JsonId, _modelHelper.Right);
            //Act
            var response = (OkObjectResult) mockedDiffController.Diff(_modelHelper.JsonId).Result;
            //Assert
            Assert.IsType<OkObjectResult>(response);
            var diffResult = (DiffResult)response.Value;
            Assert.Equal("Found 1 inconsistencies between jsons", diffResult.Message);
            Assert.Single(diffResult.Inconsistencies);
            Assert.Equal("Property 'Name' changed! From: Lucas - To: Robert", diffResult.Inconsistencies.First());
        }
    }
}
