CREATE DATABASE [JobsNet]
GO
USE [jobsnet]
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
[IdProvincia] [nchar](50) NOT NULL PRIMARY KEY,
[NombreProvincia] [varchar](150) NOT NULL
)
GO
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ CANTONES
CREATE TABLE [dbo].[Cantones](
[IdCanton] [numeric](10, 0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
[NombreCanton] [varchar](150) NOT NULL,
[IdProvincia] [nchar](50) NOT NULL
)
GO
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ EMPRESAS
CREATE TABLE [dbo].[Empresas](
[IdEmpresa] [numeric](18, 0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
[NombreEmpresa] [varchar](150) NOT NULL,
[Descripcion] [varchar](150),
[Telefono] [varchar](150),
[IdCanton] [numeric](10, 0),
[UserName] [nvarchar](256) NOT NULL,
)
GO
-- ---------------------------------------------------------------------
-- -------------------------------------------------- PUESTOS DE TRABAJO
CREATE TABLE [dbo].[PuestosTrabajo](
[IdPuesto] [numeric](18, 0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
[IdEmpresa] [numeric](18, 0) NOT NULL,
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
[IdOferente] [numeric](18, 0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
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
[IdOferente] [numeric](18, 0) NOT NULL,
[IdPuesto] [numeric](18, 0) NOT NULL,
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
INSERT INTO Provincias (IdProvincia, NombreProvincia) VALUES ('ALJ', 'Alajuela');
INSERT INTO Provincias (IdProvincia, NombreProvincia) VALUES ('CAR', 'Cartago');
INSERT INTO Provincias (IdProvincia, NombreProvincia) VALUES ('GUA', 'Guanacaste');
INSERT INTO Provincias (IdProvincia, NombreProvincia) VALUES ('HER', 'Heredia');
INSERT INTO Provincias (IdProvincia, NombreProvincia) VALUES ('LIM', 'Limon');
INSERT INTO Provincias (IdProvincia, NombreProvincia) VALUES ('PUN', 'Puntarenas');
INSERT INTO Provincias (IdProvincia, NombreProvincia) VALUES ('SJO', 'San Jose');
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ CANTONES
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('San Ramon', 'ALJ');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Atenas', 'ALJ');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('La Union', 'CAR');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Paraiso', 'CAR');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Liberia', 'GUA');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('La Cruz', 'GUA');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('San Rafael', 'HER');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Belen', 'HER');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Pocosi', 'LIM');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Guacimo', 'LIM');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Esparza', 'PUN');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Buenos Aires', 'PUN');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Guadalupe', 'SJO');
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('Montes de Oca', 'SJO');
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
