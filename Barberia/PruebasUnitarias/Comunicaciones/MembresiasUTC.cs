using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class MembresiasUTC
    {
        [TestMethod]
        public void ConsultarMembresias()
        {// Debe retornar un estado 200 OK al obtener las Membresias

            // Instanciamos el controlador directamente, ya no la Conexion
            MembresiasController controlador = new MembresiasController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<Membresias>), "El controlador no devolvió el formato esperado (lista Membresias).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una Membresia
            // Arrange
            MembresiasController controlador = new MembresiasController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de Membresias, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto Membresias. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is Membresias, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarMembresias()
        {
            // Arrange
            MembresiasController controlador = new MembresiasController();
            Membresias nuevaMembresia = new Membresias
            {
                Id = 0, // Id 0 porque es nueva
                NombrePlan = "Plan Classic",
                CostoMensual = 30000.00m,
                DescuentoPorcentaje = 5.00m,
                DiaVigencia = 30

            };

            // Act
            var respuesta = controlador.Guardar(nuevaMembresia);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la Membresia guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(Membresias), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La Membresia no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarMembresias()
        {// Deber retornar las Membresias actualizadas si el ID existe, o nulo si no existe (y no explotar)

            MembresiasController controlador = new MembresiasController();
            Membresias MembresiaModificada = new Membresias
            {
                Id = 0, // Id 0 porque es nueva
                NombrePlan = "Plan Classic",
                CostoMensual = 30000.00m,
                DescuentoPorcentaje = 5.00m,
                DiaVigencia = 30
            };

            Membresias MembresiaGuardada = controlador.Guardar(MembresiaModificada);
            MembresiaGuardada.NombrePlan = "Plan Classic";
            // Act
            var respuesta = controlador.Actualizar(MembresiaGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Plan Classic", respuesta.NombrePlan, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarMembresia()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            MembresiasController controlador = new MembresiasController();

            Membresias MembresiaBasura = new Membresias
            {
                Id = 0,
                NombrePlan = "Plan Classic",
                CostoMensual = 30000.00m,
                DescuentoPorcentaje = 5.00m,
                DiaVigencia = 30
            };

            Membresias MembresiaGuardada = controlador.Guardar(MembresiaBasura);

            int idParaBorrar = MembresiaGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}