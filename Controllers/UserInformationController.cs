﻿using appPrevencionRiesgos.Exceptions;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Model.Security;
using appPrevencionRiesgos.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace appPrevencionRiesgos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInformationController : Controller
    {
        private IUserInformationService _userService;
        public UserInformationController(IUserInformationService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInformationModel>>> GetAllUsersAsync()
        {
            try
            {
                var informationList = await _userService.GetAllUsersAsync();
                return Ok(informationList);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInformationModel>> GetOneUserAsync(string id)
        {
            try
            {
                var information = await _userService.GetOneUserAsync(id);
                return Ok(information);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpGet("uid/{id}")]
        public async Task<ActionResult<UserInformationModel>> GetOneUserByUidAsync(string id)
        {
            try
            {
                var information = await _userService.GetOneUserByUidAsync(id);
                return Ok(information);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserInformationModel>> GetOneUserByEmailAsync(string email)
        {
            try
            {
                var information = await _userService.GetOneUserByEmailAsync(email);
                return Ok(information);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpPost("userconfidence")]
        public async Task<ActionResult<UserConfidenceExtendedModel>> PostAddUserConfidenceAsync([FromBody] UserConfidenceExtendedModel information)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newInformation = await _userService.AddUserConfidence(information);
                return Created($"/api/userinformation/userconfidence", newInformation);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyAddedElementException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<UserInformationModel>> PostUserAsync([FromBody] UserInformationModel information)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newInformation = await _userService.CreateUser(information);
                var newId = Convert.ToString(newInformation.Id);
                return Created($"/api/userinformation/{newId}", newInformation);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserInformationModel>> PutUserInformationAsync(string id, [FromBody] UserInformationModel information)
        {
            try
            {
                information.Id = new ObjectId(id);
                var updatedInformation = await _userService.UpdateUserAsync(id, information);
                return Ok(updatedInformation);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpPut("uid/{id}")]
        public async Task<ActionResult<UserInformationModel>> PutUserInformationByEmailAsync(string id, [FromBody] UserInformationModel information)
        {
            try
            {
                information.UserId = id;
                var updatedInformation = await _userService.UpdateUserByEmailAsync(id, information);
                return Ok(updatedInformation);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInformation(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpDelete("uid/{id}")]
        public async Task<ActionResult> DeleteInformationByUidAsync(string id)
        {
            try
            {
                await _userService.DeleteUserByUidAsync(id);
                return Ok();
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }

        [HttpDelete("email/{email}")]
        public async Task<ActionResult> DeleteInformationByEmailAsync(string email)
        {
            try
            {
                await _userService.DeleteUserByEmailAsync(email);
                return Ok();
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something Happened: {ex.Message}");
            }
        }
    }
}
