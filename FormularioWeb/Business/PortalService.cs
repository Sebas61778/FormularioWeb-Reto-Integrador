using System;
using System.Data;
using FormularioWeb.Data;

namespace FormularioWeb.Business
{
    public class PortalService
    {
        private readonly PortalRepository repo=new PortalRepository();
        public int Registrar(string cedula,string nombre,string apellido,string correo,string telefono,string programa,int semestre,string clave){if(String.IsNullOrWhiteSpace(cedula)||String.IsNullOrWhiteSpace(nombre)||String.IsNullOrWhiteSpace(apellido)||String.IsNullOrWhiteSpace(correo)||String.IsNullOrWhiteSpace(programa))throw new InvalidOperationException("Completa todos los datos obligatorios.");if(cedula.Length>20||nombre.Length>100||apellido.Length>100||correo.Length>150||telefono.Length>20||programa.Length>100)throw new InvalidOperationException("Uno de los datos supera la longitud permitida.");if(clave==null||clave.Length<8)throw new InvalidOperationException("La contraseña debe tener al menos 8 caracteres.");return repo.Registrar(cedula.Trim(),nombre.Trim(),apellido.Trim(),correo.Trim(),telefono.Trim(),programa,semestre,clave);}
        public int? IniciarSesion(string cedula,string clave,out string nombre){return repo.IniciarSesion(cedula,clave,out nombre);}
        public DataTable Disponibles(int id){return repo.Disponibles(id);}
        public DataTable Inscripciones(int id){return repo.MisInscripciones(id);}
        public void Inscribir(int actividad,int estudiante){repo.Inscribir(actividad,estudiante);}
        public void GuardarResena(int actividad,int estudiante,int calificacion,string comentario){if(actividad<1||estudiante<1)throw new InvalidOperationException("Selecciona una actividad válida.");if(calificacion<1||calificacion>5)throw new InvalidOperationException("La calificación debe estar entre 1 y 5.");if(comentario!=null&&comentario.Trim().Length>1000)throw new InvalidOperationException("El comentario no puede superar 1000 caracteres.");repo.GuardarResena(actividad,estudiante,calificacion,comentario);}
    }
}
