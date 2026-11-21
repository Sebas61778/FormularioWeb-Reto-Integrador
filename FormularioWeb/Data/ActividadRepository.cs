using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FormularioWeb.Models;

namespace FormularioWeb.Data
{
    public class ActividadRepository
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["ConexionFormulario"].ConnectionString;

        public List<ActividadDto> Listar(string filtro)
        {
            var lista = new List<ActividadDto>();
            const string sql = @"SELECT a.ActividadId,a.Titulo,a.Descripcion,a.Tipo,a.FechaInicio,a.FechaFin,
 a.HoraInicio,a.HoraFin,a.Modalidad,a.Lugar,a.Cupo,a.Estado,COUNT(i.InscripcionId) Inscritos
 FROM dbo.ActividadAcademica a LEFT JOIN dbo.InscripcionActividad i ON i.ActividadId=a.ActividadId
 WHERE (@Filtro='' OR a.Titulo LIKE @Like OR a.Tipo LIKE @Like OR a.Estado LIKE @Like)
 GROUP BY a.ActividadId,a.Titulo,a.Descripcion,a.Tipo,a.FechaInicio,a.FechaFin,a.HoraInicio,a.HoraFin,a.Modalidad,a.Lugar,a.Cupo,a.Estado
 ORDER BY a.FechaInicio DESC";
            using (var cn = new SqlConnection(cs)) using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Filtro", SqlDbType.NVarChar, 120).Value = filtro ?? "";
                cmd.Parameters.Add("@Like", SqlDbType.NVarChar, 130).Value = "%" + (filtro ?? "") + "%";
                cn.Open(); using (var r = cmd.ExecuteReader()) while (r.Read()) lista.Add(Map(r));
            }
            return lista;
        }

        private static ActividadDto Map(SqlDataReader r) { return new ActividadDto { Id=r.GetInt32(0), Titulo=r.GetString(1), Descripcion=r.GetString(2), Tipo=r.GetString(3), FechaInicio=r.GetDateTime(4), FechaFin=r.GetDateTime(5), HoraInicio=r.GetTimeSpan(6).ToString(@"hh\:mm"), HoraFin=r.GetTimeSpan(7).ToString(@"hh\:mm"), Modalidad=r.GetString(8), Lugar=r.GetString(9), Cupo=r.GetInt32(10), Estado=r.GetString(11), Inscritos=r.GetInt32(12) }; }
        public ActividadDto Obtener(int id) { using(var cn=new SqlConnection(cs)) using(var cmd=new SqlCommand("SELECT a.ActividadId,a.Titulo,a.Descripcion,a.Tipo,a.FechaInicio,a.FechaFin,a.HoraInicio,a.HoraFin,a.Modalidad,a.Lugar,a.Cupo,a.Estado,COUNT(i.InscripcionId) FROM dbo.ActividadAcademica a LEFT JOIN dbo.InscripcionActividad i ON i.ActividadId=a.ActividadId WHERE a.ActividadId=@Id GROUP BY a.ActividadId,a.Titulo,a.Descripcion,a.Tipo,a.FechaInicio,a.FechaFin,a.HoraInicio,a.HoraFin,a.Modalidad,a.Lugar,a.Cupo,a.Estado",cn)){cmd.Parameters.Add("@Id",SqlDbType.Int).Value=id;cn.Open();using(var r=cmd.ExecuteReader())return r.Read()?Map(r):null;} }
        public void Guardar(ActividadDto a) { using(var cn=new SqlConnection(cs)) using(var cmd=new SqlCommand(a.Id==0 ? @"INSERT dbo.ActividadAcademica(Titulo,Descripcion,Tipo,FechaInicio,FechaFin,HoraInicio,HoraFin,Modalidad,Lugar,Cupo,Estado) VALUES(@t,@d,@tipo,@fi,@ff,@hi,@hf,@m,@l,@c,@e)" : @"UPDATE dbo.ActividadAcademica SET Titulo=@t,Descripcion=@d,Tipo=@tipo,FechaInicio=@fi,FechaFin=@ff,HoraInicio=@hi,HoraFin=@hf,Modalidad=@m,Lugar=@l,Cupo=@c,Estado=@e WHERE ActividadId=@id AND @c >= (SELECT COUNT(*) FROM dbo.InscripcionActividad WHERE ActividadId=@id)",cn)){cmd.Parameters.Add("@t",SqlDbType.NVarChar,120).Value=a.Titulo;cmd.Parameters.Add("@d",SqlDbType.NVarChar,1000).Value=a.Descripcion;cmd.Parameters.Add("@tipo",SqlDbType.NVarChar,40).Value=a.Tipo;cmd.Parameters.Add("@fi",SqlDbType.Date).Value=a.FechaInicio;cmd.Parameters.Add("@ff",SqlDbType.Date).Value=a.FechaFin;cmd.Parameters.Add("@hi",SqlDbType.Time).Value=TimeSpan.Parse(a.HoraInicio);cmd.Parameters.Add("@hf",SqlDbType.Time).Value=TimeSpan.Parse(a.HoraFin);cmd.Parameters.Add("@m",SqlDbType.NVarChar,30).Value=a.Modalidad;cmd.Parameters.Add("@l",SqlDbType.NVarChar,150).Value=a.Lugar;cmd.Parameters.Add("@c",SqlDbType.Int).Value=a.Cupo;cmd.Parameters.Add("@e",SqlDbType.NVarChar,20).Value=a.Estado;if(a.Id>0)cmd.Parameters.Add("@id",SqlDbType.Int).Value=a.Id;cn.Open();if(cmd.ExecuteNonQuery()==0&&a.Id>0)throw new InvalidOperationException("El cupo no puede ser menor que el número de estudiantes inscritos, o la actividad ya no existe.");} }
        public void Eliminar(int id) { using(var cn=new SqlConnection(cs)) using(var cmd=new SqlCommand("DELETE FROM dbo.ActividadAcademica WHERE ActividadId=@id AND NOT EXISTS(SELECT 1 FROM dbo.InscripcionActividad WHERE ActividadId=@id)",cn)){cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;cn.Open();if(cmd.ExecuteNonQuery()==0)throw new InvalidOperationException("La actividad tiene inscripciones o ya no existe. Puedes cambiar su estado a Finalizada.");} }
        public DataTable Estudiantes() { var t=new DataTable(); using(var cn=new SqlConnection(cs)) using(var da=new SqlDataAdapter("SELECT Id, Nombres+' '+Apellidos Nombre FROM dbo.Estudiantes ORDER BY Nombres,Apellidos",cn))da.Fill(t); return t; }
        public DataTable Inscritos(int actividadId) { var t=new DataTable(); const string sql=@"SELECT i.InscripcionId,e.Nombres+' '+e.Apellidos AS Nombre,e.Programa,i.FechaInscripcion AS FechaInscripcion FROM dbo.InscripcionActividad i INNER JOIN dbo.Estudiantes e ON e.Id=i.EstudianteId WHERE i.ActividadId=@Id ORDER BY e.Apellidos,e.Nombres"; using(var cn=new SqlConnection(cs))using(var cmd=new SqlCommand(sql,cn))using(var da=new SqlDataAdapter(cmd)){cmd.Parameters.Add("@Id",SqlDbType.Int).Value=actividadId;da.Fill(t);}return t; }
        public void EliminarInscripcion(int inscripcionId){using(var cn=new SqlConnection(cs))using(var cmd=new SqlCommand("DELETE dbo.InscripcionActividad WHERE InscripcionId=@id",cn)){cmd.Parameters.Add("@id",SqlDbType.Int).Value=inscripcionId;cn.Open();if(cmd.ExecuteNonQuery()==0)throw new InvalidOperationException("La inscripción ya no existe.");}}
        public void Inscribir(int actividad,int estudiante) { using(var cn=new SqlConnection(cs)){cn.Open();using(var tx=cn.BeginTransaction(IsolationLevel.Serializable))try{using(var check=new SqlCommand("SELECT Cupo,Estado,FechaInicio,HoraInicio,(SELECT COUNT(*) FROM dbo.InscripcionActividad WITH(UPDLOCK,HOLDLOCK) WHERE ActividadId=@a) FROM dbo.ActividadAcademica WITH(UPDLOCK,HOLDLOCK) WHERE ActividadId=@a",cn,tx)){check.Parameters.Add("@a",SqlDbType.Int).Value=actividad;using(var r=check.ExecuteReader()){if(!r.Read())throw new InvalidOperationException("No se encontró la actividad.");var estado=r.GetString(1);var inicio=r.GetDateTime(2).Date+r.GetTimeSpan(3);if(estado=="Finalizada"||estado=="Cancelada"||inicio<=DateTime.Now)throw new InvalidOperationException("La actividad ya inició, finalizó o fue cancelada; no admite inscripciones.");if(r.GetInt32(0)<=r.GetInt32(4))throw new InvalidOperationException("La actividad ya no tiene cupos disponibles.");}}using(var cmd=new SqlCommand("INSERT dbo.InscripcionActividad(ActividadId,EstudianteId) VALUES(@a,@e)",cn,tx)){cmd.Parameters.Add("@a",SqlDbType.Int).Value=actividad;cmd.Parameters.Add("@e",SqlDbType.Int).Value=estudiante;cmd.ExecuteNonQuery();}tx.Commit();}catch{tx.Rollback();throw;}} }
    }
}
