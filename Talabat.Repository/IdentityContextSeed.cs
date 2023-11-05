using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.IdentityEntities;

namespace Talabat.Repository
{
    public class IdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Count() == 0) 
            {
                var user = new AppUser
                {
                    DisplayName = "Ahmed",
                    Email = "ahmed@gmai.com",
                    UserName = "ahmedkamal",
                    Address = new Address
                    {
                        FirstName = "Ahmed",
                        LastName = "Kamal",
                        Street = "Salah-Nesem.st",
                        City = "Suez",
                        State = "Suez",
                        ZipCode = "154"
                    }
                };

                await userManager.CreateAsync(user , "Password123!");
            }
        }
    }
}
