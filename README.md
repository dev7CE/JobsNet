# JobsNet

JobsNet es un sistema de empleabilidad, en el que empleadores puedan publicar sus ofertas de empleo y oferentes puedan postularse a dichas publicaciones respectivamente. Inicialmente se pretenden los siguientes módulos:

- Registro: Es el modulo que permite dar de alta a un usuario. Consiste en ingresar un correo electrónico, una contraseña, además de realizar una selección entre Empleador y Oferente.
- Inicio de Sesión: Es el encargo de autenticar al usuario mediante su correo electrónico y contraseña.
- Perfil: Este espacio muestra la información básica del oferente o empresa,
dependiendo del tipo rol (Oferente o Empresa). Además, puede asignar una foto de perfil a su usuario (aplica para empresa y oferente).
- Trabajados Publicados: Este espacio se limita a los empleadores, para que los mismos supervisen las publicaciones realizadas. Cuando la publicación se observa en detalle, se debe enlistar los candidatos que aplicaron a la oferta.
- Ofertas De Trabajo: Se enlistan las ofertas publicadas por los empleadores a los que los oferentes pueden aplicar. Se puede ingresar a un puesto, se detalla el puesto y se muestra la opción de postularse para el oferente. El oferente puede ver a qué puestos se ha postulado desde el modulo de Perfil.


## Para Empezar

Verifique que cumple con los Pre-requisitos y posteriormente siga las indicaciones de instalacion ubicadas a continuación.

### Pre-requisitos

Los requisitos minimos para ejecutar esta aplicacion son los siguientes: 
- [Visual Studio 2019](https://visualstudio.microsoft.com/es/vs/) o cualquier editor de texto como [Visual Studio Code](https://code.visualstudio.com/) (requiere el uso del .Net CLI).
- [ASP.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1).
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Intalacion y Ejecucion

+ Instale el SDK y la Base de Datos SQL Server y compruebe su correcto funcionamiento.
+ Ejecute el Script alojado en ['Solution/Solution.API.W/Models'](https://github.com/dev7CE/JobsNet/blob/master/JobsNet/Solution/Solution.API.W/Models/JobsNet-SchemaDBScript.sql) en el servidor SQL instalado.
+ Modifique los strings de conexión ubicados en ['Solution.FrontEnd/Solution.FrontEnd/appsettings.json'](https://github.com/dev7CE/JobsNet/blob/master/JobsNet/Solution.FrontEnd/Solution.FrontEnd/appsettings.json) y ['Solution/Solution.API/appsettings.json'](https://github.com/dev7CE/JobsNet/blob/master/JobsNet/Solution/Solution.API/appsettings.json) de acuerdo al del servidor instalado.
+ Ejecute alguno de los siguientes comandos de 'Actualizar Base de Datos' en ['Solution.FrontEnd/Solution.FrontEnd'](https://github.com/dev7CE/JobsNet/tree/master/JobsNet/Solution.FrontEnd/Solution.FrontEnd) según el entorno en que esté:

Visual Studio 2019:

    Update-Database

.NET CLI:

    dotnet tool install --global dotnet-aspnet-codegenerator --version 3.1.4
    dotnet ef migrations add InitialCreate
    dotnet ef database update

## Autores

  - **Byron Leal** -
    [dev7CE](https://github.com/dev7CE)
  - **Valery Sanabria** -
    [Valery2508](https://github.com/Valery2508)
