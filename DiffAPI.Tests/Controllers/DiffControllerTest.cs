using System.Threading.Tasks;
using DiffAPI.Models;
using DiffAPI.Tests.Utils;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DiffAPI.Tests.Controllers
{
    public class DiffControllerTest
    {
        private const string WrongFormat = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        private static readonly MockHelper Mock = new MockHelper();
        private readonly Json _modelHelper = Mock.GetModelHelper();


        [Fact]
        public async Task Should_Return_Sucess_When_Correct_Encoded_Json_LEFT()
        {
            var controller = Mock.GetMockedDiffController(new Json());
            var response = (OkObjectResult) await controller.LeftJson(_modelHelper.JsonId, _modelHelper.Left);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal("Left json stored sucessfully.", (response.Value as Response)?.Message);
            Assert.True(((Response) response.Value).Success);
        }

        [Fact]
        public async Task Should_Return_Sucess_When_Correct_Encoded_Json_RIGHT()
        {
            var controller = Mock.GetMockedDiffController(new Json());
            var response = (OkObjectResult) await controller.RightJson(_modelHelper.JsonId, _modelHelper.Right);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal("Right json stored sucessfully.", (response.Value as Response)?.Message);
            Assert.True(((Response) response.Value).Success);
        }

        [Fact]
        public async Task Should_Fail_When_Wrong_Json_Format_LEFT()
        {
            var controller = Mock.GetMockedDiffController(new Json());
            var response = await controller.LeftJson(_modelHelper.JsonId, WrongFormat);
            Assert.IsType<BadRequestObjectResult>(response);
        }
        
        [Fact]
        public async Task Should_Fail_When_Wrong_Json_Format_RIGHT()
        {
            var controller = Mock.GetMockedDiffController(new Json());
            var response = await controller.RightJson(_modelHelper.JsonId, WrongFormat);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Should_Fail_When_Empty_Json_LEFT()
        {
            var controller = Mock.GetMockedDiffController(new Json { Id = 1, Left = null, Right = _modelHelper.Right, JsonId = _modelHelper.JsonId });
            var response = await controller.LeftJson(_modelHelper.JsonId, null);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Should_Fail_When_Empty_Json_RIGHT()
        {
            var controller = Mock.GetMockedDiffController(new Json { Id = 1, Left = _modelHelper.Left, Right = null, JsonId = _modelHelper.JsonId });
            var response = await controller.RightJson(_modelHelper.JsonId, null);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Should_Sucess_When_Diff_Process_Correctly()
        {
            var controller = Mock.GetMockedDiffController(new Json { Id = 1, Left = _modelHelper.Left, Right = _modelHelper.Right, JsonId = _modelHelper.JsonId });
            var response = await controller.Diff(_modelHelper.JsonId);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Should_Sucess_When_Diff_Process_Inconsistencies_Correctly()
        {
            var controller = Mock.GetMockedDiffController(new Json() { Id = 1, Left = _modelHelper.Left, Right = _modelHelper.Right, JsonId = _modelHelper.JsonId });
            var response = await controller.Diff(_modelHelper.JsonId);
            Assert.IsType<OkObjectResult>(response);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public async Task Should_Fail_Then_Wrong_Id_Format_Is_Provided(string id)
        {
            var controller = Mock.GetMockedDiffController(new Json());
            var response = (BadRequestObjectResult) await controller.Diff(id);
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal("Id field is required.", response.Value);
        }
    }
}
