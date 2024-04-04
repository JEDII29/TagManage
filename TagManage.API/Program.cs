using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TagManage.Data;
using TagManage.Domain.Command;
using TagManage.Domain.ExternalApp;
using TagManage.Domain.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LocalConnection")));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TagManage", Version = "v1" });
});

builder.Services.AddScoped<IExternalApi, ExternalApiService>();
builder.Services.AddScoped<TagCommand>();
builder.Services.AddScoped<TagQuery>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TagManage");
        c.RoutePrefix = string.Empty;
    });

app.UseAuthorization();

app.MapControllers();
app.Run();
