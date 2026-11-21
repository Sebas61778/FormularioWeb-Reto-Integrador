namespace FormularioWeb.Models
{
    public class EstudianteDto
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Programa { get; set; }
        public int Semestre { get; set; }
    }
}
