

using BiblioAPI.Services.Interface;
using BiblioAPI.Services.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Add(new ServiceDescriptor(typeof(ILibro), new LibroRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(IAutor), new AutorRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(IEditorial), new EditorialRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(IPrestamo), new PrestamoRepository()));


// Add services to the container.

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

app.MapControllers();

app.Run();
