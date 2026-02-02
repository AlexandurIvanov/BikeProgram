using BikeProgram.Models.Dto;

namespace BikeProgram.BL.Interfaces
{
    public interface IBikeCrudProgram
    {
        List<Bike> GetAllBikes();
        Bike? GetById(Guid id);
        void AddBike(Bike bike);
        void DeleteBike(Guid id);
    }
}