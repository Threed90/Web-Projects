using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Entities;

namespace TaskBoardApp.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Board>().HasData(
                new Board { Id = 1, Name = "Open" },
                new Board { Id = 2, Name = "In Progress" },
                new Board { Id = 3, Name = "Done" }
            );

            builder.Entity<TaskBoardApp.Data.Entities.Task>()
                .Property(t => t.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}