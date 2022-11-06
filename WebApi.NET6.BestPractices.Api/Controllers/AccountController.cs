using BestPractices.API.Authentication.Models;
using BestPractices.API.Authentication;
using Microsoft.AspNetCore.Mvc;
using BestPractices.API.Controllers.Common;
using BestPractices.Core.Account.Models;
using BestPractices.Core.Account.Dto;
using BestPractices.Core.Account.Query;
using Microsoft.AspNetCore.Authorization;

namespace BestPractices.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AccountController : BaseAPIController
    {
        /// <summary>
        /// Get an auhtentication token for calling the endpoints
        /// </summary>
        /// <param name="getTokenQuery">User logins</param>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///       "userLoginsDto": {
        ///         "userName": "admin",
        ///         "password": "admin"
        ///       }
        ///     }
        ///     
        /// Sample Response:
        /// 
        ///     {
        ///       "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjZiMGVkMzM3LTZkNjktNDM0Zi1iZTlkLTgzMWM5YmI5MDAzYyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluYWtwQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiZjg1MWE2NjMtZTY5My00MWQ0LWEwNjYtZjViYWE0ZGRiNmEzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9leHBpcmF0aW9uIjoiT2N0IFR1ZSAyNSAyMDIyIDEwOjAyOjIxIEFNIiwibmJmIjoxNjY2NjA1NzQxLCJleHAiOjE2NjY2ODQ5NDEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwOTYiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDk2In0.ICFkf_J8JaPECeyTzGL4hCqVDedkq1I0ov0Dl3BrvVo",
        ///       "userName": "admin",
        ///       "validaty": "10:02:21.7847976",
        ///       "refreshToken": null,
        ///       "id": "6b0ed337-6d69-434f-be9d-831c9bb9003c",
        ///       "emailId": null,
        ///       "guidId": "f851a663-e693-41d4-a066-f5baa4ddb6a3",
        ///       "expiredTime": "0001-01-01T00:00:00"
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginsDto))]
        public async Task<ActionResult<UserLoginsDto>> GetToken(GetTokenQuery getTokenQuery)
        {
            var result = await Mediator.Send(getTokenQuery);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
