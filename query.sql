-- =============================================
-- CREACI�N DE BASE DE DATOS PROYECTANDO
-- =============================================
CREATE DATABASE proyectando
USE proyectando

-- Tabla Estado
CREATE TABLE Estado (
    id_estado BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion TEXT
);

-- Tabla Ciudad
CREATE TABLE Ciudad (
    id_ciudad BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(200) NOT NULL,
    departamento VARCHAR(200) NOT NULL,
    pais VARCHAR(200) NOT NULL
);

-- Tabla TipoTelefono (para clientes)
CREATE TABLE TipoTelefono (
    id_tipo_telefono BIGINT IDENTITY(1,1) PRIMARY KEY,
    descripcion VARCHAR(100) NOT NULL
);

-- Tabla Cargo
CREATE TABLE Cargo (
    id_cargo BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    activo BIT DEFAULT 1
);

-- Tabla TipoTel (para empleados)
CREATE TABLE TipoTel (
    id_tipo_telefono BIGINT IDENTITY(1,1) PRIMARY KEY,
    descripcion VARCHAR(100) NOT NULL
);

-- Tabla Cliente
CREATE TABLE Cliente (
    documento VARCHAR(50) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(200) NOT NULL,
	activo BIT DEFAULT 1 NOT NULL,
);
ALTER TABLE Cliente
ADD activo BIT DEFAULT 1 NOT NULL;

-- Tabla Direcci�n
CREATE TABLE Direccion (
    id_direccion BIGINT IDENTITY(1,1) PRIMARY KEY,
    documentoCliente VARCHAR(50) NOT NULL,
    id_ciudad BIGINT NOT NULL,
    descripcion VARCHAR(200),
    activo BIT DEFAULT 1,
    FOREIGN KEY (documentoCliente) REFERENCES Cliente(documento),
    FOREIGN KEY (id_ciudad) REFERENCES Ciudad(id_ciudad)
);

-- Tabla Tel�fono
CREATE TABLE Telefono (
    id_telefono BIGINT IDENTITY(1,1) PRIMARY KEY,
	documentoCliente VARCHAR(50) NOT NULL,
    id_tipo_telefono BIGINT,
    numero VARCHAR(50) NOT NULL,
    activo BIT DEFAULT 1,
    FOREIGN KEY (id_tipo_telefono) REFERENCES TipoTelefono(id_tipo_telefono),
	FOREIGN KEY (documentoCliente) REFERENCES Cliente(documento)
);

-- Tabla Empleado
CREATE TABLE Empleado (
    documento VARCHAR(50) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL,
    telefono VARCHAR(20),
    id_cargo BIGINT,
    id_tipo_telefono BIGINT,
    esUsuario BIT NOT NULL,
    FOREIGN KEY (id_cargo) REFERENCES Cargo(id_cargo),
    FOREIGN KEY (id_tipo_telefono) REFERENCES TipoTel(id_tipo_telefono)
);

-- Tabla Usuario
CREATE TABLE Usuario (
    id_usuario BIGINT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    activo BIT DEFAULT 0,
    documentoEmp VARCHAR(50) NOT NULL,
    FOREIGN KEY (documentoEmp) REFERENCES Empleado(documento)
);

-- Tabla Proyecto
CREATE TABLE Proyecto (
    id_proyecto BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    descripcion TEXT,
    fecha_inicio DATE NOT NULL,
    fecha_fin_estim DATE NOT NULL,
    id_estado BIGINT,
    documentoCliente VARCHAR(50) NOT NULL,
    FOREIGN KEY (id_estado) REFERENCES Estado(id_estado),
    FOREIGN KEY (documentoCliente) REFERENCES Cliente(documento)
);

-- Tabla Proceso
CREATE TABLE Proceso (
    id_proceso BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    activo BIT DEFAULT 1
);

-- Tabla Fase
CREATE TABLE Fase (
    id_fase BIGINT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    orden INT,
    activo BIT DEFAULT 1
);

-- Tabla FaseProceso
CREATE TABLE FaseProceso (
    id_fase_proceso BIGINT IDENTITY(1,1) PRIMARY KEY,
    id_fase BIGINT NOT NULL,
    id_proceso BIGINT NOT NULL,
    activo BIT,
    FOREIGN KEY (id_fase) REFERENCES Fase(id_fase),
    FOREIGN KEY (id_proceso) REFERENCES Proceso(id_proceso)
);

