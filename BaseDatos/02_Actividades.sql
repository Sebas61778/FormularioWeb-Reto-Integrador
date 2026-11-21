/*
  Ampliación no destructiva para FormularioDB.
  Conserva dbo.Estudiantes y crea únicamente las entidades que faltan.
  Ejecutar con sqlcmd contra FormularioDB o desde SSMS.
*/
IF OBJECT_ID(N'dbo.ActividadAcademica', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ActividadAcademica
    (
        ActividadId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ActividadAcademica PRIMARY KEY,
        Titulo NVARCHAR(120) NOT NULL,
        Descripcion NVARCHAR(1000) NOT NULL,
        Tipo NVARCHAR(40) NOT NULL,
        FechaInicio DATE NOT NULL,
        FechaFin DATE NOT NULL,
        HoraInicio TIME(0) NOT NULL,
        HoraFin TIME(0) NOT NULL,
        Modalidad NVARCHAR(30) NOT NULL,
        Lugar NVARCHAR(150) NOT NULL,
        Cupo INT NOT NULL,
        Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_ActividadAcademica_Estado DEFAULT(N'Próxima'),
        CONSTRAINT CK_ActividadAcademica_Cupo CHECK (Cupo > 0),
        CONSTRAINT CK_ActividadAcademica_Fechas CHECK (FechaFin >= FechaInicio),
        CONSTRAINT CK_ActividadAcademica_Horas CHECK (HoraFin > HoraInicio)
    );
END;
GO

IF OBJECT_ID(N'dbo.InscripcionActividad', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InscripcionActividad
    (
        InscripcionId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_InscripcionActividad PRIMARY KEY,
        ActividadId INT NOT NULL,
        EstudianteId INT NOT NULL,
        FechaInscripcion DATETIME2(0) NOT NULL CONSTRAINT DF_InscripcionActividad_Fecha DEFAULT(SYSDATETIME()),
        CONSTRAINT FK_InscripcionActividad_Actividad FOREIGN KEY (ActividadId) REFERENCES dbo.ActividadAcademica(ActividadId),
        CONSTRAINT FK_InscripcionActividad_Estudiante FOREIGN KEY (EstudianteId) REFERENCES dbo.Estudiantes(Id),
        CONSTRAINT UQ_InscripcionActividad UNIQUE (ActividadId, EstudianteId)
    );
END;
GO

IF OBJECT_ID(N'dbo.ResenaActividad', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ResenaActividad
    (
        ResenaId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ResenaActividad PRIMARY KEY,
        ActividadId INT NOT NULL,
        EstudianteId INT NOT NULL,
        Calificacion TINYINT NOT NULL,
        Comentario NVARCHAR(1000) NULL,
        FechaCreacion DATETIME2(0) NOT NULL CONSTRAINT DF_ResenaActividad_Fecha DEFAULT(SYSDATETIME()),
        CONSTRAINT FK_ResenaActividad_Actividad FOREIGN KEY (ActividadId) REFERENCES dbo.ActividadAcademica(ActividadId),
        CONSTRAINT FK_ResenaActividad_Estudiante FOREIGN KEY (EstudianteId) REFERENCES dbo.Estudiantes(Id),
        CONSTRAINT UQ_ResenaActividad UNIQUE (ActividadId, EstudianteId),
        CONSTRAINT CK_ResenaActividad_Calificacion CHECK (Calificacion BETWEEN 1 AND 5)
    );
END;
GO

/* Datos de demostración solo si la tabla de actividades está vacía. */
IF NOT EXISTS (SELECT 1 FROM dbo.ActividadAcademica)
BEGIN
    INSERT dbo.ActividadAcademica
        (Titulo, Descripcion, Tipo, FechaInicio, FechaFin, HoraInicio, HoraFin, Modalidad, Lugar, Cupo, Estado)
    VALUES
        (N'Taller de Programación Web', N'Espacio práctico para reforzar conceptos de programación web con ASP.NET.', N'Taller', '2026-11-20', '2026-11-20', '08:00', '12:00', N'Presencial', N'Laboratorio 3', 30, N'Próxima'),
        (N'Conferencia de Inteligencia Artificial', N'Conversatorio sobre aplicaciones responsables de inteligencia artificial.', N'Conferencia', '2026-11-24', '2026-11-24', '14:00', '16:00', N'Híbrida', N'Auditorio principal', 100, N'Próxima'),
        (N'Seminario de Bases de Datos', N'Buenas prácticas de diseño, consultas y administración de datos.', N'Seminario', '2026-12-02', '2026-12-02', '09:00', '12:00', N'Presencial', N'Bloque B - Sala 204', 50, N'Próxima'),
        (N'Curso de Diseño UX/UI', N'Introducción al diseño centrado en las personas.', N'Curso', '2026-12-10', '2026-12-11', '08:00', '12:00', N'Virtual', N'En línea', 40, N'Próxima'),
        (N'Encuentro de Investigación', N'Presentación de semilleros y proyectos estudiantiles.', N'Encuentro', '2026-10-20', '2026-10-20', '10:00', '13:00', N'Presencial', N'Auditorio norte', 80, N'Finalizada');
END;
GO
