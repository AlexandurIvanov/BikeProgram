using BikeProgram.Models.Dto;

namespace BikeProgram.BL.Interfaces
{
    public interface IBikeBusinessProgram
    {
        Bike? GetBikeDetailsWithManufacturerCheck(Guid bikeId);
    }
}