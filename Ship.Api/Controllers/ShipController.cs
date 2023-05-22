using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ship.Bussiness.Models;
using Ship.Bussiness.Services;
using Ship.Common.Exceptions;

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        private IShipService _shipService;

        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }
        /// <summary>
        /// if Want get all not set take and skip 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchShipViewModel model)
        {
            var ship = await _shipService.Search(model);
            return Ok(ship);
        }


        /// <summary>
        /// add ships to the system
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ShipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _shipService.Add(model);
            return Ok();
        }
     


        /// <summary>
        /// estimated arrival time of given ship to the closest port.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("estimated_arrival")]
        public async Task<IActionResult> EstimatedArrival([FromBody] EstimatedArrivalModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _shipService.EstimationArrival(model);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        /// <summary>
        /// Update velocity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="velocity"></param>
        /// <returns></returns>
        [HttpPatch("{id}/velocity")]
        public async Task<IActionResult> Updatevelocity(Guid id, [FromBody] double velocity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _shipService.UpdateVelocity(id, velocity);
            return Ok();
        }
    }
}
