using BestPractices.Core.Book.Data;
using BestPractices.Core.Book.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Book.Query
{
    public class GetBookQuery : IRequest<BookDto>
    {
        public int Id { get; set; }
    }

    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
    {
        private readonly BooksInMemory _booksInMemory;

        public GetBookQueryHandler(BooksInMemory booksInMemory) => _booksInMemory = booksInMemory;

        public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book =  _booksInMemory.GetAllBooks().Result.Find(b => b.Id == request.Id);

            return await Task.FromResult(book);
        }
    }
}
