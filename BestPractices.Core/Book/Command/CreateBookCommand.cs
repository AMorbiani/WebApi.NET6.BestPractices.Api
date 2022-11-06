using BestPractices.Core.Book.Data;
using BestPractices.Core.Book.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Book.Command
{
    public class CreateBookCommand : IRequest<bool>
    {
        public BookDto BookDto { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly BooksInMemory _booksInMemory;

        public CreateBookCommandHandler(BooksInMemory booksInMemory) => _booksInMemory = booksInMemory;
        public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _booksInMemory.GetAllBooks().Result.Find(b => b.Id == request.BookDto.Id);

            if (book != null) return await Task.FromResult(false);

            _booksInMemory.GetAllBooks().Result.Add(request.BookDto);

            return await Task.FromResult(true);
        }
    }
}
