CREATE DATABASE db_barberia; --Creación de la base de datos
GO
USE db_barberia;
GO

---------------------- TABLAS ------------------------

CREATE TABLE Sedes(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(30) NOT NULL,
	[Direccion] NVARCHAR(30) NOT NULL,
	[Ciudad] NVARCHAR(30) NOT NULL,
	[Correo] NVARCHAR(40) NOT NULL UNIQUE
);

CREATE TABLE PerfilUsuarios(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Correo] NVARCHAR(40) NOT NULL,
	[Contraseña] NVARCHAR(30) NOT NULL,
	[Rol] NVARCHAR(30) NOT NULL,
	[Estado] NVARCHAR(30) NOT NULL
);

CREATE TABLE Servicios(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(30) NOT NULL,
	[Costo] DECIMAL(18,2) NOT NULL CHECK(Costo>0),
	[Tiempo] INT NOT NULL,
	[Nota] NVARCHAR(200) NOT NULL
);

CREATE TABLE MetodosPago(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[TipoMetodo] NVARCHAR(30) NOT NULL,
	[Banco] NVARCHAR(30) NOT NULL,
	[Moneda] NVARCHAR(30) NOT NULL
);

CREATE TABLE Proveedores(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(30) NOT NULL,
	[NombreEmpresa] NVARCHAR(30) NOT NULL,
	[Correo] NVARCHAR(40) NOT NULL,
	[Telefono] NVARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE PromocionesEspeciales(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(30) NOT NULL,
	[Descuento] NVARCHAR(30) NOT NULL UNIQUE,
	[FechaInicio] DATE NOT NULL,
	[FechaFin] DATE NOT NULL
);

CREATE TABLE Barberos(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(30) NOT NULL,
	[Correo] NVARCHAR(40) NOT NULL,
	[FechaNacimiento] DATE NOT NULL,
	[Especialidad] NVARCHAR(30) NOT NULL,
	[Biografia] NVARCHAR(200) NOT NULL,
	[IdUsuario] int, -- FK de Perfil usuario
	[IdSede] int, -- FK de Sedes
	FOREIGN KEY (IdUsuario) REFERENCES PerfilUsuarios(Id),
	FOREIGN KEY (IdSede) REFERENCES Sedes(Id)
);

CREATE TABLE Recepcionistas(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(30),
	[FechaNacimiento] Date NOT NULL,
	[Turno] NVARCHAR(30) NOT NULL,
	[Telefono] NVARCHAR(30) NOT NULL UNIQUE,
	[IdUsuario] int, -- FK  de Perfil de usuarios
	[IdSede] int, -- FK de Sedes
	FOREIGN KEY (IdUsuario) REFERENCES PerfilUsuarios(Id),
	FOREIGN KEY (IdSede) REFERENCES Sedes(Id)
);

CREATE TABLE Clientes(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(30) NOT NULL,
	[Telefono] NVARCHAR(30) NOT NULL UNIQUE,
	[Correo] NVARCHAR(40) NOT NULL UNIQUE,
	[IdUsuario] int, -- FK De perfil usuarios
	[IdSede] int, -- FK de Sedes
	FOREIGN KEY (IdUsuario) REFERENCES PerfilUsuarios(Id),
	FOREIGN KEY (IdSede) REFERENCES Sedes(Id)
);

CREATE TABLE HorariosLaborales(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Dia] NVARCHAR(30) NOT NULL,
	[HoraApertura] TIME NOT NULL,
	[HoraCierre] TIME NOT NULL,
	[DiaFestivo] BIT NOT NULL DEFAULT 0,
	[IdSede] int, -- FK de sedes
	FOREIGN KEY (IdSede) REFERENCES Sedes(Id)
);

CREATE TABLE GastosOperativos(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Categoria] NVARCHAR(30) NOT NULL,
	[Monto] Decimal(18,2) NOT NULL,
	[FechaPago] DATE NOT NULL,
	[NumeroComprobante] NVARCHAR(10) NOT NULL,
	[IdSede] int, --FK de la sede
	FOREIGN KEY (IdSede) REFERENCES Sedes(Id)
);

CREATE TABLE Inventarios(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(30) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[CantidadActual] int NOT NULL,
	[FechaAbastecimiento] DATE NOT NULL,
	[IdSede] int, -- FK de sedes
	FOREIGN KEY (IdSede) REFERENCES Sedes(Id)
);

