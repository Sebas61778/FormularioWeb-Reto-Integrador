using System;
using System.Data;
using FormularioWeb.Data;
using FormularioWeb.Models;

namespace FormularioWeb.Business
{
    public class EstudianteService
    {
        private readonly EstudianteRepository repo=new EstudianteRepository();
        public DataTable Listar(string filtro){return repo.Listar(filtro);}
        public EstudianteDto Obtener(int id){return repo.Obtener(id);}
        public void Guardar(EstudianteDto e){if(e==null||String.IsNullOrWhiteSpace(e.Cedula)||String.IsNullOrWhiteSpace(e.Nombres)||String.IsNullOrWhiteSpace(e.Apellidos)||String.IsNullOrWhiteSpace(e.Correo)||String.IsNullOrWhiteSpace(e.Programa))throw new InvalidOperationException("Completa cédula, nombres, apellidos, correo y programa.");if(e.Cedula.Length>20||e.Nombres.Length>100||e.Apellidos.Length>100||e.Correo.Length>150||e.Telefono.Length>20||e.Programa.Length>100)throw new InvalidOperationException("Uno de los datos supera la longitud permitida.");if(e.Semestre<1||e.Semestre>10)throw new InvalidOperationException("El semestre debe estar entre 1 y 10.");repo.Guardar(e);}
        public void Eliminar(int id){repo.Eliminar(id);}
    }
}
