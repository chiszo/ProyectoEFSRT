using EFSRT_RopaStore.Repositorio.Interface;
using EFSRT_RopaStore.Repositorio.RepositorioSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProducto, productoSQL>();
builder.Services.AddSingleton<IProveedor, proveedorSQL>();
builder.Services.AddSingleton<ITrabajador, trabajadorSQL>();
builder.Services.AddSingleton<IArea, areaSQL>();
builder.Services.AddSingleton<ICargo, cargoSQL>();
builder.Services.AddSingleton<ITipoprod, tipoproSQL>();
builder.Services.AddSingleton<ILote, loteSQL>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
