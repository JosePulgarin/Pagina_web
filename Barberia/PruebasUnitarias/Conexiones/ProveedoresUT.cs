using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class ProveedoresUT
    {
        [TestMethod]
        public void ConsultarProveedores() // Debe retornar una lista de Proveedores (aunque esté vacía)
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Proveedores>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Proveedor O Nulo
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Proveedores);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Proveedorr en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarProveedores() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            Proveedores ProveedorPasada = new Proveedores
            {
                Id = 0,
                
                Nombre = "Jhonson",
                NombreEmpresa = "Company elegance",
                Correo = "marta@quimicos.com", // Repetido a propósito para probar validación de correo
                Telefono = "123456733030"


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(ProveedorPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarProveedoresExitoso() // POLICÍA BUENO
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            Proveedores ProveedorValida = new Proveedores
            {
                Id = 0,
                Nombre = "Jhonson",
                NombreEmpresa = "Company elegance",
                Correo = "jhon@quimicos.com", 
                Telefono = "123456733030"
            };

            var respuesta = negocio.Guardar(ProveedorValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Proveedor válida no se guardó.");
        }
        

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarProveedores() //Debe Modificar Y Retornar
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();

            // Creamos una Proveedor VÁLIDA (con fecha de mañana para que no explote tu validación)
            Proveedores temporal = new Proveedores
            {
                Id = 0,
                Nombre = "Jhonson",
                NombreEmpresa = "Company elegance",
                Correo = "jhon@quimicos.com",
                Telefono = "123456733030"
            };
            Proveedores guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.Telefono = "202002";
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual("202002", respuesta.Telefono);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarProveedores() //Debe Retornar True
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();

            // Creamos basura temporal VÁLIDA
            Proveedores basura = new Proveedores
            {
                Id = 0,
                Nombre = "Jhonson",
                NombreEmpresa = "Company elegance",
                Correo = "jhon@quimicos.com",
                Telefono = "123456733030"
            };
            Proveedores guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}