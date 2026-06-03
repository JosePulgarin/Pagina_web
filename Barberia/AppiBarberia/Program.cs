using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using System.IO; // Para leer el archivo Script.sql
=======
<<<<<<< HEAD
using System.IO; // Para leer el archivo Script.sql
=======

>>>>>>> 7a211c30954c5185a1af436a7a13b3f477101c47
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042

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
<<<<<<< HEAD
// EL ESCUDO ANTI-BORRADOS + AUTO RELLENADO DE DATOS
=======
<<<<<<< HEAD
// EL ESCUDO ANTI-BORRADOS + AUTO RELLENADO DE DATOS
=======
// EL ESCUDO ANTI-BORRADOS
>>>>>>> 7a211c30954c5185a1af436a7a13b3f477101c47
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
// ========================================================================
using (var scope = app.Services.CreateScope())
{
    using (var contexto = new LibreriaBarberia.Implementaciones.Conexion())
    {
        contexto.string_conexion = LibreriaBarberia.Nucleo.Configuraciones.obtener("string_conexion");

        // EnsureCreated devuelve 'true' SI LA BD SE ACABA DE CREAR NUEVA
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
        bool bdRecienCreada = contexto.Database.EnsureCreated();

        // Si la BD es nuevecita, le disparamos el script de una
        if (bdRecienCreada)
        {
            string rutaScript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "script.sql");

            if (File.Exists(rutaScript))
            {
                // 1. Leemos su archivo COMPLETO tal cual lo tiene
                string scriptCompleto = File.ReadAllText(rutaScript);

                // 2. Buscamos en qué momento empiezan los datos reales
                int indicePrimerInsert = scriptCompleto.IndexOf("INSERT INTO", StringComparison.OrdinalIgnoreCase);

                if (indicePrimerInsert != -1)
                {
                    // 3. Cortamos el texto en memoria (desde el primer INSERT hasta el final)
                    // Así evitamos los CREATE TABLE sin modificar su archivo físico
                    string scriptSoloDatos = scriptCompleto.Substring(indicePrimerInsert);

                    // 4. Lo disparamos limpio a la base de datos
                    contexto.Database.ExecuteSqlRaw(scriptSoloDatos);
                }
            }
        }


<<<<<<< HEAD
=======
=======
        contexto.Database.EnsureCreated();

   
>>>>>>> 7a211c30954c5185a1af436a7a13b3f477101c47
>>>>>>> 112295c9af2e932238842f9f0beda5235c10b042
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

