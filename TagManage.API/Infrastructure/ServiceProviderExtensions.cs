using Microsoft.EntityFrameworkCore;
using TagManage.Data;
using TagManage.Data.Entities;
using TagManage.Domain.Authentication;
using TagManage.Domain.Command;



namespace MediManage.App.Helpers;

public static class ServiceProviderExtensions
{
    public static async Task PrepareDatabase(this IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var tagCommand = scope.ServiceProvider.GetRequiredService<TagCommand>();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();

        //add seed data here
        await SeedData(dbContext);
        await tagCommand.UpdateTagFromStackBase();
    }

    private static async Task SeedData(AppDbContext dbContext)
    {
        dbContext.Users.AddRange(GetUsers());
        await dbContext.SaveChangesAsync();
    }


    private static UserEntity[] GetUsers() =>
        new[]
        {
            new UserEntity
            {
                Id = Guid.Parse("e8ba6b3f-3e6f-4578-8e0a-af6aec08ad77"),
                Username = "admin",
                PasswordHash = PasswordHasher.HashPassword("admin")
            },
            new UserEntity
            {
                Id = Guid.Parse("d6eb20f7-8837-4b98-8ea6-ba95331473e3"),
                Username = "standard",
                PasswordHash = PasswordHasher.HashPassword("standard")
            }
        };
}


