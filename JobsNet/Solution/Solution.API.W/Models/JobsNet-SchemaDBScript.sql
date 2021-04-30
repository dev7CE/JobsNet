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
-- --------------------------------------------------- DOCUMENTOS (CV's)
CREATE TABLE [dbo].[Documentos](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[UserName] [nvarchar](256) NOT NULL, 
	[Guid] [varchar](MAX) NOT NULL,
	[FileContent] varbinary(MAX) NOT NULL, 
	[Type] [varchar](256) NOT NULL
)
GO
-- ---------------------------------------------------------------------
-- ----------------------------------------------------- FOTOS DE PERFIL
CREATE TABLE [dbo].[FotosPerfil](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[UserName] [nvarchar](256) NOT NULL, 
    [Guid] [varchar](MAX) NOT NULL, 
	[FileContent] varbinary(MAX) NOT NULL, 
	[Type] [varchar](256) NOT NULL 
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
ALTER TABLE [dbo].[Documentos] WITH CHECK 
    ADD CONSTRAINT [FK_USUARIO_DOCUEMNTO] 
    FOREIGN KEY([UserName])
    REFERENCES [dbo].[Usuarios] ([UserName])
GO
ALTER TABLE [dbo].[FotosPerfil] WITH CHECK 
    ADD CONSTRAINT [FK_USUARIO_FOTOPERFIL] 
    FOREIGN KEY([UserName])
    REFERENCES [dbo].[Usuarios] ([UserName])
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
-- SAN JOSE ------------------------------------------------------------
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SAN JOSE', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ESCAZU', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('DESAMPARADOS', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('PURISCAL', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('TARRAZU', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ASERRI', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('MORA', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('GOICOECHEA', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SANTA ANA', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ALAJUELITA', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('CORONADO', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ACOSTA', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('TIBAS', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('MORAVIA', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('MONTES DE OCA', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('TURRUBARES', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('DOTA', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('CURRIDABAT', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('PEREZ ZELEDON', 7)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('LEON CORTES', 7)
-- ALAJUELA -----------------------------------------------------------
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ALAJUELA', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SAN RAMON', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES (' GRECIA', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SAN MATEO', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ATENAS', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('NARANJO', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('PALMARES', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('POAS', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('OROTINA', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SAN CARLOS', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ALFARO RUIZ', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('VALVERDE VEGA', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('UPALA', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('LOS CHILES', 1)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('GUATUSO', 1)
-- CARTAGO ------------------------------------------------------------
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('CARTAGO', 2)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('PARAISO', 2)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('LA UNION', 2)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('JIMENEZ', 2)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('TURRIALBA', 2)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ALVARADO', 2)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('OREAMUNO', 2)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('EL GUARCO', 2)
-- HEREDIA ------------------------------------------------------------
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('HEREDIA', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('BARVA', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SANTO DOMINGO', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SANTA BARBARA', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SAN RAFAEL', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SAN ISIDRO', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('BELEN', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('FLORES', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SAN PABLO', 4)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SARAPIQUI', 4)
-- GUANACASTE ---------------------------------------------------------
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('LIBERIA', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('NICOYA', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SANTA CRUZ', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('BAGACES', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('CARRILLO', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('CAÑAS', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ABANGARES', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('TILARAN', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('NANDAYURE', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('LA CRUZ', 3)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('HOJANCHA', 3)
-- PUNTARENAS ---------------------------------------------------------
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('PUNTARENAS', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('ESPARZA', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('BUENOS AIRES', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('MONTES DE ORO', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('OSA', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('AGUIRRE', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('GOLFITO', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('COTO BRUS', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('PARRITA', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('CORREDORES', 6)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('GARABITO', 6)
-- LIMON --------------------------------------------------------------
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('LIMON', 5)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('POCOCI', 5)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('SIQUIRRES', 5)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('TALAMANCA', 5)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('MATINA', 5)
INSERT INTO Cantones (NombreCanton, IdProvincia) VALUES ('GUACIMO', 5)
-- ---------------------------------------------------------------------
-- --------------------------------------------------- USUARIOS IDENTITY
-- INSERT INTO Usuarios (UserName) VALUES ('recursoshumanos@kcc.com');
-- INSERT INTO Usuarios (UserName) VALUES ('reclutamiento@correoscr.com');
-- INSERT INTO Usuarios (UserName) VALUES ('talento@calvinkleinla.com');
-- INSERT INTO Usuarios (UserName) VALUES ('bmora70466@ufide.ac.cr');
-- INSERT INTO Usuarios (UserName) VALUES ('lvega70467@ufide.ac.cr');
-- ---------------------------------------------------------------------
-- ------------------------------------------------------------ EMPRESAS
-- INSERT INTO Empresas (UserName, NombreEmpresa, Descripcion, Telefono, IdCanton)
-- 	VALUES ('recursoshumanos@kcc.com', 'Kymberly Clark', 'Productos de aseo', '84781217', 1);
-- INSERT INTO Empresas (UserName, NombreEmpresa, Descripcion, Telefono, IdCanton)
--     VALUES ('reclutamiento@correoscr.com', 'Correos de Costa Rica', 'Servicio Postal', '84781216', 5);
-- INSERT INTO Empresas (UserName, NombreEmpresa, Descripcion, Telefono, IdCanton)
--     VALUES ('talento@calvinkleinla.com', 'Calvin Klein Costa Rica', 'Moda y Estilo de Vida', '84781218', 4);
-- ---------------------------------------------------------------------
-- -------------------------------------------------- PUESTOS DE TRABAJO

-- ---------------------------------------------------------------------
-- ----------------------------------------------------------- OFERENTES
-- INSERT INTO Oferentes (Nombre, Apellido1, Apellido2, Telefono
--     , UrlCurriculo, UrlFoto, UserName)
--     VALUES ('Bryan', 'Mora', 'Cascante', '84782021'
--     , 'urlCV', 'facedefault.png', 'bmora70466@ufide.ac.cr');
-- INSERT INTO Oferentes (Nombre, Apellido1, Apellido2, Telefono
--     , UrlCurriculo, UrlFoto, UserName)
--     VALUES ('Lyam', 'Vega', 'Hernandez', '84782022'
--     , 'urlCV', 'facedefault.png', 'lvega70467@ufide.ac.cr');
