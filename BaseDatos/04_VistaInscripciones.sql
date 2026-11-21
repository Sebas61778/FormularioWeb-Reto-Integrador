/* Vista de lectura: conserva las FK en la tabla y presenta nombres descriptivos. */
CREATE OR ALTER VIEW dbo.vw_InscripcionesActividad
AS
    SELECT
        i.InscripcionId,
        i.ActividadId,
        a.Titulo AS NombreActividad,
        i.EstudianteId,
        e.Nombres AS NombresEstudiante,
        e.Apellidos AS ApellidosEstudiante,
        CONCAT(e.Nombres, N' ', e.Apellidos) AS NombreCompletoEstudiante,
        i.FechaInscripcion
    FROM dbo.InscripcionActividad AS i
    INNER JOIN dbo.ActividadAcademica AS a ON a.ActividadId = i.ActividadId
    INNER JOIN dbo.Estudiantes AS e ON e.Id = i.EstudianteId;
GO
