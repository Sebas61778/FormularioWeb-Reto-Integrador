# FormularioWeb / UniCampus

Aplicación ASP.NET Web Forms (.NET Framework 4.7.2) con SQL Server LocalDB.
La solución conserva el registro CRUD de estudiantes en `Default.aspx` y agrega
un panel de gestión académica en `Actividades.aspx`.

## Base de datos

La cadena existente `ConexionFormulario` apunta a `(localdb)\MSSQLLocalDB`,
base `FormularioDB`. El script `BaseDatos/02_Actividades.sql` es idempotente:
crea las tablas de actividades, inscripciones y reseñas solo si faltan y deja
los datos existentes de `dbo.Estudiantes` intactos. Para ejecutarlo:

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d FormularioDB -f 65001 -i "BaseDatos\01_Estudiantes.sql"
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d FormularioDB -f 65001 -i "BaseDatos\02_Actividades.sql"
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d FormularioDB -f 65001 -i "BaseDatos\03_CorregirMuestraUTF8.sql"
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d FormularioDB -f 65001 -i "BaseDatos\04_VistaInscripciones.sql"
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d FormularioDB -f 65001 -i "BaseDatos\05_CuentasPortal.sql"
```

Para consultar las inscripciones con nombres descriptivos desde SQL Server:

```sql
SELECT * FROM dbo.vw_InscripcionesActividad;
```

## Funciones

- CRUD de actividades con filtros y validaciones de servidor.
- Inscripción de estudiantes existentes, con transacción, cupos y restricción de duplicados.
- Reseñas por estudiante y actividad (calificación 1–5, una por estudiante).
- Exportación CSV de actividades.
- Portal de estudiantes con registro/inicio de sesión, actividades disponibles y consulta de sus propias inscripciones.
- Registro, consulta, actualización y eliminación de estudiantes en la página original.

## Estructura

La solución mantiene el proyecto Web Forms original en .NET Framework 4.7.2 y ahora separa
las responsabilidades en proyectos: `Entidades`, `Datos`, `Negocio` y `FormularioWeb`.
Las referencias apuntan desde Web hacia Negocio/Entidades; Negocio hacia Datos/Entidades;
Datos hacia Entidades. `Actividades.aspx` implementa `IActividadesView` y delega la carga inicial
al `ActividadesPresenter`.

Las contraseñas del portal se guardan como hash PBKDF2 con sal aleatoria; nunca en texto plano.
Las cuentas se vinculan a `Estudiantes` por cédula. Para activar una ficha existente se debe
ingresar la misma cédula y el correo ya registrado en esa ficha.
