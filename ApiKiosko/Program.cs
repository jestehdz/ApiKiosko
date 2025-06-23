using ApiKiosko.Context;
using ApiKiosko.Interfaces;
using ApiKiosko.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// Add services to the container.
//crar variable para la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("Connection");
//Registar servicio para la conexion.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IProductos,IProductosMapper>();
builder.Services.AddScoped<IClientes,IClientesMapper>();
builder.Services.AddScoped<IVentas,IVentasMapper>();
builder.Services.AddScoped<IInvoice, IInvoiceMapper>();
builder.Services.AddControllers();
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


// Usa la política de CORS
app.UseCors("AllowAll");

app.MapControllers();

app.Run();


