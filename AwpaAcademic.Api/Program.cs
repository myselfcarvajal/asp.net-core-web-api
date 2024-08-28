using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Infrastructure;
using AwpaAcademic.Api.Mappers;
using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Repositories;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
{
    // configure services (DI)
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.AddControllers();

    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserMapper, UserMapper>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();

    builder.Services.AddScoped<IFacultadService, FacultadService>();
    builder.Services.AddScoped<IFacultadMapper, FacultadMapper>();
    builder.Services.AddScoped<IFacultadRepository, FacultadRepository>();

    builder.Services.AddScoped<IPublicacionService, PublicacionService>();
    builder.Services.AddScoped<IPublicacionMapper, PublicacionMapper>();
    builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();

    // add database connection
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AwpaAcademicDbContext>(options =>
        options.UseSqlServer(connString));

    // configure Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "AwpaAcademic API",
            Version = "v1",
            Description = "API para AwpaAcademic"
        });
    });
}

var app = builder.Build();
{
    // config pipeline Swagger
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "AwpaAcademic API v1");
            c.RoutePrefix = string.Empty;
        });
    }
    app.UseExceptionHandler();
    app.MapControllers();
    app.Run();
}
