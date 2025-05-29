using BlazorForKidsSampleDatabase.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorForKidsSampleDatabase.Web
{
    public static class SampleData
    {
        public static async Task SeedSampleData(this WebApplication app)
        {

            await using var scope = app.Services.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            await dbContext.SeedDepartments();
            await dbContext.SeedEmployee();
            await CreateSampleUsersAndRoles(scope);
        }

        private static async Task CreateSampleUsersAndRoles(AsyncServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            // Define roles you want to ensure exist
            var rolesToCreate = new[] { "Admin", "Editor", "Viewer" };

            foreach (var role in rolesToCreate)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        logger.LogWarning("Failed to create role {Role}. Errors: {Errors}", role,
                            string.Join(" | ", result.Errors.Select(e => e.Description)));
                    }
                }
            }

            // Define users to create along with their roles
            var sampleUsers = new[]
            {
                new
                {
                    User = new ApplicationUser
                    {
                        UserName = "admin@mail.com",
                        Email = "admin@mail.com",
                        EmailConfirmed = true
                    },
                    Password = "P@s123456",
                    Roles = new[] { "Admin" }
                },
                new
                {
                    User = new ApplicationUser
                    {
                        UserName = "editor@mail.com",
                        Email = "editor@mail.com",
                        EmailConfirmed = true
                    },
                    Password = "P@s123456",
                    Roles = new[] { "Editor" }
                }
            };

            foreach (var entry in sampleUsers)
            {
                var user = entry.User;

                if (await userManager.FindByNameAsync(user.UserName!) != null)
                    continue; // Skip if user already exists

                var createResult = await userManager.CreateAsync(user, entry.Password);

                if (createResult.Succeeded)
                {
                    foreach (var role in entry.Roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }

                    logger.LogInformation("Created user {UserName} with roles: {Roles}", user.UserName, string.Join(", ", entry.Roles));
                }
                else
                {
                    logger.LogWarning("Failed to create user {UserName}. Errors: {Errors}", user.UserName,
                        string.Join(" | ", createResult.Errors.Select(e => e.Description)));
                }
            }
        }


        private static async Task SeedDepartments(this ApplicationDbContext context)
        {
            context.Department.Add(Department.Create("Production"));
            context.Department.Add(Department.Create("Quality"));
            context.Department.Add(Department.Create("Sales"));

            await context.SaveChangesAsync();
        }
        private static async Task SeedEmployee(this ApplicationDbContext context)
        {
            var departments = await context.Department.ToListAsync();

            for (int i = 0; i < 20; i++)
            {
                var department = departments[Random.Shared.Next(departments.Count)];
                var employee = Employee.Create($"FirstName{i}", $"LastName{i}", department.Id);
                context.Employee.Add(employee);
            }

            await context.SaveChangesAsync();
        }

    }
}