using Merchandise.Application.Interfaces;
using Merchandise.Application.Services;
using Merchandise.Domain.DomainServices;
using Merchandise.Domain.Extensions;
using Merchandise.Domain.Interfaces.DomainServices;
using Merchandise.Domain.Interfaces.Queries;
using Merchandise.Domain.Interfaces.Repositories;
using Merchandise.Infrastructure.Persistences;
using Merchandise.Infrastructure.Queries;
using Merchandise.Infrastructure.Repositories;
using Merchandise.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<MerchandiseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MerchandiseDb")));

//builder.Services.AddDbContext<MerchandiseDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MerchandiseDb")));

// For Upload image
builder.Services.AddHttpContextAccessor();

// Application Service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBrandService, BrandService>();

// Domain Service
builder.Services.AddScoped<IProductDomainService, ProductDomainService>();
builder.Services.AddScoped<ICategoryDomainService, CategoryDomainService>();
builder.Services.AddScoped<IBrandDomainService, BrandDomainService>();

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();

// Queries
builder.Services.AddScoped<IProductQuery, ProductQuery>();

//builder.Services.AddControllers();
// To Accept Non Standard DateTimeOffet
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new FlexibleDateTimeOffsetConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// This line is telling the app to serve the wwwroot folder
app.UseStaticFiles();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    TransactionService.ApplyPendingMigrations(scope.ServiceProvider);
}

app.Run();
