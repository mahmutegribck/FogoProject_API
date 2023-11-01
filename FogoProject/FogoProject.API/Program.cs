using FogoProject.Business.Categories;
using FogoProject.Business.Products;
using FogoProject.DataAccess;
using FogoProject.DataAccess.Abstract;
using FogoProject.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



builder.Services.AddSwaggerGen(swagger =>
{

    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "FOGO TASK"
    });

});


string serverName = builder.Configuration["ServerName"];
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection").Replace("{ServerName}", serverName);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FogoProjectDBContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
