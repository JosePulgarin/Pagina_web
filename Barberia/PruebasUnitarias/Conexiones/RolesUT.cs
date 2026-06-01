using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class RolesUT
    {
        [TestMethod]
        public void ConsultarRoles() // Debe retornar una lista de Roles (aunque esté vacía)
        {
            RolesNegocio negocio = new RolesNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Roles>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Rol O Nulo
        {
            RolesNegocio negocio = new RolesNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Roles);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Rolr en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarRoles() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            RolesNegocio negocio = new RolesNegocio();
            Roles RolPasada = new Roles
            {
                Id = 0,
                
                Nombre = "Administrador Lopez",
                Descripcion = "Acceso a todo",
                Estado = true,
                FechaCreacion = new DateTime(2027, 04, 02) // Esta en el futuro


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(RolPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarRolesExitoso() // POLICÍA BUENO
        {
            RolesNegocio negocio = new RolesNegocio();
            Roles RolValida = new Roles
            {
                Id = 0,
                Nombre = "Administrador Lopez",
                Descripcion = "Acceso a todo",
                Estado = true,
                FechaCreacion = new DateTime(2023, 04, 02)
            };

            var respuesta = negocio.Guardar(RolValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Rol válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarRoles() //Debe Modificar Y Retornar
        {
            RolesNegocio negocio = new RolesNegocio();

            // Creamos una Rol VÁLIDA (con fecha de mañana para que no explote tu validación)
            Roles temporal = new Roles
            {
                Id = 0,
                Nombre = "Administrador Lopez",
                Descripcion = "Acceso a todo",
                Estado = true,
                FechaCreacion = new DateTime(2023, 04, 02)
            };
            Roles guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.FechaCreacion = guardada.FechaCreacion.AddDays(5);
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(guardada.FechaCreacion.AddDays(5), respuesta.FechaCreacion);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarRoles() //Debe Retornar True
        {
            RolesNegocio negocio = new RolesNegocio();

            // Creamos basura temporal VÁLIDA
            Roles basura = new Roles
            {
                Id = 0,
                Nombre = "Administrador Lopez",
                Descripcion = "Acceso a todo",
                Estado = true,
                FechaCreacion = new DateTime(2023, 04, 02)
            };
            Roles guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}