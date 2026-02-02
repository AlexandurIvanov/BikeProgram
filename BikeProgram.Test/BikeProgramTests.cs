using BikeProgram.BL.Interfaces;
using BikeProgram.DL.Interfaces;
using BikeProgram.Models.Dto;
using Moq;
using Xunit;

namespace BikeProgram.Test
{
    public class BikeBusinessProgramTests
    {
        private Mock<IBikeRepository> _bikeRepositoryMock;
        private Mock<IManufacturerRepository> _manufacturerRepositoryMock;

        [Fact]
        public void GetBikeDetails_Return_Ok()
        {
            // Arrange
            _bikeRepositoryMock = new Mock<IBikeRepository>();
            _manufacturerRepositoryMock = new Mock<IManufacturerRepository>();
            var manufacturerId = Guid.NewGuid();
            var bikeId = Guid.NewGuid();

            _bikeRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Bike
            {
                Id = bikeId,
                Title = "Specialized Rockhopper",
                Price = 1200,
                ManufacturerId = manufacturerId
            });

            _manufacturerRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Manufacturer
            {
                Id = manufacturerId,
                Name = "Specialized"
            });

            var bikeService = new BL.Services.BikeBusinessProgram(_bikeRepositoryMock.Object, _manufacturerRepositoryMock.Object);

            // Act
            var result = bikeService.GetBikeDetailsWithManufacturerCheck(bikeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bikeId, result.Id);
            Assert.Equal("Specialized Rockhopper", result.Title);
        }

        [Fact]
        public void GetBikeDetails_When_Manufacturer_Missing()
        {
            // Arrange
            _bikeRepositoryMock = new Mock<IBikeRepository>();
            _manufacturerRepositoryMock = new Mock<IManufacturerRepository>();
            var manufacturerId = Guid.NewGuid();
            var bikeId = Guid.NewGuid();

            _bikeRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Models.Dto.Bike
            {
                Id = bikeId,
                Title = "Trek Domane",
                Price = 2500,
                ManufacturerId = manufacturerId
            });

            // Simulate manufacturer not found in DB
            _manufacturerRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Manufacturer)null);

            var bikeService = new BL.Services.BikeBusinessProgram(_bikeRepositoryMock.Object, _manufacturerRepositoryMock.Object);

            // Act
            var result = bikeService.GetBikeDetailsWithManufacturerCheck(bikeId);

            // Assert
            Assert.Null(result);
        }
    }
}