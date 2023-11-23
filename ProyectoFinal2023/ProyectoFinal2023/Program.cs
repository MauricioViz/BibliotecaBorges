using ProyectoFinal2023.Services.Interface;
using ProyectoFinal2023.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Add(new ServiceDescriptor(typeof(IUsuario), new UsuarioRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(ICliente), new ClienteRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(ILibro), new LibroRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(IAutor), new AutorRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(IEditorial), new EditorialRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(IPrestamo), new PrestamoRepository()));

builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
}); //Agregar sesiones al proyecto

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Index}/{id?}");

app.Run();
