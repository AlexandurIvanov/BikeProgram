using LibrarySystem.DL.Interfaces;
using LibrarySystem.Models.Configurations;
using LibrarySystem.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibrarySystem.DL.Repositories
{
    public class BookMongoRepository : IBookRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<BookMongoRepository> _logger;
        private readonly IMongoCollection<Book> _booksCollection;

        public BookMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<BookMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _booksCollection = database.GetCollection<Book>($"{nameof(Book)}s");
        }

        public void AddBook(Book book)
        {
            if (book == null) return;

            try
            {
                _booksCollection.InsertOne(book);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding book to the DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public void DeleteBook(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = _booksCollection.DeleteOne(b => b.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No book found with Id: {id} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(DeleteBook)}:{e.Message}-{e.StackTrace}");
            }
        }

        public List<Book> GetAllBooks()
        {
            return _booksCollection.Find(_ => true).ToList();
        }

        public Book? GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return _booksCollection.Find(b => b.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}:{e.Message}-{e.StackTrace}");
            }

            return default;
        }
    }
}