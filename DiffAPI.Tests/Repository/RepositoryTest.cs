﻿using System.Threading.Tasks;
using DiffAPI.Models;
using DiffAPI.Repository;
using DiffAPI.Tests.Utils;
using Moq;
using Xunit;

namespace DiffAPI.Tests.Repository
{
    public class RepositoryTest
    {
        private static readonly MockHelper Mock = new MockHelper();
        private readonly Json _modelHelper = Mock.GetModelHelper();
        private readonly Mock<IJsonRepository> _mockRepository = new Mock<IJsonRepository>();

        [Fact]
        public void Should_Return_Json()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetById(_modelHelper.JsonId)).Returns(Task.FromResult(new Json()
            {
                Id = 1,
                JsonId = _modelHelper.JsonId,
                Left = _modelHelper.Left,
                Right = _modelHelper.Right
            }));
            //Act
            var result = _mockRepository.Object.GetById(_modelHelper.JsonId).Result;
            //Assert
            Assert.Equal(_modelHelper.Right, result.Right);
            Assert.Equal(_modelHelper.Left, result.Left);
        }

        [Fact]
        public void Should_Add_Or_Update_Json()
        {
            //Arrange
            _mockRepository.Setup(x => x.AddOrUpdate(It.IsAny<Json>())).Returns(true);

            var json = new Json
            {
                Id = 1,
                JsonId = _modelHelper.JsonId,
                Left = _modelHelper.Left,
                Right = null
            };
            //Act
            var response = _mockRepository.Object.AddOrUpdate(json);
            //Assert
            Assert.True(response);
        }
    }
}
