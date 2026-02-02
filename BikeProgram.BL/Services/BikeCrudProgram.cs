using BikeProgram.BL.Interfaces;
using BikeProgram.DL.Interfaces;
using BikeProgram.Models.Dto;

namespace BikeProgram.BL.Services
{
    public class BikeCrudProgram : IBikeCrudProgram
    {
        private readonly IBikeRepository _bikeRepository;

        public BikeCrudProgram(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public void AddBike(Bike bike)
        {
            if (bike == null) return;

            if (bike.Id == Guid.Empty)
            {
                bike.Id = Guid.NewGuid();
            }

            _bikeRepository.AddBike(bike);
        }

        public void DeleteBike(Guid id)
        {
            _bikeRepository.DeleteBike(id);
        }

        public List<Bike> GetAllBikes()
        {
            return _bikeRepository.GetAllBikes();
        }

        public Bike? GetById(Guid id)
        {
            return _bikeRepository.GetById(id);
        }
    }
}