CREATE TABLE PromocionesServicios(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[MontoDescuento] DECIMAL(18,2) NOT NULL,
	[DescuentoFinde] DECIMAL(18,2) NOT NULL,
	[IdServicio] int, -- FK de servicios
	[IdPromocionEspecial] int, -- FK de promociones especiales
	FOREIGN KEY (IdServicio) REFERENCES Servicios(Id),
	FOREIGN KEY (IdPromocionEspecial) REFERENCES PromocionesEspeciales(Id)
);

CREATE TABLE Agendas(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Fecha] DATE NOT NULL,
	[Hora] TIME NOT NULL,
	[Estado] NVARCHAR(30) NOT NULL CHECK (Estado IN('Pendiente', 'Disponible', 'Ocupado')),
	[IdBarbero] int, -- FK  de barberos
	FOREIGN KEY (IdBarbero) REFERENCES Barberos(Id)
);

CREATE TABLE Productos(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[MarcaProducto] NVARCHAR(30) NOT NULL,
	[NombreArticulo] NVARCHAR(30) NOT NULL,
	[Precio] DECIMAL(18,2) NOT NULL,
	[StockActual] int NOT NULL,
	[IdInventario] int, -- FK de inventarios
	[IdProveedor] int, -- FK de proveedores
	FOREIGN KEY (IdInventario) REFERENCES Inventarios(id),
	FOREIGN KEY (IdProveedor) REFERENCES Proveedores(id)
);

CREATE TABLE Reservas(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Recordatorio] NVARCHAR(30) NOT NULL,
	[Fecha] DATE NOT NULL,
	[Estado] NVARCHAR(30) NOT NULL,
	[Notas] NVARCHAR(200) NOT NULL,
	[IdAgenda] int, -- FK de agendas
	[IdCliente] int, -- FK de clientes
	FOREIGN KEY (IdAgenda) REFERENCES Agendas(Id),
	FOREIGN KEY (IdCliente) REFERENCES Clientes(Id)
);

CREATE TABLE ReseñasClientes(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Puntuacion] INT NOT NULL CHECK(Puntuacion>=1 AND Puntuacion<=5),
	[Comentario] NVARCHAR(200),
	[FechaPublicacion] DATE NOT NULL,
	[Etiquetas] NVARCHAR(30) NOT NULL,
	[IdReserva] int, -- FK de Reservas
	FOREIGN KEY (IdReserva) REFERENCES Reservas(Id)
);

CREATE TABLE ReservasServicios(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[Precio] DECIMAL(18,2) NOT NULL,
	[Observacion] NVARCHAR(200) NOT NULL,
	[IdServicio] int, -- FK a servicios
	[IdReserva] int, -- FK a reservas
	FOREIGN KEY (IdServicio) REFERENCES Servicios(Id),
	FOREIGN KEY (IdReserva) REFERENCES Reservas(Id)
);

CREATE TABLE Facturas(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[NumeroFactura] NVARCHAR(30) NOT NULL,
	[MontoSubtotal] DECIMAL(18,2) NOT NULL,
	[IVA] DECIMAL(3,2) NOT NULL,
	[Total] DECIMAL(18,2) NOT NULL,
	[IdReserva] int, -- FK pa reservas
	[IdMetodo] int, -- FK a Métodos de pago
	FOREIGN KEY (IdReserva) REFERENCES Reservas(Id),
	FOREIGN KEY (IdMetodo) REFERENCES MetodosPago(Id)
);

CREATE TABLE Comisiones(
	[Id] int PRIMARY KEY IDENTITY(1,1),
	[PorcentajeAplicado] DECIMAL(3,2) NOT NULL,
	[Monto] DECIMAL(18,2) NOT NULL,
	[Fecha] DATE NOT NULL,
	[EstadoLiquidacion] NVARCHAR(30) NOT NULL CHECK (EstadoLiquidacion IN('Pendiente','Liquidado','Anulado')),
	[IdFactura] int, --FK a Facturas
	[IdBarbero] int, -- FK a Barberos
	FOREIGN KEY (IdFactura) REFERENCES Facturas(Id),
	FOREIGN KEY (IdBarbero) REFERENCES Barberos(Id)
);

USE db_barberia;
GO

