using System;
using System.Threading.Tasks;
using DiffAPI.Models;
using DiffAPI.Repository;
using DiffAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiffAPI.Controllers.v1
{
    [Route("v1/diff")]
    public class DiffController : Controller
    {
        private readonly IRepository _repository;
        private readonly IEncodeService _encodeService;
        private readonly IDiffService _diffService;
        private readonly ILogger _logger;
        
        public DiffController(IRepository repository, IDiffService diffService, IEncodeService encodeService, ILogger<DiffController> logger)
        {
            _repository = repository;
            _encodeService = encodeService;
            _diffService = diffService;
            _logger = logger;
        }

        [Route("{id}/right")]
        [HttpPut]
        public async Task<IActionResult> RightJson(string id, [FromBody]string json)
        {
            try
            {
                _encodeService.DeserializeJsonFrom(json);
                await _repository.SaveJson(id, json, Side.Right);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to store JSON. Stack Trace: {ex.StackTrace}");
                return BadRequest($"Failed to store JSON. Error Message: {ex.Message}");
            }

            return Ok( new Response(true, $"{Side.Right} json stored sucessfully."));
        }

        [Route("{id}/left")]
        [HttpPut]
        public async Task<IActionResult> LeftJson(string id, [FromBody]string json)
        {
            try
            {
                _encodeService.DeserializeJsonFrom(json);
                await _repository.SaveJson(id, json, Side.Left);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to deserialize and save JSON. Stack Trace: {ex.StackTrace}");
                return BadRequest(string.Format($"Failed to deserialize and save JSON. Error Message: {ex.Message}"));

            }

            return Ok(new Response(true, $"{Side.Left} json stored sucessfully."));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Diff(string id)
        {
            if (id == null || id.Trim() == string.Empty)
                return BadRequest("Id field is required.");

            var json = await _repository.GetById(id);

            if (json == null)
                return BadRequest("There is no JSON stored under given ID.");

            if (json.Left == null || json.Right == null)
                return BadRequest("Left and Right side are required to peform diff.");

            try
            {
                var response = _diffService.ProcessDiff(json);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to process diff. Stack Trace: {ex.StackTrace}");
                return BadRequest($"Failed to process diff. Error Message: {ex.Message}");
            }
        }
    }
}
