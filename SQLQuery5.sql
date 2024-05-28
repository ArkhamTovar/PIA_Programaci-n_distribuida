CREATE PROC sp_ActualizarLibro
(@P_IDLibro INT,
@P_Nombre VARCHAR(60),
@P_Genero VARCHAR(60),  
@P_ISBN INT, 
@P_Editorial VARCHAR (60),
@P_Precio INT)
AS
BEGIN
	SET NOCOUNT ON
 
	UPDATE CLIENTE SET Nombre = @P_Nombre,
					   Genero = @P_Genero,
					   ISBN = @P_ISBN,
					   Editorial = @P_Editorial,
					   Precio=@P_Precio
					   --IsActivo = @P_IsActivo
	WHERE IDLibro = @P_IDLibro
 
 
	SET NOCOUNT OFF
END