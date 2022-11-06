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
    public class GetBooksQuery : IRequest<List<BookDto>> { }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
    {
        private readonly BooksInMemory _booksInMemory;

        public GetBooksQueryHandler(BooksInMemory booksInMemory) => _booksInMemory = booksInMemory;
        public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_booksInMemory.GetAllBooks().Result);
        }
    }
}
