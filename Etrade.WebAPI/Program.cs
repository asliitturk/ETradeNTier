using Etrade.DAL.Abstract;
using Etrade.DAL.Conscreate;
using Etrade.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddDbContext<EtradeContext>(); //EtradeContext tipi için DbContext'in eklenmesi = Uygulanamanın yaşam süresi boyunca kullanılcak olan 'EtradeContext' tipinin DbContext sınıfını ekler.Genellikler veritabanı işlemleri için kullanılır.
builder.Services.AddScoped<ICategoryDAL, CategoryDAL>();// 'ICategoryDAL' interface ile 'CategoryDAL' sınıfını ilişkilendirir ve her bi istemci isteği için bir kapsamda (scope) bir örneği oluşturur.Yani,her bir istemci isteği için ayrı bir 'CategoryDAL' örneği oluşturulur.
builder.Services.AddScoped<IProductDAL, ProductDAL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
