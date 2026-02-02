using BikeProgram.DL.Interfaces;
using BikeProgram.Models.Configurations;
using BikeProgram.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BikeProgram.DL.Repositories
{
    public class BikeMongoRepository : IBikeRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<BikeMongoRepository> _logger;
        private readonly IMongoCollection<Bike> _bikesCollection;

        public BikeMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<BikeMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _bikesCollection = database.GetCollection<Bike>($"{nameof(Bike)}s");
        }

        public void AddBike(Bike bike)
        {
            if (bike == null) return;

            try
            {
                _bikesCollection.InsertOne(bike);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding bike to the DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public void DeleteBike(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = _bikesCollection.DeleteOne(b => b.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No bike found with Id: {id} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(DeleteBike)}:{e.Message}-{e.StackTrace}");
            }
        }

        public List<Bike> GetAllBikes()
        {
            return _bikesCollection.Find(_ => true).ToList();
        }

        public Bike? GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return _bikesCollection.Find(b => b.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}:{e.Message}-{e.StackTrace}");
            }

            return default;
        }
    }
}