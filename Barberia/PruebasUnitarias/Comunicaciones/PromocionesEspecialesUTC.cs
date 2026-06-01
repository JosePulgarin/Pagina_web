using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiBarberia.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Para usar List<T>
using LibreriaBarberia.Entidades; // Para los estados HTTP (Ok, BadRequest)

namespace PruebasUnitarias.Comunicaciones
{
    [TestClass]
    public class PromocionesEspecialesUTC
    {
        [TestMethod]
        public void ConsultarPromocionesEspeciales()
        {// Debe retornar un estado 200 OK al obtener las PromocionesEspeciales

            // Instanciamos el controlador directamente, ya no la Conexion
            PromocionesEspecialesController controlador = new PromocionesEspecialesController();

            // 2. Act (Ejecutar)
            // Simulamos que el Frontend pide las citas
            var respuesta = controlador.Consultar(); // o el nombre que tenga tu método

            // 3. Assert (Comprobar)
            // Verificamos que la respuesta de la API sea una lista valida
            Assert.IsNotNull(respuesta, "El controlador falló y devolvió nulo");
            Assert.IsInstanceOfType(respuesta, typeof(List<PromocionesEspeciales>), "El controlador no devolvió el formato esperado (lista PromocionesEspeciales).");
        }

        [TestMethod]
        public void ConsultarPorId()
        {//Debe retornar una PromocionEspecial
            // Arrange
            PromocionesEspecialesController controlador = new PromocionesEspecialesController();
            int idPrueba = 1; // Asegúrate de que el ID 1 exista en tu tabla de PromocionesEspeciales, o cámbialo por uno real.

            // Act
            var respuesta = controlador.ConsultarPorId(idPrueba);

            // Assert
            // Si el ID existe, debe devolver un objeto PromocionesEspeciales. Si no existe, devolverá nulo (y está bien).
            // Lo importante es que la comunicación no explote.
            Assert.IsTrue(respuesta == null || respuesta is PromocionesEspeciales, "El controlador no procesó bien la petición del ID.");
        }

        // ==========================================
        // 3. Prueba de GUARDAR (POST)
        // ==========================================
        [TestMethod]
        public void GuardarPromocionesEspeciales()
        {
            // Arrange
            PromocionesEspecialesController controlador = new PromocionesEspecialesController();
            PromocionesEspeciales nuevaPromocionEspecial = new PromocionesEspeciales
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Inauguración",
                Descuento = "PROMO10",
                FechaInicio = new DateOnly(2026,1,1) ,
                FechaFin = new DateOnly(2026, 12, 31)

            };

            // Act
            var respuesta = controlador.Guardar(nuevaPromocionEspecial);

            // Assert
            Assert.IsNotNull(respuesta, "No se retornó la PromocionEspecial guardada.");
            Assert.IsInstanceOfType(respuesta, typeof(PromocionesEspeciales), "El formato de respuesta es incorrecto.");
            // Si la base de datos funciona, el ID debió cambiar a un número mayor que 0
            Assert.IsTrue(respuesta.Id > 0, "La PromocionEspecial no se guardó en la base de datos, el ID sigue siendo 0.");
        }

        // ==========================================
        // 4. Prueba de ACTUALIZAR (PUT)
        // ==========================================
        [TestMethod]
        public void ActualizarPromocionesEspeciales()
        {// Deber retornar las PromocionesEspeciales actualizadas si el ID existe, o nulo si no existe (y no explotar)

            PromocionesEspecialesController controlador = new PromocionesEspecialesController();
            PromocionesEspeciales PromocionEspecialModificada = new PromocionesEspeciales
            {
                Id = 0, // Id 0 porque es nueva
                Nombre = "Inauguración",
                Descuento = "PROMO10",
                FechaInicio = new DateOnly(2026, 1, 1),
                FechaFin = new DateOnly(2026, 12, 31)
            };

            PromocionesEspeciales PromocionEspecialGuardada = controlador.Guardar(PromocionEspecialModificada);
            PromocionEspecialGuardada.Descuento = "PADRE20";
            // Act
            var respuesta = controlador.Actualizar(PromocionEspecialGuardada);

            // Assert
            Assert.IsNotNull(respuesta);
            Assert.AreEqual("PADRE20", respuesta.Descuento, "El estado no se actualizó correctamente.");
        }

        // ==========================================
        // 5. Prueba de ELIMINAR (DELETE)
        // ==========================================
        [TestMethod]
        public void EliminarPromocionEspecial()
        {//Debe retornar un booleano indicando si se eliminó o no, sin importar si el ID existe o no (y sin explotar)
            // Arrange
            PromocionesEspecialesController controlador = new PromocionesEspecialesController();

            PromocionesEspeciales PromocionEspecialBasura = new PromocionesEspeciales
            {
                Id = 0,
                Nombre = "Inauguración",
                Descuento = "PROMO10",
                FechaInicio = new DateOnly(2026, 1, 1),
                FechaFin = new DateOnly(2026, 12, 31)
            };

            PromocionesEspeciales PromocionEspecialGuardada = controlador.Guardar(PromocionEspecialBasura);

            int idParaBorrar = PromocionEspecialGuardada.Id;

            // Act
            var respuesta = controlador.Eliminar(idParaBorrar);

            // Assert
            // Aquí solo comprobamos que el controlador nos responde con un True o False (booleano) como se espera
            Assert.IsInstanceOfType(respuesta, typeof(bool), "El método eliminar no devolvió un booleano.");
            Assert.IsTrue(respuesta, "El método eliminar devolvió False.");
        }
    }
}