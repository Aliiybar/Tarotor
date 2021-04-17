using System;
using Microsoft.AspNetCore.Identity;

namespace Tarotor.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
