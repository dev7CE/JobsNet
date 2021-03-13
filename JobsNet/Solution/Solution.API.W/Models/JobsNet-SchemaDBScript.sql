CREATE DATABASE [JobsNet]
GO
USE [JobsNet]
GO
-- ---------------------------------------------------------------------
-- ------------------------------ TABLAS -------------------------------
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ USUARIOS
CREATE TABLE [dbo].[Usuarios](
[UserName] [nvarchar](256) NOT NULL PRIMARY KEY
)
GO
-- ---------------------------------------------------------------------
-- ---------------------------------------------------------- PROVINCIAS
CREATE TABLE [dbo].[Provincias](
[IdProvincia] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[NombreProvincia] [varchar](150) NOT NULL
)
GO
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ CANTONES
CREATE TABLE [dbo].[Cantones](
[IdCanton] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[NombreCanton] [varchar](150) NOT NULL,
[IdProvincia] [int] NOT NULL
)
GO
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ EMPRESAS
CREATE TABLE [dbo].[Empresas](
[IdEmpresa] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[NombreEmpresa] [varchar](150) NOT NULL,
[Descripcion] [varchar](150),
[Telefono] [varchar](150),
[IdCanton] [int],
[UserName] [nvarchar](256) NOT NULL,
)
GO
-- ---------------------------------------------------------------------
-- -------------------------------------------------- PUESTOS DE TRABAJO
CREATE TABLE [dbo].[PuestosTrabajo](
[IdPuesto] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[IdEmpresa] [int] NOT NULL,
[Titulo] [varchar](150) NOT NULL,
[Descripcion] [varchar](150),
[Requisitos] [varchar](150),
[FechaPublicacion] [date] DEFAULT GETDATE(),
[FechaCierre] [date] NOT NULL
)
GO
-- ---------------------------------------------------------------------
-- ----------------------------------------------------------- OFERENTES
CREATE TABLE [dbo].[Oferentes](
[IdOferente] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Nombre] [varchar](150) NOT NULL,
[Apellido1] [varchar](150),
[Apellido2] [varchar](150),
[Telefono] [varchar](150),
[UrlCurriculo] [nvarchar](260),
[UrlFoto] [nvarchar](260),
[UserName] [nvarchar](256) NOT NULL
)
GO
-- ---------------------------------------------------------------------
-- ----------------------------------------------------- LISTA OFERENTES
CREATE TABLE [dbo].[ListaOferentes](
[IdOferente] [int] NOT NULL,
[IdPuesto] [int] NOT NULL,
[Descartado] [bit] DEFAULT 0,
PRIMARY KEY ([IdOferente], [IdPuesto])
)
GO
-- ---------------------------------------------------------------------
-- -------------------------- CLAVES FORANEAS --------------------------
ALTER TABLE [dbo].[Cantones] WITH CHECK 
    ADD CONSTRAINT [FK_PROVINCIAS] 
    FOREIGN KEY([IdProvincia])
    REFERENCES [dbo].[Provincias] ([IdProvincia])
GO
ALTER TABLE [dbo].[Empresas] WITH CHECK 
    ADD CONSTRAINT [FK_USUARIO_EMPRESA] 
    FOREIGN KEY([UserName])
    REFERENCES [dbo].[Usuarios] ([UserName])
GO
ALTER TABLE [dbo].[Empresas] WITH CHECK 
    ADD CONSTRAINT [FK_CANTON] 
    FOREIGN KEY([IdCanton])
    REFERENCES [dbo].[Cantones] ([IdCanton])
GO
ALTER TABLE [dbo].[PuestosTrabajo] WITH CHECK 
    ADD CONSTRAINT [FK_EMPRESA_ID] 
    FOREIGN KEY([IdEmpresa])
    REFERENCES [dbo].[Empresas] ([IdEmpresa])
GO
ALTER TABLE [dbo].[Oferentes] WITH CHECK 
    ADD CONSTRAINT [FK_USUARIO_OFERENTE] 
    FOREIGN KEY([UserName])
    REFERENCES [dbo].[Usuarios] ([UserName])