-- Tabla ProyectoFase
CREATE TABLE ProyectoFase (
    id_proyecto_fase BIGINT IDENTITY(1,1) PRIMARY KEY,
    id_faseProceso BIGINT NOT NULL,
    id_proyecto BIGINT NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE,
    FOREIGN KEY (id_faseProceso) REFERENCES FaseProceso(id_fase_proceso),
    FOREIGN KEY (id_proyecto) REFERENCES Proyecto(id_proyecto)
);

-- Tabla Control
CREATE TABLE Control (
    id_control BIGINT IDENTITY(1,1) PRIMARY KEY,
    documentoEmpleado VARCHAR(50) NOT NULL,
    fecha DATE NOT NULL,
    observaciones TEXT NOT NULL,
    FOREIGN KEY (documentoEmpleado) REFERENCES Empleado(documento)
);

-- Tabla DetalleControl
CREATE TABLE DetalleControl (
    idDetalleControl BIGINT IDENTITY(1,1) PRIMARY KEY,
    id_control BIGINT NOT NULL,
    id_fase_proceso BIGINT NOT NULL,
    horas INT NOT NULL,
    comentarios TEXT,
    FOREIGN KEY (id_control) REFERENCES Control(id_control),
    FOREIGN KEY (id_fase_proceso) REFERENCES FaseProceso(id_fase_proceso)
);

-- =============================================
-- INSERTS DE DATOS EST�TICOS
-- =============================================
-- Ciudades
INSERT INTO Ciudad (nombre, departamento, pais) VALUES
('Bogot�', 'Cundinamarca', 'Colombia'),
('Medell�n', 'Antioquia', 'Colombia'),
('Cali', 'Valle del Cauca', 'Colombia'),
('Barranquilla', 'Atl�ntico', 'Colombia'),
('Cartagena', 'Bol�var', 'Colombia'),
('Bucaramanga', 'Santander', 'Colombia'),
('Pereira', 'Risaralda', 'Colombia'),
('Manizales', 'Caldas', 'Colombia'),
('Santa Marta', 'Magdalena', 'Colombia'),
('C�cuta', 'Norte de Santander', 'Colombia');


-- Estados de proyectos
INSERT INTO Estado (nombre, descripcion) VALUES
('Sin Planear', 'Proyecto sin Planear'),
('Planeado', 'Proyecto planeado'),
('En ejecuci�n', 'Proyecto en desarrollo'),
('Finalizado', 'Proyecto concluido');

-- Tipos de tel�fono para cliente
INSERT INTO TipoTelefono (descripcion) VALUES
('Celular'),
('Fijo'),
('Oficina');

-- Tipos de tel�fono para empleado
INSERT INTO TipoTel (descripcion) VALUES
('Celular personal'),
('Tel�fono corporativo');

-- Cargos
INSERT INTO Cargo (nombre, descripcion) VALUES
('Desarrollador Junior', 'Encargado de tareas b�sicas de desarrollo'),
('Analista', 'Responsable del an�lisis funcional y t�cnico'),
('L�der de Proyecto', 'Responsable de la planificaci�n y seguimiento del proyecto'),
('Administrador', 'Administrador del Sitio'),
('Jefe de �rea', 'Responsable del un sector de la organizaci�n');

-- Fases del ciclo de vida del software
INSERT INTO Fase (nombre, orden) VALUES
('Levantamiento de requerimientos', 1),
('An�lisis y dise�o de la soluci�n', 2),
('Construcci�n (desarrollo)', 3),
('Pruebas', 4),
('Producci�n', 5);

-- Procesos gen�ricos
INSERT INTO Proceso (nombre, descripcion) VALUES
('Reuni�n con cliente', 'Entrevistas, cuestionarios u observaci�n directa'),
('Modelado de datos', 'Dise�o de base de datos'),
('Programaci�n de backend', 'Desarrollo de l�gica del servidor'),
('Pruebas unitarias', 'Verificaci�n de funciones individuales'),
('Pruebas de integraci�n', 'Pruebas de componentes en conjunto');


-- Empleado y usuario Administrador
INSERT INTO Empleado (documento, nombre, apellido, email, telefono, id_cargo, id_tipo_telefono, esUsuario) VALUES
('103610361036', 'Juan', 'Bedoya', 'localhost@localhost', '3232297800', 4, 1, 1);

INSERT INTO Usuario (username, password, activo, documentoEmp) VALUES
('jjbedoya', 'Adm1n2025', 1, '103610361036')

-- Funciones utiles
--DELETE FROM Direccion WHERE documentoCliente = '123456789'
--DELETE FROM Telefono WHERE documentoCliente = '123456789'
--DELETE FROM Cliente WHERE documento = '123456789'