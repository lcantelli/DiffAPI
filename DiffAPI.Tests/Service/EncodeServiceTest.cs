using System;
using DiffAPI.Service;
using Xunit;

namespace DiffAPI.Tests.Service
{
    public class EncodeServiceTest
    {
        //{"Name":"Lucas"}
        private const string EncodedJson = "eyJOYW1lIjoiTHVjYXMifQ==";
        private const string UncodedString = "WrongFormat";

        private readonly EncodeService _encoderService = new EncodeService();
        
        [Fact]
        public void Should_Not_Decode_No_Base_64_String()
        {
            Assert.Throws<FormatException>(() => _encoderService.DecodeFrom(UncodedString));
        }

        [Fact]
        public void Should_Decode_Base_64_String()
        {
            var result = _encoderService.DecodeFrom(EncodedJson);

            Assert.Equal("{\"Name\":\"Lucas\"}", result);
        }
    }
}
