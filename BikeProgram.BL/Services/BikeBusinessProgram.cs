using BikeProgram.BL.Interfaces;
using BikeProgram.DL.Interfaces;
using BikeProgram.Models.Dto;

namespace BikeProgram.BL.Services
{
    public class BikeBusinessProgram : IBikeBusinessProgram
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public BikeBusinessProgram(IBikeRepository bikeRepository, IManufacturerRepository manufacturerRepository)
        {
            _bikeRepository = bikeRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public Bike? GetBikeDetailsWithManufacturerCheck(Guid bikeId)
        {
            var bike = _bikeRepository.GetById(bikeId);
            if (bike == null) return null;

            var manufacturer = _manufacturerRepository.GetById(bike.ManufacturerId);

            if (manufacturer == null)
            {
                return null;
            }

            return bike;
        }
    }
}