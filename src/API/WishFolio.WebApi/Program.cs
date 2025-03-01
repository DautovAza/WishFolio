using Microsoft.EntityFrameworkCore;
using WishFolio.Application.DI;
using WishFolio.Infrastructure.Auth;
using WishFolio.Infrastructure.CORS;
using WishFolio.Infrastructure.Swagger;
using WishFolio.Infrastructure.Dal.Write;
using WishFolio.Infrastructure.DAL.Read;
using WishFolio.Infrastructure.DataSeeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationServices()
    .AddInfrastructureServices()
    .AddWishFolioSwagger()
    .AddJwtAuth(builder.Configuration)
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
    .AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    })
    .AddDbContext<WishFolioContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"));
    })
    .AddReadRepositoriesWithDapper(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();

    await app.UseTestDataAsync();

    app.UseSwagger()
       .UseSwaggerUI();
}
app.UseAuthentication()
   .UseAuthorization()
   .ConfigureCors();

app.MapControllers();

app.Run();
