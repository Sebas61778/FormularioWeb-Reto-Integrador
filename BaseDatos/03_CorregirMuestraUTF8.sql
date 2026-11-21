/* Corrige las cinco filas de demostración insertadas por 02_Actividades.sql. */
UPDATE dbo.ActividadAcademica SET
    Descripcion=N'Espacio práctico para reforzar conceptos de programación web con ASP.NET.',
    Estado=N'Próxima'
WHERE ActividadId=1 AND Titulo=N'Taller de Programación Web';

UPDATE dbo.ActividadAcademica SET
    Descripcion=N'Conversatorio sobre aplicaciones responsables de inteligencia artificial.',
    Estado=N'Próxima'
WHERE ActividadId=2 AND Titulo=N'Conferencia de Inteligencia Artificial';

UPDATE dbo.ActividadAcademica SET
    Descripcion=N'Buenas prácticas de diseño, consultas y administración de datos.',
    Estado=N'Próxima'
WHERE ActividadId=3 AND Titulo=N'Seminario de Bases de Datos';

UPDATE dbo.ActividadAcademica SET
    Descripcion=N'Introducción al diseño centrado en las personas.',
    Lugar=N'En línea',
    Estado=N'Próxima'
WHERE ActividadId=4 AND Titulo=N'Curso de Diseño UX/UI';

UPDATE dbo.ActividadAcademica SET
    Descripcion=N'Presentación de semilleros y proyectos estudiantiles.',
    Estado=N'Finalizada'
WHERE ActividadId=5 AND Titulo=N'Encuentro de Investigación';
GO
