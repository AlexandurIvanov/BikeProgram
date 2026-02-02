using BikeProgram.Models.Dto;

namespace BikeProgram.DL.Interfaces
{
    public interface IManufacturerRepository
    {
        List<Manufacturer> GetAllManufacturers();
        Manufacturer? GetById(Guid id);
        void AddManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(Guid id);
    }
}