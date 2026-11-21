/* Credenciales del portal. No altera ni elimina los registros de Estudiantes. */
IF OBJECT_ID(N'dbo.CuentaEstudiante', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.CuentaEstudiante
    (
        EstudianteId INT NOT NULL CONSTRAINT PK_CuentaEstudiante PRIMARY KEY,
        Salt VARBINARY(16) NOT NULL,
        ClaveHash VARBINARY(32) NOT NULL,
        FechaCreacion DATETIME2(0) NOT NULL CONSTRAINT DF_CuentaEstudiante_Fecha DEFAULT(SYSDATETIME()),
        CONSTRAINT FK_CuentaEstudiante_Estudiante FOREIGN KEY (EstudianteId) REFERENCES dbo.Estudiantes(Id)
    );
END;
GO
