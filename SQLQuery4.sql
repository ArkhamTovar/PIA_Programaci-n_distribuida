CREATE PROC spConsultaLibro
(@P_Accion TINYINT,
@P_Texto VARCHAR(15))
AS
BEGIN
	SET NOCOUNT ON
 
	IF @P_Accion = 1
	BEGIN
 
		SELECT IDLibro,
			   CONCAT_WS(' ', Nombre,Genero,ISBN, Editorial, Precio) AS Libro
			   --FORMAT(FecRegistro,'dd/MM/yyyy HH:mm') AS FecRegistro
			   --iif(IsActivo= 1, 'Activo', 'InActico') AS Estatus
		FROM LIBRO
 
	END
	--ELSE IF @P_Accion = 2
--	BEGIN
 
--		SELECT Matricula,
--			   CONCAT_WS(' ', Nombre,APaterno,AMaterno,Grupo) AS Alumno
--			   --FORMAT(FecRegistro,'dd/MM/yyyy HH:mm') AS FecRegistro
--			   --iif(IsActivo= 1, 'Activo', 'InActico') AS Estatus
--		FROM ALUMNO
--		WHERE CONCAT(Nombre,APaterno,AMaterno) LIKE '%' + @P_Texto + '%'
 
--	END
 
--	SET NOCOUNT OFF
END