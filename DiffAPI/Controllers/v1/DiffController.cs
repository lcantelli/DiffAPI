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
        //Dependent services and repositories
        private readonly IJsonRepository _jsonRepository;
        private readonly IEncodeService _encodeService;
        private readonly IDiffService _diffService;
        private readonly ILogger _logger;
        
        /// <summary>
        /// Main facade from API - uses dependency injection in order to load the services and repository
        /// There are 3 endpoints available
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="diffService"></param>
        /// <param name="encodeService"></param>
        /// <param name="logger"></param>
        public DiffController(IJsonRepository repository, IDiffService diffService, IEncodeService encodeService, ILogger<DiffController> logger)
        {
            _jsonRepository = repository;
            _encodeService = encodeService;
            _diffService = diffService;
            _logger = logger;
        }

        
        /// <summary>
        /// Saves the encoded JSON into the RIGHT position
        /// </summary>
        /// <param name="id">required field to identify the diff</param>
        /// <param name="json">required base64 encoded JSON</param>
        /// <returns>Action Result - bad request if fails, or Ok reponse if succeeded</returns>
        [Route("{id}/right")]
        [HttpPut]
        public async Task<IActionResult> RightJson(string id, [FromBody]string json)
        {
            try
            {
                //Deserializes and Saves JSON into database under given ID on the RIGHT side
                _encodeService.DeserializeJsonFrom(json);
                await _jsonRepository.SaveJson(id, json, Side.Right);
            }
            catch (Exception ex)
            {
                //Logs and Returns bad request if deserialize method or save fails
                _logger.LogError($"Failed to store JSON. Stack Trace: {ex.StackTrace}");
                return BadRequest($"Failed to store JSON. Error Message: {ex.Message}");
            }

            return Ok($"{Side.Right} json stored sucessfully.");
        }
        
        /// <summary>
        /// Saves the encoded JSON into the LEFT position
        /// </summary>
        /// <param name="id">required field to identify the diff</param>
        /// <param name="json">required base64 encoded JSON</param>
        /// <returns>Action Result - bad request if fails, or Ok reponse if succeeded</returns>
        [Route("{id}/left")]
        [HttpPut]
        public async Task<IActionResult> LeftJson(string id, [FromBody]string json)
        {
            try
            {
                //Deserializes and Saves JSON into database under given ID on the LEFT side
                _encodeService.DeserializeJsonFrom(json);
                await _jsonRepository.SaveJson(id, json, Side.Left);
            }
            catch (Exception ex)
            {
                //Logs and Returns bad request if deserialize method or save fails
                _logger.LogError($"Failed to deserialize and save JSON. Stack Trace: {ex.StackTrace}");
                return BadRequest(string.Format($"Failed to deserialize and save JSON. Error Message: {ex.Message}"));

            }

            return Ok($"{Side.Left} json stored sucessfully.");
        }

        /// <summary>
        /// Process differences between RIGHT and LEFT JSONS saved under given ID
        /// In case of any side missing, it fails with bad request
        /// </summary>
        /// <param name="id">existing ID saved previously by LEFT and RIGHT endpoints</param>
        /// <returns>Action REesult - Return success when objects are the same or different
        /// bad Request when ID does not exist, not given or when any side is missing</returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Diff(string id)
        {
            //parameter validation - fails if null or empty
            if (id == null || id.Trim() == string.Empty)
                return BadRequest("Id field is required.");

            //retrieves object from database
            var json = await _jsonRepository.GetById(id);

            //fails if Id doesn't exist
            if (json == null)
                return BadRequest("There is no JSON stored under given ID.");

            //fails if LEFT or RIGHT sides are empty 
            if (json.Left == null || json.Right == null)
                return BadRequest("Left and Right side are required to peform diff.");

            try
            {
                //Process differences and build response object
                var response = _diffService.ProcessDiff(json);
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Logs and Returns bad request if fails when processing differences
                _logger.LogError($"Failed to process diff. Stack Trace: {ex.StackTrace}");
                return BadRequest($"Failed to process diff. Error Message: {ex.Message}");
            }
        }
    }
}
