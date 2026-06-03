using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class MembresiasUT
    {
        [TestMethod]
        public void ConsultarMembresias() // Debe retornar una lista de Membresias (aunque esté vacía)
        {
            MembresiasNegocio negocio = new MembresiasNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Membresias>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Membresia O Nulo
        {
            MembresiasNegocio negocio = new MembresiasNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Membresias);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Membresiar en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarMembresias() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            MembresiasNegocio negocio = new MembresiasNegocio();
            Membresias MembresiaPasada = new Membresias
            {
                Id = 0,
                NombrePlan = "Golden barber",
                CostoMensual = 3000.0m,
                DescuentoPorcentaje = 110.0m,
                DiaVigencia = 20


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(MembresiaPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarMembresiasExitoso() // POLICÍA BUENO
        {
            MembresiasNegocio negocio = new MembresiasNegocio();
            Membresias MembresiaValida = new Membresias
            {
                Id = 0,
                NombrePlan = "Golden barber",
                CostoMensual = 3000.0m,
                DescuentoPorcentaje = 30.0m,
                DiaVigencia = 20
            };

            var respuesta = negocio.Guardar(MembresiaValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Membresia válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarMembresias() //Debe Modificar Y Retornar
        {
            MembresiasNegocio negocio = new MembresiasNegocio();

            // Creamos una Membresia VÁLIDA (con fecha de mañana para que no explote tu validación)
            Membresias temporal = new Membresias
            {
                Id = 0,
                NombrePlan = "Golden barber",
                CostoMensual = 3000.0m,
                DescuentoPorcentaje = 30.0m,
                DiaVigencia = 20
            };
            Membresias guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.DescuentoPorcentaje = 0.50m;
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual( 0.50m, respuesta.DescuentoPorcentaje);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarMembresias() //Debe Retornar True
        {
            MembresiasNegocio negocio = new MembresiasNegocio();

            // Creamos basura temporal VÁLIDA
            Membresias basura = new Membresias
            {
                Id = 0,
                NombrePlan = "Golden barber",
                CostoMensual = 3000.0m,
                DescuentoPorcentaje = 110.0m,
                DiaVigencia = 20
            };
            Membresias guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}