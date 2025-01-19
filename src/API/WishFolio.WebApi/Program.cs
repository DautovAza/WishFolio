using Microsoft.EntityFrameworkCore;
using WishFolio.Infrastructure.Auth;
using WishFolio.Infrastructure.Dal;
using WishFolio.Infrastructure.Swagger;
using WishFolio.Application.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddInfrastructureServices()
    .AddWishFolioSwagger()
    .AddJwtAuth(builder.Configuration)
    .AddDbContext<WishFolioContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
