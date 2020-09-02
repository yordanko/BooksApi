using BooksApi.Models;
using Microsoft.OpenApi.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> GetAllBooks() =>
            _books.Find(book => true).ToList();

        public List<Book> GetByCategory(string category)
        {
            if(!String.IsNullOrEmpty(category) && category != Category.All.GetDisplayName() )
            {
                var result = _books.Aggregate()
                    .Match(new BsonDocument { { "Category", category } })
                    .Sort(new BsonDocument("Price", -1));

                return result.ToList();
            }
            
            return GetAllBooks();
        }
            

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}

