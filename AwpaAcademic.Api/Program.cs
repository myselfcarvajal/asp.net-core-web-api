using AwpaAcademic.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("AwpaAcademicDB");
builder.Services.AddDbContext<AwpaAcademicDB>(options =>
    options.UseSqlServer(connString));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Hello World!");

app.Run();
