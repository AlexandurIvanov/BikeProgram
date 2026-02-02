using FluentValidation;
using BikeProgram.BL.Interfaces;
using BikeProgram.Models.Dto;
using BikeProgram.Models.Requests;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace BikeProgram.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly IBikeCrudProgram _bikeCrudProgram;
        private readonly IMapper _mapper;
        private readonly IValidator<AddBikeRequest> _validator;

        public BikesController(
            IBikeCrudProgram bikeCrudProgram,
            IMapper mapper,
            IValidator<AddBikeRequest> validator)
        {
            _bikeCrudProgram = bikeCrudProgram;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("Add")]
        public IActionResult AddBike([FromBody] AddBikeRequest request)
        {
            if (request == null) return BadRequest("Bike data is null.");

            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var bike = _mapper.Map<Bike>(request);

            _bikeCrudProgram.AddBike(bike);

            return Ok("Bike created successfully");
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid id)
        {
            var bike = _bikeCrudProgram.GetById(id);

            if (bike == null)
                return NotFound("Bike not found");

            return Ok(bike);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var bikes = _bikeCrudProgram.GetAllBikes();
            return Ok(bikes);
        }
    }
}