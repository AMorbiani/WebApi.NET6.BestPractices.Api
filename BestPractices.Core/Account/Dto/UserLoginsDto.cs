using System.ComponentModel.DataAnnotations;

namespace BestPractices.Core.Account.Dto
{
    public class UserLoginsDto
    {
        [Required]
        public string UserName
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }
        public UserLoginsDto() { }
    }
}