---------------------------------------------------
-- 1. SEDES
---------------------------------------------------
INSERT INTO Sedes (Nombre, Direccion, Ciudad, Correo) VALUES 
('Elite Central', 'Cl. 10 #43-20', 'Medellín', 'central@barberia.com'),
('Norte Premium', 'Av. 45 #12-30', 'Bello', 'norte@barberia.com'),
('Poblado Real', 'Cra 30 #5-10', 'Medellín', 'poblado@barberia.com'),
('Laureles Style', 'Circular 4 #70-15', 'Medellín', 'laureles@barberia.com'),
('Envigado Classic', 'Cl. 33 Sur #40', 'Envigado', 'envigado@barberia.com');

---------------------------------------------------
-- 2. PERFIL USUARIOS
---------------------------------------------------
INSERT INTO PerfilUsuarios (Correo, Contraseña, Rol, Estado) VALUES 
('admin@barberia.com', 'Admin2026!', 'Administrador', 'Activo'),
('carlos.barber@mail.com', 'Pass123', 'Barbero', 'Activo'),
('recepcion.norte@mail.com', 'Recepcion99', 'Recepcionista', 'Activo'),
('juan.cliente@mail.com', 'Juanito88', 'Cliente', 'Activo'),
('mario.barber@mail.com', 'MarioCorte1', 'Barbero', 'Activo');

---------------------------------------------------
-- 3. SERVICIOS
---------------------------------------------------
INSERT INTO Servicios (Nombre, Costo, Tiempo, Nota) VALUES 
('Corte Clásico', 25000.00, 30, 'Corte tradicional a tijera o máquina'),
('Barba VIP', 15000.00, 20, 'Toalla caliente y perfilado'),
('Combo Elite', 35000.00, 50, 'Corte + Barba + Hidratación'),
('Colorimetría', 80000.00, 120, 'Tinte completo o rayos'),
('Limpieza Facial', 20000.00, 25, 'Exfoliación y mascarilla negra');

---------------------------------------------------
-- 4. METODOS DE PAGO
---------------------------------------------------
INSERT INTO MetodosPago (TipoMetodo, Banco, Moneda) VALUES 
('Efectivo', 'N/A', 'COP'),
('Transferencia', 'Bancolombia', 'COP'),
('Tarjeta Crédito', 'Visa', 'COP'),
('Digital', 'Nequi', 'COP'),
('Digital', 'Daviplata', 'COP');

---------------------------------------------------
-- 5. PROVEEDORES
---------------------------------------------------
INSERT INTO Proveedores (Nombre, NombreEmpresa, Correo, Telefono) VALUES 
('Andrés Giraldo', 'Distribuidora Belleza', 'ventas@distribelleza.com', '3001112233'),
('Marta Lucía', 'Químicos Premium', 'marta@quimicos.com', '3004445566'),
('Jorge Ruiz', 'Herramientas Pro', 'jorge@herrapro.com', '3107778899'),
('Sofía Cano', 'Insumos Antioquia', 'sofia@insumos.com', '3152223344'),
('Pedro Nel', 'Mobiliario Barber', 'pedro@mobiliario.com', '3205556677');

---------------------------------------------------
-- 6. PROMOCIONES ESPECIALES
---------------------------------------------------
INSERT INTO PromocionesEspeciales (Nombre, Descuento, FechaInicio, FechaFin) VALUES 
('Inauguración', 'PROMO10', '2026-01-01', '2026-12-31'),
('Black Friday', 'BLACK50', '2026-11-20', '2026-11-30'),
('Día del Padre', 'PADRE20', '2026-06-01', '2026-06-30'),
('Amigo Fiel', 'FIEL15', '2026-02-01', '2026-12-31'),
('Madrugador', 'EARLY05', '2026-01-01', '2026-06-01');

---------------------------------------------------
-- 7. BARBEROS (Relacionados con Perfil 2 y 5, Sedes 1-5)
---------------------------------------------------
INSERT INTO Barberos (Nombre, Correo, FechaNacimiento, Especialidad, Biografia, IdUsuario, IdSede) VALUES 
('Carlos Pérez', 'carlos@mail.com', '1990-05-15', 'Degradados', 'Experto en faders urbanos', 2, 1),
('Mario Ruiz', 'mario@mail.com', '1985-08-20', 'Barbas', 'Maestro de la navaja clásica', 5, 2),
('Luis Cano', 'luis@mail.com', '1992-12-10', 'Tijeras', 'Especialista en cortes largos', 1, 3),
('Andrés Blade', 'andres@mail.com', '1998-01-25', 'Diseño', 'Artista de hair tattoos', 1, 4),
('Samuel Gold', 'samuel@mail.com', '1994-03-30', 'Infantil', 'Paciencia y estilo para niños', 1, 5);

