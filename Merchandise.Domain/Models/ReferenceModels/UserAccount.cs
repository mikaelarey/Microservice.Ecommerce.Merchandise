using Merchandise.Domain.Models.Common;

namespace Merchandise.Domain.Models.ReferenceModels
{
    public class UserAccount : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }

        public UserAccount() : base()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        public UserAccount(string firstName, string lastName, string email) : base()
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
