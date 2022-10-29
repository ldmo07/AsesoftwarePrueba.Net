USE [TurnosAsesoftware]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LUIS DAVID MERCADO ORTEGA
-- Create date: 26/10/2022
-- Description:	GENERA TURNOS ALEATORIOS
-- =============================================
CREATE PROCEDURE [dbo].[SP_GENERAR_TURNO]
	-- Add the parameters for the stored procedure here
	@id_servicio INT,
	@fecha_inicio DATETIME,
	@fecha_fin DATETIME,
	@tabla_turnos NVARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
    --capturo la hora incio  hora fin y duracion de la tabla de servicios
	 DECLARE @hora_apertura TIME  = (SELECT TOP(1) hora_apertura FROM  Servicio WHERE id_servicio = @id_servicio)
	 DECLARE @hora_cierre TIME  = (SELECT TOP(1) hora_cierre FROM  Servicio WHERE id_servicio = @id_servicio)
	 DECLARE @duracion TIME  = (SELECT TOP(1) duracion FROM  Servicio WHERE id_servicio = @id_servicio)	

	 --calculo el numero de dias
	 DECLARE @numero_dias INT = (SELECT DATEDIFF(DAY,@fecha_inicio,@fecha_fin)+1);
	 print @numero_dias
	 --calculo la diferencia en minutos entre la hora apertura y hora cierre
     DECLARE @diferencia_minutos INT = DATEDIFF(MINUTE,@hora_apertura,@hora_cierre);

	 --calculo el numero de turnos por dia
	 DECLARE @numero_turno_por_dia  INT;
	 SET @numero_turno_por_dia = ( (@diferencia_minutos) /(SELECT SUM(DATEDIFF(MINUTE,0,cast(@duracion AS DATETIME)))) );

	 --recorro el numero de dias
	 DECLARE @i INT = 0;
	 DECLARE @j INT = 0;

	 -- while dias
		WHILE @i < @numero_dias
		BEGIN
		PRINT 'dia = ' + CONVERT(VARCHAR,@i)
			--while turnos
				WHILE @j < @numero_turno_por_dia
				BEGIN
					IF(@j=0)
						BEGIN
							--convierto el time a segundo y luego sumo el total y lo convierto en time nuevamente
							DECLARE @hora_apertura_min  INT=  (SELECT SUM(DATEDIFF(MINUTE,0,cast(@hora_apertura AS DATETIME))))
							DECLARE @hora_cierre_min  INT=  (SELECT SUM(DATEDIFF(MINUTE,0,cast(@hora_cierre AS DATETIME))))
							DECLARE @duracion_min_1  INT=  (SELECT SUM(DATEDIFF(MINUTE,0,cast(@duracion AS DATETIME))))
							--DECLARE @hora_cierre_1 TIME = (SELECT CAST(DATEADD(MINUTE,(@hora_cierre_min + @hora_apertura_min), 0) AS TIME))
							DECLARE @hora_cierre_1 TIME = (SELECT CAST(DATEADD(MINUTE,(@duracion_min_1 + @hora_apertura_min), 0) AS TIME))

							--INSERTO PRIMER TURNO	
							INSERT INTO [dbo].[Turno]
							   ([id_servicio]
							   ,[fecha_turno]
							   ,[hora_inicio]
							   ,[hora_fin]
							   ,[estado])
						 VALUES
							   (@id_servicio
							   ,@fecha_inicio
							   ,@hora_apertura
							   ,@hora_cierre_1
							   ,0)
						END
					ELSE
						BEGIN
							--OBTENGO LA HORA FIN DEL ULTIMO TURNO
							DECLARE @ultima_hora_fin TIME = (SELECT TOP (1) hora_fin  FROM Turno ORDER BY id_turno DESC)
							--CONVIERTO LOS TIME EN INT Y LE SUMO LA DURACION PARA OBTENER LA HORA  FIN
							DECLARE @ultima_hora_fin_min  INT=  (SELECT SUM(DATEDIFF(MINUTE,0,cast(@ultima_hora_fin AS DATETIME))))
							DECLARE @duracion_min  INT=  (SELECT SUM(DATEDIFF(MINUTE,0,cast(@duracion AS DATETIME))))
							DECLARE @hora_fin_2 TIME = (SELECT CAST(DATEADD(MINUTE,(@ultima_hora_fin_min + @duracion_min), 0) AS TIME))
							--INSERTO LOS DEMAS TURNOS
							INSERT INTO [dbo].[Turno]
							   ([id_servicio]
							   ,[fecha_turno]
							   ,[hora_inicio]
							   ,[hora_fin]
							   ,[estado])
						 VALUES
							   (@id_servicio
							   ,@fecha_inicio
							   ,@ultima_hora_fin
							   ,@hora_fin_2
							   ,0)
						END

					SET @j = @j + 1;
				END
			--end while turnos
			
			--reseteo el iterador de turnos por dia 
			SET @j=0; 
			--VOY SUMADO DIAS A LA FECHA INICIAL
			SET @fecha_inicio = (SELECT DATEADD(DAY,1,@fecha_inicio))
			SET @i = @i + 1;
			PRINT 'The counter value is = ' + CONVERT(VARCHAR,@i)
		END
		--end while dias
		
		--SALIDA DE LOS TURNOS INSERTADOS EN FORMATO JSON
		SET @tabla_turnos = ( SELECT TOP(@numero_dias*@numero_turno_por_dia) 
		id_turno,id_servicio,fecha_turno,hora_inicio,hora_fin,estado  
		FROM Turno FOR JSON PATH)

END