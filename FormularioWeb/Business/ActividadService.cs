using System;
using System.Collections.Generic;
using System.Data;
using FormularioWeb.Data;
using FormularioWeb.Models;

namespace FormularioWeb.Business
{
    public class ActividadService
    {
        private readonly ActividadRepository repo = new ActividadRepository();
        public List<ActividadDto> Listar(string q) { return repo.Listar(q); }
        public ActividadDto Obtener(int id) { return repo.Obtener(id); }
        public DataTable Estudiantes() { return repo.Estudiantes(); }
        public DataTable Inscritos(int actividadId) { if(actividadId<1)throw new InvalidOperationException("Selecciona una actividad."); return repo.Inscritos(actividadId); }
        public void EliminarInscripcion(int id){if(id<1)throw new InvalidOperationException("Inscripción inválida.");repo.EliminarInscripcion(id);}
        public void Guardar(ActividadDto a) { if(a==null||String.IsNullOrWhiteSpace(a.Titulo)||a.Titulo.Length>120)throw new InvalidOperationException("El título es obligatorio (máximo 120 caracteres)."); if(a.Cupo<1)throw new InvalidOperationException("El cupo debe ser mayor que cero."); if(a.FechaFin.Date<a.FechaInicio.Date)throw new InvalidOperationException("La fecha final no puede ser anterior a la inicial.");var ahora=DateTime.Now;var momentoActual=new DateTime(ahora.Year,ahora.Month,ahora.Day,ahora.Hour,ahora.Minute,0);var inicioNuevo=a.FechaInicio.Date+TimeSpan.Parse(a.HoraInicio);if(inicioNuevo<momentoActual){var existente=a.Id>0?repo.Obtener(a.Id):null;var inicioAnterior=existente==null?DateTime.MinValue:existente.FechaInicio.Date+TimeSpan.Parse(existente.HoraInicio);if(a.Id==0||existente==null||inicioNuevo!=inicioAnterior)throw new InvalidOperationException("La actividad debe iniciar en una fecha y hora futuras.");} if(TimeSpan.Parse(a.HoraFin)<=TimeSpan.Parse(a.HoraInicio))throw new InvalidOperationException("La hora final debe ser posterior a la inicial."); repo.Guardar(a); }
        public void Eliminar(int id) { repo.Eliminar(id); }
        public void Inscribir(int actividad,int estudiante) { if(actividad<1||estudiante<1)throw new InvalidOperationException("Selecciona una actividad y un estudiante."); repo.Inscribir(actividad,estudiante); }
    }
}
