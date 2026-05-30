using PresentacionesAspnetcoreBarberia.Services;
using PresentacionesLibreriaBarberia.Interfaces;
using PresentacionesLibreriaBarberia.Implementaciones; // importar las carpetas de mi puente


var builder = WebApplication.CreateBuilder(args); // Add services to the container.



builder.Services.AddRazorPages();

builder.Services.AddScoped<IComunicaciones, Comunicaciones>(); // Registramos la interfaz y su implementación para comunicaciones

builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.AgendasService>(); // Registramos nuestro servicio moderno de Agendas
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.BarberosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.CategoriasProductosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.ClientesService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.ComisionesService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.FacturasService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.GastosOperativosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.HistoricosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.HorariosLaboralesService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.InventariosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.MembresiasService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.MetodosPagoService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.PerfilUsuariosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.PortafoliosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.ProductosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.PromocionesEspecialesService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.PromocionesServiciosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.ProveedoresService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.RecepcionistasService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.ReseñasClientesService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.ReservasService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.ReservasServiciosService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.RolesService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.SedesService>();
builder.Services.AddScoped<PresentacionesAspnetcoreBarberia.Services.ServiciosService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();