GO
ALTER TABLE [dbo].[ListaOferentes] WITH CHECK 
    ADD CONSTRAINT [FK_ID_OFERENTE] 
    FOREIGN KEY([IdOferente])
    REFERENCES [dbo].[Oferentes] ([IdOferente])
GO
ALTER TABLE [dbo].[ListaOferentes] WITH CHECK 
    ADD CONSTRAINT [FK_ID_PUESTO] 
    FOREIGN KEY([IdPuesto])
    REFERENCES [dbo].[PuestosTrabajo] ([IdPuesto])
GO
-- ---------------------------------------------------------------------
-- ------------------------ INSERCION DE DATOS -------------------------
-- ---------------------------------------------------------------------
-- ---------------------------------------------------------- PROVINCIAS
INSERT INTO Provincias (NombreProvincia) VALUES ('Alajuela');
INSERT INTO Provincias (NombreProvincia) VALUES ('Cartago');
INSERT INTO Provincias (NombreProvincia) VALUES ('Guanacaste');
INSERT INTO Provincias (NombreProvincia) VALUES ('Heredia');
INSERT INTO Provincias (NombreProvincia) VALUES ('Limon');
INSERT INTO Provincias (NombreProvincia) VALUES ('Puntarenas');
INSERT INTO Provincias (NombreProvincia) VALUES ('San Jose');
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ CANTONES
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('San Ramon', 1);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Atenas', 1);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('La Union', 2);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Paraiso', 2);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Liberia', 3);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('La Cruz', 3);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('San Rafael', 4);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Belen', 4);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Pocosi', 5);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Guacimo', 5);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Esparza', 6);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Buenos Aires', 6);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Guadalupe', 7);
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Montes de Oca', 7);
-- ---------------------------------------------------------------------
-- --------------------------------------------------- USUARIOS IDENTITY
INSERT INTO Usuarios (UserName) VALUES ('recursoshumanos@kcc.com');
INSERT INTO Usuarios (UserName) VALUES ('reclutamiento@correoscr.com');
INSERT INTO Usuarios (UserName) VALUES ('talento@calvinkleinla.com');
INSERT INTO Usuarios (UserName) VALUES ('bmora70466@ufide.ac.cr');
INSERT INTO Usuarios (UserName) VALUES ('lvega70467@ufide.ac.cr');
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ EMPRESAS
INSERT INTO Empresas (UserName, NombreEmpresa, Descripcion, Telefono, IdCanton)
	VALUES ('recursoshumanos@kcc.com', 'Kymberly Clark', 'Productos de aseo', '84781217', 1);
INSERT INTO Empresas (UserName, NombreEmpresa, Descripcion, Telefono, IdCanton)
    VALUES ('reclutamiento@correoscr.com', 'Correos de Costa Rica', 'Servicio Postal', '84781216', 5);
INSERT INTO Empresas (UserName, NombreEmpresa, Descripcion, Telefono, IdCanton)
    VALUES ('talento@calvinkleinla.com', 'Calvin Klein Costa Rica', 'Moda y Estilo de Vida', '84781218', 4);
-- ---------------------------------------------------------------------
-- -------------------------------------------------- PUESTOS DE TRABAJO

-- ---------------------------------------------------------------------
-- ----------------------------------------------------------- OFERENTES
INSERT INTO Oferentes (Nombre, Apellido1, Apellido2, Telefono
    , UrlCurriculo, UrlFoto, UserName)
    VALUES ('Bryan', 'Mora', 'Cascante', '84782021'
    , 'urlCV', 'urlPhoto', 'bmora70466@ufide.ac.cr');
INSERT INTO Oferentes (Nombre, Apellido1, Apellido2, Telefono
    , UrlCurriculo, UrlFoto, UserName)
    VALUES ('Lyam', 'Vega', 'Hernandez', '84782022'
    , 'urlCV', 'urlPhoto', 'lvega70467@ufide.ac.cr');
