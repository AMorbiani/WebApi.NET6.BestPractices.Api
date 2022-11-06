using BestPractices.API.Controllers.Common;
using BestPractices.Common.Exceptions;
using BestPractices.Core.Book.Command;
using BestPractices.Core.Book.Dto;
using BestPractices.Core.Book.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BestPractices.API.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class BookController : BaseAPIController
    {
        [HttpGet("error")]
        [AllowAnonymous]
        public async Task<ActionResult> Error()
        {
            throw new DefaultBestPracticesException()
            {
                ErrorCode = 10,
                HttpStatusCode = HttpStatusCode.FailedDependency,
                Message = "Book Error"
            };
        }

        [HttpGet("book")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
        public async Task<ActionResult<BookDto>> Get(int id)
        {
            var result = await Mediator.Send(new GetBookQuery() { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("books")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDto>))]
        public async Task<ActionResult<List<BookDto>>> GetAll()
        {
            var result = await Mediator.Send(new GetBooksQuery());
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDto))]
        public async Task<ActionResult<BookDto>> Create(CreateBookCommand createBookCommand)
        {
            var result = await Mediator.Send(createBookCommand);
            if (!result) return NotFound();
            return Created($"book/{createBookCommand.BookDto.Id}", createBookCommand);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<BookDto>> Update(UpdateBookCommand updateBookCommand)
        {
            var result = await Mediator.Send(updateBookCommand);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
