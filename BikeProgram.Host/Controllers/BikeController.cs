using BikeProgram.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeProgram.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeBusinessController : ControllerBase
    {
        private readonly IBikeBusinessProgram _bikeBusinessProgram;

        public BikeBusinessController(IBikeBusinessProgram bikeBusinessProgram)
        {
            _bikeBusinessProgram = bikeBusinessProgram;
        }

        [HttpGet("GetBikeDetails")]
        public IActionResult GetBikeWithManufacturerCheck(Guid bikeId)
        {
            var result = _bikeBusinessProgram.GetBikeDetailsWithManufacturerCheck(bikeId);

            if (result == null)
            {
                return NotFound("Bike or Manufacturer not found.");
            }

            return Ok(result);
        }
    }
}