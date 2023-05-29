using appPrevencionRiesgos.Exceptions;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace appPrevencionRiesgos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationInformationController : Controller
    {
        private ILocationInformationService _locationService;
        public LocationInformationController(ILocationInformationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetAllLocationsAsync()
        {
            try
            {
                var locationList = await _locationService.GetAllLocationsAsync();
                return Ok(locationList);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationModel>> GetOneLocationAsync(string id)
        {
            try
            {
                var location = await _locationService.GetOneLocationAsync(id);
                return Ok(location);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend. {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LocationModel>> PostLocationAsync([FromBody] LocationModel location)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newLocation = await _locationService.CreateLocation(location);
                var newId = Convert.ToString(newLocation.Id);
                return Created($"/api/basicinformation/{newId}", newLocation);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend. {ex.Message}"); 
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LocationModel>> PutLocationAsync(string id, [FromBody] LocationModel location)
        {
            try
            {
                location.Id = new ObjectId(id);
                var updatedLocation = await _locationService.UpdateLocationAsync(id, location);
                return Ok(updatedLocation);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happened: {ex.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLocation(string id)
        {
            try
            {
                await _locationService.DeleteLocationAsync(id);
                return Ok();
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happened. {ex.Message}");
            }
        }
    }
}
