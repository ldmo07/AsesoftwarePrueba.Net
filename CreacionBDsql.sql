
--Creo la bd si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TurnosAsesoftware')
BEGIN
  CREATE DATABASE TurnosAsesoftware;
END;
GO

IF  EXISTS (SELECT * FROM sys.databases WHERE name = 'TurnosAsesoftware')
BEGIN
  Use TurnosAsesoftware
END;
GO



--creo la tabla comercio
CREATE TABLE Comercio (
    id_comercio INT IDENTITY(1,1),
    nom_comercio NVARCHAR (250) NOT NULL,
	aforo_maximo INT DEFAULT 1,
	CONSTRAINT PK_Comercio PRIMARY KEY (id_comercio)
);

--creo la tabla servicio
CREATE TABLE Servicio (
    id_servicio INT IDENTITY(1,1),
	id_comercio INT NOT NULL,
    nom_servicio NVARCHAR (250) NOT NULL,
	hora_apertura TIME NOT NULL,
	hora_cierre TIME NOT NULL,
	duracion TIME,
	CONSTRAINT PK_Servicio PRIMARY KEY (id_servicio),
	CONSTRAINT FK_Factura FOREIGN KEY (id_comercio) REFERENCES Comercio(id_comercio)
);

--creo la tabal turno
CREATE TABLE Turno(
	id_turno INT IDENTITY(1,1),
	id_servicio INT NOT NULL,
	fecha_turno DATETIME NOT NULL,
	hora_inicio TIME NOT NULL,
	hora_fin TIME NOT NULL,
	estado BIT DEFAULT 0,
	CONSTRAINT PK_Turno PRIMARY KEY (id_turno),
	CONSTRAINT FK_Servicio FOREIGN KEY (id_servicio) REFERENCES Servicio(id_servicio)
)



--CRUD COMERCIOS
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	INSERTA UN COMERCIO
-- =============================================
CREATE PROCEDURE SP_INSERTAR_COMERCIO 
	-- Add the parameters for the stored procedure here
	@nom_comercio NVARCHAR (250),
	@aforo_maximo INT 
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Comercio]
           ([nom_comercio]
           ,[aforo_maximo])
     VALUES
           (@nom_comercio
           ,@aforo_maximo)
    
END
GO

	
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	ACTUALIZA UN COMERCIO
-- =============================================
CREATE PROCEDURE SP_ACTUALIZAR_COMERCIO 
	-- Add the parameters for the stored procedure here
	@id_comercio INT,
	@nom_comercio NVARCHAR (250),
	@aforo_maximo INT 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[Comercio]
   SET [nom_comercio] = @nom_comercio
      ,[aforo_maximo] = @aforo_maximo
 WHERE id_comercio = @id_comercio
	
    
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	LISTA UNO O TODOS LOS COMERCIOS

-- =============================================
CREATE PROCEDURE SP_LISTAR_COMERCIO 
	-- Add the parameters for the stored procedure here
	@id_comercio INT = 0
AS
BEGIN
	SET NOCOUNT ON;
	IF(@id_comercio =0)
	BEGIN
		SELECT * FROM [dbo].[Comercio]
	END
	ELSE
	BEGIN
		SELECT * FROM [dbo].[Comercio] WHERE [id_comercio] = @id_comercio
	END
	
    
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	ELIMINA UN COMERCIO

-- =============================================
CREATE PROCEDURE SP_ELIMINAR_COMERCIO 
	-- Add the parameters for the stored procedure here
	@id_comercio INT 
AS
BEGIN
	SET NOCOUNT ON;
	DELETE  FROM [dbo].[Comercio] WHERE [id_comercio] = @id_comercio    
END
GO
--END CRUD COMERCIOS


