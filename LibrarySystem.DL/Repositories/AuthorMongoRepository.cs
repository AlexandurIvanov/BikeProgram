using LibrarySystem.DL.Interfaces;
using LibrarySystem.Models.Configurations;
using LibrarySystem.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibrarySystem.DL.Repositories
{
    public class AuthorMongoRepository : IAuthorRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<AuthorMongoRepository> _logger;
        private readonly IMongoCollection<Author> _authorsCollection;

        public AuthorMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<AuthorMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _authorsCollection = database.GetCollection<Author>($"{nameof(Author)}s");
        }

        public void AddAuthor(Author author)
        {
            if (author == null) return;
            try
            {
                _authorsCollection.InsertOne(author);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding author to DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public void DeleteAuthor(Guid id)
        {
            try
            {
                _authorsCollection.DeleteOne(a => a.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error delete author:{e.Message}");
            }
        }

        public List<Author> GetAllAuthors()
        {
            return _authorsCollection.Find(_ => true).ToList();
        }

        public Author? GetById(Guid id)
        {
            try
            {
                return _authorsCollection.Find(a => a.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error get author:{e.Message}");
                return null;
            }
        }
    }
}