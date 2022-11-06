using BestPractices.Core.Book.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Book.Data
{
    public class BooksInMemory
    {
        private static List<BookDto> _bookList;

        public BooksInMemory()
        {
            _bookList = new List<BookDto>()
            {
                new BookDto()
                {
                    Id = 1,
                    Title = "Red",
                    Description = "Good",
                    Author = "John"
                },
                new BookDto()
                {
                    Id = 2,
                    Title = "Blue",
                    Description = "Bad",
                    Author = "Luca"
                },
                new BookDto()
                {
                    Id = 3,
                    Title = "Green",
                    Description = "Bad",
                    Author = "Marco"
                },
                new BookDto()
                {
                    Id = 4,
                    Title = "Yellow",
                    Description = "Good",
                    Author = "John"
                }
            };
        }
        public async Task<List<BookDto>> GetAllBooks() => await Task.FromResult(_bookList);
    }
}

