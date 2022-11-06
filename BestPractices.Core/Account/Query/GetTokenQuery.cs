using BestPractices.API.Authentication.Models;
using BestPractices.Core.Account.Data;
using BestPractices.Core.Account.Dto;
using BestPractices.Core.Account.Models;
using BestPractices.Core.Account.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Account.Query
{
    public class GetTokenQuery : IRequest<UserToken>
    {
        public UserLoginsDto UserLoginsDto { get; set; }
    }
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, UserToken>
    {
        private readonly JwtSettings jwtSettings;
        private readonly UsersInMemory _usersInMemory;
        public GetTokenQueryHandler(UsersInMemory usersInMemory, JwtSettings jwtSettings)
        {
            _usersInMemory = usersInMemory;
            this.jwtSettings = jwtSettings;
        }

        public async Task<UserToken> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var Token = new UserToken();
            var Valid = _usersInMemory.GetAllUsers().Result.Any(x => x.UserName.Equals(request.UserLoginsDto.UserName, StringComparison.OrdinalIgnoreCase));
            if (Valid)
            {
                var user = _usersInMemory.GetAllUsers().Result.FirstOrDefault(x => x.UserName.Equals(request.UserLoginsDto.UserName, StringComparison.OrdinalIgnoreCase));
                Token = JwtHelper.GenTokenkey(new UserToken()
                {
                    EmailId = user.EmailId,
                    GuidId = Guid.NewGuid(),
                    UserName = user.UserName,
                    Id = user.Id,
                }, jwtSettings);
            }
            else
            {
                Token = null;
                return await Task.FromResult(Token);
            }
            return await Task.FromResult(Token);
        }
    }
}
