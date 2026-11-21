using System;

namespace FormularioWeb.Models
{
    public class ActividadDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Modalidad { get; set; }
        public string Lugar { get; set; }
        public int Cupo { get; set; }
        public int Inscritos { get; set; }
        public string Estado { get; set; }
    }
}
