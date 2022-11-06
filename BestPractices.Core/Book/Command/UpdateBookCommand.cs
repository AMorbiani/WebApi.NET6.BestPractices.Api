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
    public class UpdateBookCommand : IRequest<bool>
    {
        public BookDto BookDto { get; set; }
    }
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly BooksInMemory _booksInMemory;

        public UpdateBookCommandHandler(BooksInMemory booksInMemory) => _booksInMemory = booksInMemory;
        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _booksInMemory.GetAllBooks().Result.FirstOrDefault(b => b.Id == request.BookDto.Id);

            if (book == null) return await Task.FromResult(false);

            _booksInMemory.GetAllBooks().Result[_booksInMemory.GetAllBooks().Result.IndexOf(book)] = request.BookDto;

            return await Task.FromResult(true);
        }
    }
}
