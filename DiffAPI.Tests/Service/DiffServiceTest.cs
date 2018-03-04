using System;
using System.Linq;
using DiffAPI.Models;
using DiffAPI.Service;
using DiffAPI.Tests.Utils;
using Xunit;

namespace DiffAPI.Tests.Service
{
    public class DiffServiceTest
    {
        private const string JsonId = "99";
        private readonly IDiffService _diffService = new DiffService();
        private static readonly MockHelper MockHelper = new MockHelper();
        private readonly Json _modelHelper = MockHelper.GetModelHelper();

        [Fact]
        public void Should_Return_Equals_Message_When_Objects_Are_The_Same()
        {
            var jsonSides = new Json
            {
                Id = 1,
                JsonId = JsonId,
                Left = _modelHelper.Left,
                Right = _modelHelper.Left
            };

            var result = _diffService.ProcessDiff(jsonSides);

            Assert.Equal(JsonId, result.Id);
            Assert.Equal("Objects are the same", result.Message);
            Assert.Empty(result.Inconsistencies);
        }

        [Fact]
        public void Should_Return_Inconsistencies_When_Objects_Are_Different()
        {
            var jsonSides = new Json
            {
                Id = 1,
                JsonId = JsonId,
                Left = _modelHelper.Left,
                Right = _modelHelper.Right
            };

            var result = _diffService.ProcessDiff(jsonSides);

            Assert.Equal(JsonId, result.Id);
            Assert.Equal("Found 1 inconsistencies between jsons", result.Message);
            Assert.Equal("Property 'Name' changed! From: Lucas - To: Robert", result.Inconsistencies.First());
            Assert.Single(result.Inconsistencies);
        }

        [Fact]
        public void Should_Fail_When_Left_Side_Is_Null()
        {
            var jsonSides = new Json
            {
                Id = 1,
                JsonId = JsonId,
                Left = null,
                Right = _modelHelper.Right
            };

            Assert.Throws<ArgumentNullException>(() => _diffService.ProcessDiff(jsonSides));
        }

        [Fact]
        public void Should_Fail_When_Right_Side_Is_Null()
        {
            var jsonSides = new Json
            {
                Id = 1,
                JsonId = JsonId,
                Left = _modelHelper.Left,
                Right = null
            };

            Assert.Throws<ArgumentNullException>(() => _diffService.ProcessDiff(jsonSides));
        }
    }
}
