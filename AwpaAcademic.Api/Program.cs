using AwpaAcademic.Api.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AwpaAcademicDbContext>(options =>
        options.UseSqlServer(connString));
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.MapGet("/", () => "Hello World!");
    app.Run();
}