---------------------------------------------------
-- 8. RECEPCIONISTAS
---------------------------------------------------
INSERT INTO Recepcionistas (Nombre, FechaNacimiento, Turno, Telefono, IdUsuario, IdSede) VALUES 
('Daniela Henao', '1996-06-12', 'Mañana', '3009990011', 3, 1),
('Paola Ortiz', '1994-02-10', 'Tarde', '3009990022', 1, 2),
('Juliana Soler', '1997-11-05', 'Mañana', '3009990033', 1, 3),
('Elena Gómez', '1993-08-15', 'Tarde', '3009990044', 1, 4),
('Claudia Marín', '1995-04-22', 'Mañana', '3009990055', 1, 5);

---------------------------------------------------
-- 9. CLIENTES
---------------------------------------------------
INSERT INTO Clientes (Nombre, Telefono, Correo, IdUsuario, IdSede) VALUES 
('Juan Pérez', '3110001111', 'juanp@mail.com', 4, 1),
('Mateo López', '3110002222', 'mateol@mail.com', 1, 2),
('Sebastian Gil', '3110003333', 'sebas@mail.com', 1, 3),
('Ricardo Arjona', '3110004444', 'ricardo@mail.com', 1, 4),
('Daniel Restrepo', '3110005555', 'danielr@mail.com', 1, 5);

---------------------------------------------------
-- 10. HORARIOS LABORALES
---------------------------------------------------
INSERT INTO HorariosLaborales (Dia, HoraApertura, HoraCierre, DiaFestivo, IdSede) VALUES 
('Lunes', '08:00:00', '20:00:00', 0, 1),
('Martes', '08:00:00', '20:00:00', 0, 1),
('Sábado', '09:00:00', '21:00:00', 0, 2),
('Domingo', '10:00:00', '16:00:00', 1, 1),
('Viernes', '08:00:00', '22:00:00', 0, 3);

---------------------------------------------------
-- 11. GASTOS OPERATIVOS
---------------------------------------------------
INSERT INTO GastosOperativos (Categoria, Monto, FechaPago, NumeroComprobante, IdSede) VALUES 
('Arriendo', 2500000.00, '2026-04-01', 'ARR-001', 1),
('Servicios', 450000.00, '2026-04-05', 'PUB-050', 2),
('Internet', 120000.00, '2026-04-02', 'INT-99', 3),
('Limpieza', 85000.00, '2026-04-10', 'LIM-12', 4),
('Marketing', 300000.00, '2026-04-15', 'ADV-44', 5);

---------------------------------------------------
-- 12. INVENTARIOS
---------------------------------------------------
INSERT INTO Inventarios (Nombre, Descripcion, CantidadActual, FechaAbastecimiento, IdSede) VALUES 
('Capa Barbero', 'Capas negras impermeables', 20, '2026-03-15', 1),
('Gel Afeitar', 'Gel transparente 500ml', 15, '2026-03-20', 2),
('Cuchillas', 'Cuchillas doble filo caja x100', 50, '2026-03-10', 3),
('After Shave', 'Loción hidratante refrescante', 12, '2026-03-25', 4),
('Papel Cuello', 'Rollos de papel protector', 100, '2026-03-01', 5);

---------------------------------------------------
-- 13. PROMOCIONES SERVICIOS
---------------------------------------------------
INSERT INTO PromocionesServicios (MontoDescuento, DescuentoFinde, IdServicio, IdPromocionEspecial) VALUES 
(5000.00, 0.00, 1, 1),
(10000.00, 5000.00, 3, 2),
(2000.00, 0.00, 2, 4),
(15000.00, 0.00, 4, 3),
(1000.00, 500.00, 5, 5);

