using Microsoft.EntityFrameworkCore;
using WishFolio.Infrastructure.Auth;
using WishFolio.Infrastructure.Dal;
using WishFolio.Infrastructure.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddWishFolioSwagger();
builder.Services.AddHttpContextAccessor();

builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddDbContext<WishFolioContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WishFolioContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
