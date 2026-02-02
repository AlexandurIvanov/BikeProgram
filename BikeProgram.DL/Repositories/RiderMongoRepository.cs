using BikeProgram.DL.Interfaces;
using BikeProgram.Models.Configurations;
using BikeProgram.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BikeProgram.DL.Repositories
{
    public class ManufacturerMongoRepository : IManufacturerRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<ManufacturerMongoRepository> _logger;
        private readonly IMongoCollection<Manufacturer> _manufacturersCollection;

        public ManufacturerMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<ManufacturerMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _manufacturersCollection = database.GetCollection<Manufacturer>($"{nameof(Manufacturer)}s");
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null) return;
            try
            {
                _manufacturersCollection.InsertOne(manufacturer);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding manufacturer to DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public void DeleteManufacturer(Guid id)
        {
            try
            {
                _manufacturersCollection.DeleteOne(m => m.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error delete manufacturer:{e.Message}");
            }
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return _manufacturersCollection.Find(_ => true).ToList();
        }

        public Manufacturer? GetById(Guid id)
        {
            try
            {
                return _manufacturersCollection.Find(m => m.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error get manufacturer:{e.Message}");
                return null;
            }
        }
    }
}