--CRUD SERVICIOS
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	INSERTA UN SERVICIO
-- =============================================
CREATE PROCEDURE SP_INSERTAR_SERVICIO
	-- Add the parameters for the stored procedure here
	@id_comercio INT,
	@nom_servicio NVARCHAR (250),
	@hora_apertura TIME,
	@hora_cierre TIME,
	@duracion TIME
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Servicio]
           (id_comercio, nom_servicio ,
			hora_apertura , hora_cierre ,
			duracion)
     VALUES
           (@id_comercio, @nom_servicio,
			@hora_apertura,@hora_cierre,
			@duracion)
    
END
GO

	
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	ACTUALIZA UN SERVICIO
-- =============================================
CREATE PROCEDURE SP_ACTUALIZAR_SERVICIO
	-- Add the parameters for the stored procedure here
	@id_servicio INT,
	@id_comercio INT,
	@nom_servicio NVARCHAR (250),
	@hora_apertura TIME,
	@hora_cierre TIME,
	@duracion TIME
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[Servicio]
   SET [id_comercio] = @id_comercio
      ,[nom_servicio] = @nom_servicio
	  ,[hora_apertura] = @hora_apertura
	  ,[hora_cierre] = @hora_cierre
	  ,[duracion] = @duracion
 WHERE id_servicio = @id_servicio
    
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	LISTA UNO O TODOS LOS SERVICIOS

-- =============================================
CREATE PROCEDURE SP_LISTAR_SERVICIO
	-- Add the parameters for the stored procedure here
	@id_servicio INT = 0
AS
BEGIN
	SET NOCOUNT ON;
	IF(@id_servicio =0)
	BEGIN
		SELECT * FROM [dbo].[Servicio]
	END
	ELSE
	BEGIN
		SELECT * FROM [dbo].[Servicio] WHERE [id_servicio] = @id_servicio
	END
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	ELIMINA UN SERVICIO

-- =============================================
CREATE PROCEDURE SP_ELIMINAR_SERVICIO 
	-- Add the parameters for the stored procedure here
	@id_servicio INT 
AS
BEGIN
	SET NOCOUNT ON;
	DELETE  FROM [dbo].[Servicio] WHERE [id_servicio] = @id_servicio    
END
GO
--END CRUD COMERCIOS


--CREO TIPO DE TABLE TURNOS PARA RETORNARLO EN EL SP DE GENERACION DE TURNOS
CREATE TYPE tabla_turno AS TABLE   
    ( [id_turno] INT
    , [id_servicio] INT
	,[fecha_turno] DATETIME
	,[hora_inicio] TIME
	,[hora_fin] TIME
	,[estado] BIT);  


--CRUD TURNOS

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	ACTUALIZA UN TURNO
-- =============================================
CREATE PROCEDURE SP_ACTUALIZAR_TUNO
	-- Add the parameters for the stored procedure here
	@id_turno INT,
	@id_servicio INT,
	@fecha_turno DATETIME,
	@hora_inicio TIME,
	@hora_fin TIME,
	@estado BIT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[Turno]
   SET [id_servicio] = @id_servicio
      ,[fecha_turno] = @fecha_turno
	  ,[hora_inicio] = @hora_inicio
	  ,[hora_fin] = @hora_fin
	  ,[estado] = @estado
 WHERE id_turno = @id_turno
    
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	LISTA UNO O TODOS LOS TURNOS

-- =============================================
CREATE PROCEDURE SP_LISTAR_TURNO
	-- Add the parameters for the stored procedure here
	@id_turno INT = 0
AS
BEGIN
	SET NOCOUNT ON;
	IF(@id_turno =0)
	BEGIN
		SELECT * FROM [dbo].[Turno]
	END
	ELSE
	BEGIN
		SELECT * FROM [dbo].[Turno] WHERE [id_turno] = @id_turno
	END
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORETGA
-- Create date: 26/10/2022
-- Description:	ELIMINA UN TURNO

-- =============================================
CREATE PROCEDURE SP_ELIMINAR_TURNO 
	-- Add the parameters for the stored procedure here
	@id_turno INT 
AS
BEGIN
	SET NOCOUNT ON;
	DELETE  FROM [dbo].[Turno] WHERE [id_turno] = @id_turno    
END
GO

--END CRUD TURNOS











