using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Infrastructure;
using AwpaAcademic.Api.Mappers;
using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Repositories;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
{
    // configure services (DI)
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.AddControllers();

    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserMapper, UserMapper>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();

    builder.Services.AddScoped<IFacultadRepository, FacultadRepository>();

    builder.Services.AddScoped<IPublicacionService, PublicacionService>();
    builder.Services.AddScoped<IPublicacionMapper, PublicacionMapper>();
    builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();

    // add database connection
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AwpaAcademicDbContext>(options =>
        options.UseSqlServer(connString));
}

var app = builder.Build();
{
    app.UseExceptionHandler();
    app.MapControllers();
    app.Run();
}
