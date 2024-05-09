using AmazonCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonRepository.Data.Identity
{
    public static class IdentityDataContextSeed
    {
        public static async Task IdentityApplySeeding(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser
                {
                    UserName = "omaralkadi",
                    DisplayName = "OmarElQady",
                    Email = "omaralkadi111@gmail.com",
                    PhoneNumber = "123456789"
                };
                await userManager.CreateAsync(User, "Omar@123");

            }

        }
    }
}
