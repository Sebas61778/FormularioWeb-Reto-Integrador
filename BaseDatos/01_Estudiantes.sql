/* Esquema mínimo de partida para una base limpia. No reemplaza tablas existentes. */
IF OBJECT_ID(N'dbo.Estudiantes', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Estudiantes
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Estudiantes PRIMARY KEY,
        Cedula VARCHAR(20) NOT NULL,
        CodigoRegistro VARCHAR(30) NOT NULL,
        Nombres NVARCHAR(100) NOT NULL,
        Apellidos NVARCHAR(100) NOT NULL,
        Correo NVARCHAR(150) NOT NULL,
        Telefono VARCHAR(20) NOT NULL,
        Programa NVARCHAR(100) NOT NULL,
        Semestre INT NOT NULL,
        Participacion NVARCHAR(30) NOT NULL,
        Jornada NVARCHAR(30) NOT NULL,
        TemasInteres NVARCHAR(500) NULL,
        Comentarios NVARCHAR(MAX) NULL,
        AceptaTratamiento BIT NOT NULL,
        FechaRegistro DATETIME2 NOT NULL CONSTRAINT DF_Estudiantes_FechaRegistro DEFAULT(SYSDATETIME()),
        CONSTRAINT UQ_Estudiantes_Cedula UNIQUE(Cedula),
        CONSTRAINT UQ_Estudiantes_CodigoRegistro UNIQUE(CodigoRegistro)
    );
END;
GO
