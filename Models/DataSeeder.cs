
using Microsoft.AspNetCore.Identity;
using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.MainBuildings;

namespace AparmentSystemAPI.Models
{
    public class DataSeeder
    {

        // if there is no user with "admin" role in database, create a user with "admin" role and assign its UserName and Password to "admin" and "admin" respectively.
        public static async Task SeedData(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            var hasRole = await roleManager.RoleExistsAsync("admin");

            if (!hasRole)
            {
                var role = new AppRole
                {
                    Name = "admin"

                };
                var roleCreateResult = await roleManager.CreateAsync(role);

            }

            var hasUserWithAdminRole = await userManager.GetUsersInRoleAsync("admin");

            if (hasUserWithAdminRole.Any())
            {
                return;

            }

            var user = new AppUser
            {
                UserName = "admin",
                TCNumber = "12345678901",
                PhoneNumber = "12345678901"

            };


            //create this user with "admin" role
            var resultPrimaryUserWithAdmin = await userManager.CreateAsync(user, "Admin12*");
         

            if (resultPrimaryUserWithAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");

            }

        }

        // if there is no flat in database with flatType = "Apartment" create a flat with flatType = "Apartment" and the other properties are default."
        public static async Task SeedMainBuilding(AppDbContext context)
        {
            var hasFlat = context.MainBuildings.Where(u => u.Name == "Apartment").FirstOrDefault();

            if (hasFlat != null)
            {
                return;
            }
            //find admin id
            var admin = context.Users.Where(u => u.UserName == "admin").FirstOrDefault();


            var building = new MainBuilding
            {
                Name = "Apartment",
                BuildingAge = "30+",
                UserId = admin.Id
             
            };
            // unitofwork pattern
            context.MainBuildings.Add(building);
            await context.SaveChangesAsync();

        }
    }
}

