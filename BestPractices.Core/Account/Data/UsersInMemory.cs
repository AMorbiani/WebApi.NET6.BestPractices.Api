using BestPractices.Core.Account.Models;
using BestPractices.Core.Book.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Account.Data
{
    public class UsersInMemory
    {
        private static List<Users> _userList;

        public UsersInMemory()
        {
            _userList = new List<Users>()
            {
                new Users() {
                    Id = Guid.NewGuid(),
                        EmailId = "adminakp@gmail.com",
                        UserName = "admin",
                        Password = "admin",
                },
                new Users() {
                    Id = Guid.NewGuid(),
                        EmailId = "adminakp@gmail.com",
                        UserName = "user1",
                        Password = "admin",
                }
            };
        }
        public async Task<List<Users>> GetAllUsers() => await Task.FromResult(_userList);
    }
}
