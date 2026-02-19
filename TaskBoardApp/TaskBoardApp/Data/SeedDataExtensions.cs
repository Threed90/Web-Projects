using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Entities = TaskBoardApp.Data.Entities;

namespace TaskBoardApp.Data
{
    public static class SeedDataExtensions
    {
        // Runtime seeding — allows dynamic values (password hash, SecurityStamp, timestamps).
        public static async Task SeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<Entities.User>>();
            var db = services.GetRequiredService<TaskBoardAppDbContext>();

            var conn = db.Database.GetDbConnection();
            await conn.OpenAsync();

            // Acquire application lock so only one instance seeds (SQL Server specific)
            using (var lockCmd = conn.CreateCommand())
            {
                lockCmd.CommandText = "EXEC sp_getapplock @Resource='SeedData', @LockMode='Exclusive', @LockTimeout=10000;";
                await lockCmd.ExecuteNonQueryAsync();
            }

            try
            {
                const string demoEmail = "guest@example.com";
                var demoUser = await userManager.FindByEmailAsync(demoEmail);
                if (demoUser == null)
                {
                    demoUser = new Entities.User
                    {
                        UserName = demoEmail,
                        Email = demoEmail,
                        FirstName = "Guest",
                        LastName = "User",
                        EmailConfirmed = true
                    };

                    // Use a safe default for development. For production, read from config/secret store.
                    await userManager.CreateAsync(demoUser, "P@ssw0rd!");
                }

                // Seed boards only if empty. Model-based HasData may already have created these via migrations.
                if (!await db.Boards.AnyAsync())
                {
                    db.Boards.AddRange(
                        new Entities.Board { Name = "Open" },
                        new Entities.Board { Name = "In Progress" },
                        new Entities.Board { Name = "Done" }
                    );

                    await db.SaveChangesAsync();
                }

                // Seed tasks with dynamic CreatedOn and owner set to the runtime-created user
                if (!await db.Tasks.AnyAsync())
                {
                    db.Tasks.AddRange(
                        new Entities.Task
                        {
                            Title = "Improve CSS styles",
                            Description = "Apply new styling to all pages",
                            CreatedOn = DateTime.UtcNow,
                            BoardId = 1,
                            OwnerId = demoUser.Id
                        },
                        new Entities.Task
                        {
                            Title = "Add seed data",
                            Description = "Implement seed data for boards and tasks",
                            CreatedOn = DateTime.UtcNow,
                            BoardId = 2,
                            OwnerId = demoUser.Id
                        }
                    );

                    await db.SaveChangesAsync();
                }
            }
            finally
            {
                using var releaseCmd = conn.CreateCommand();
                releaseCmd.CommandText = "EXEC sp_releaseapplock @Resource='SeedData';";
                await releaseCmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();
            }
        }
    }
}
