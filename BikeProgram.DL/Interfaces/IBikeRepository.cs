using BikeProgram.Models.Dto;

namespace BikeProgram.DL.Interfaces
{
    public interface IBikeRepository
    {
        List<Bike> GetAllBikes();
        Bike? GetById(Guid? id);
        void AddBike(Bike bike);
        void DeleteBike(Guid? id);
    }
}