---------------------------------------------------
-- 14. AGENDAS (Citas disponibles para los barberos)
---------------------------------------------------
INSERT INTO Agendas (Fecha, Hora, Estado, IdBarbero) VALUES 
('2026-04-10', '08:00:00', 'Ocupado', 1),
('2026-04-10', '09:00:00', 'Ocupado', 2),
('2026-04-10', '10:00:00', 'Disponible', 1),
('2026-04-11', '14:00:00', 'Disponible', 3),
('2026-04-11', '15:00:00', 'Ocupado', 4);

---------------------------------------------------
-- 15. PRODUCTOS
---------------------------------------------------
INSERT INTO Productos (MarcaProducto, NombreArticulo, Precio, StockActual, IdInventario, IdProveedor) VALUES 
('Loreal', 'Cera Mate', 45000.00, 10, 1, 1),
('Wahl', 'Aceite Máquina', 25000.00, 5, 3, 3),
('Gillette', 'Espuma Sensitive', 18000.00, 20, 2, 2),
('Nivea', 'Bálsamo Barba', 32000.00, 8, 4, 4),
('Elegance', 'Gel Extra Strong', 28000.00, 15, 2, 5);

---------------------------------------------------
-- 16. RESERVAS
---------------------------------------------------
INSERT INTO Reservas (Recordatorio, Fecha, Estado, Notas, IdAgenda, IdCliente) VALUES 
('WhatsApp', '2026-04-10', 'Confirmada', 'Cliente puntual', 1, 1),
('SMS', '2026-04-10', 'Pendiente', 'Trae niño para corte', 2, 2),
('Email', '2026-04-11', 'Cancelada', 'No puede asistir', 5, 3),
('WhatsApp', '2026-04-12', 'Confirmada', 'Solo barba', 3, 4),
('Ninguno', '2026-04-13', 'Pendiente', 'Sin observaciones', 4, 5);

---------------------------------------------------
-- 17. RESEÑAS CLIENTES
---------------------------------------------------
INSERT INTO ReseñasClientes (Puntuacion, Comentario, FechaPublicacion, Etiquetas, IdReserva) VALUES 
(5, 'Excelente atención de Carlos', '2026-04-10', 'Excelente Servicio', 1),
(4, 'Buen corte, local limpio', '2026-04-10', 'Higiene Impecable', 2),
(5, 'El mejor diseño que me han hecho', '2026-04-11', 'Corte Perfecto', 4),
(3, 'Se demoraron un poco', '2026-04-12', 'Mucha Espera', 1),
(5, 'Precio justo por la calidad', '2026-04-13', 'Precio Justo', 5);

---------------------------------------------------
-- 18. RESERVAS SERVICIOS
---------------------------------------------------
INSERT INTO ReservasServicios (Precio, Observacion, IdServicio, IdReserva) VALUES 
(25000.00, 'Sin shampoo', 1, 1),
(35000.00, 'Combo completo', 3, 2),
(15000.00, 'Solo perfilado', 2, 4),
(80000.00, 'Color cenizo', 4, 3),
(20000.00, 'Mascarilla premium', 5, 5);

---------------------------------------------------
-- 19. FACTURAS
---------------------------------------------------
INSERT INTO Facturas (NumeroFactura, MontoSubtotal, IVA, Total, IdReserva, IdMetodo) VALUES 
('FAC-1001', 21008.40, 0.19, 25000.00, 1, 1),
('FAC-1002', 29411.76, 0.19, 35000.00, 2, 2),
('FAC-1003', 12605.04, 0.19, 15000.00, 4, 4),
('FAC-1004', 67226.89, 0.19, 80000.00, 3, 3),
('FAC-1005', 16806.72, 0.19, 20000.00, 5, 5);

---------------------------------------------------
-- 20. COMISIONES (50% del subtotal aprox)
---------------------------------------------------
INSERT INTO Comisiones (PorcentajeAplicado, Monto, Fecha, EstadoLiquidacion, IdFactura, IdBarbero) VALUES 
(0.50, 10504.20, '2026-04-10', 'Pendiente', 1, 1),
(0.50, 14705.88, '2026-04-10', 'Liquidado', 2, 2),
(0.50, 6302.52, '2026-04-11', 'Pendiente', 3, 4),
(0.50, 33613.44, '2026-04-11', 'Anulado', 4, 3),
(0.50, 8403.36, '2026-04-12', 'Pendiente', 5, 1);



