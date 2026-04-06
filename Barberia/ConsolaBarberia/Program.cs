using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;

Console.WriteLine("cns_presentacion");
Console.WriteLine("Conexion de Base de datos");

IConexion conexion = new Conexion();
conexion.StringConexion = "server=DESKTOP-BJQKKO0\\SQLEXPRESS;Integrated Security=True;TrustServerCertificate=true;database=db_barberia;";
  var lista_sedes = conexion.Sedes!.ToList();
  var lista_PerfilUsuarios = conexion.PerfilUsuarios!.ToList();
  var lista_Servicios = conexion.Servicios!.ToList();
  var lista_MetodosPago = conexion.MetodosPago!.ToList();
  var lista_Proveedores = conexion.Proveedores!.ToList();
  var lista_PromocionesEspeciales = conexion.PromocionesEspeciales!.ToList();
  var lista_Barberos = conexion.Barberos!.ToList();
  var lista_Recepcionistas = conexion.Recepcionistas!.ToList();
  var lista_Clientes = conexion.Clientes!.ToList();
  var lista_HorariosLaborales = conexion.HorariosLaborales!.ToList();
  var lista_GastosOperativos = conexion.GastosOperativos!.ToList();
  var lista_Inventarios = conexion.Inventarios!.ToList();
  var lista_PromocionesServicios = conexion.PromocionesServicios!.ToList();
  var lista_Agendas = conexion.Agendas!.ToList();
  var lista_Productos = conexion.Productos!.ToList();
  var lista_Reservas = conexion.Reservas!.ToList();
  var lista_ReseñasClientes = conexion.ReseñasClientes!.ToList();
  var lista_ReservasServicios = conexion.ReservasServicios!.ToList();
  var lista_Facturas = conexion.Facturas!.ToList();
  var lista_Comisiones = conexion.Comisiones!.ToList();
  
 
Console.WriteLine("\n Sedes:");
foreach (var item in lista_sedes) // Recorre cada elemento de la lista de sedes obtenida de la base de datos
    Console.WriteLine("\n Id: " + item.Id + " Nombre: " + item.Nombre);



Console.WriteLine("\n");

Console.WriteLine("Final");