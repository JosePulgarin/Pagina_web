using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// 1. Le decimos a la API que vamos a usar Controladores (Tu AcademiasController)
builder.Services.AddControllers().AddJsonOptions(opciones =>
{
    opciones.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; // Esto mantiene los nombres de las propiedades tal cual los escribiste en tus clases
});


// 2. ¡EL INGREDIENTE MÁGICO PARA SWAGGER! (Esto faltaba en tus fotos)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ========================================================================
// EL ESCUDO ANTI-BORRADOS
// ========================================================================
using (var scope = app.Services.CreateScope())
{
    using (var contexto = new LibreriaBarberia.Implementaciones.Conexion())
    {
        contexto.string_conexion = LibreriaBarberia.Nucleo.Configuraciones.obtener("string_conexion");

        // EnsureCreated devuelve 'true' SI LA BD SE ACABA DE CREAR NUEVA
        contexto.Database.EnsureCreated();

   
    }
}


// 3. Le decimos a la app que dibuje la interfaz visual (Solo cuando la corras)
app.UseSwagger();
app.UseSwaggerUI();

// 4. Configuraciones de seguridad y ruteo estándar
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 

