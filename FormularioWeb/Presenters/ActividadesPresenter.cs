using System;
using System.Linq;
using FormularioWeb.Business;
using FormularioWeb.Views;

namespace FormularioWeb.Presenters
{
    public class ActividadesPresenter
    {
        private readonly IActividadesView vista;
        private readonly ActividadService servicio;
        public ActividadesPresenter(IActividadesView vista, ActividadService servicio){this.vista=vista??throw new ArgumentNullException("vista");this.servicio=servicio??throw new ArgumentNullException("servicio");}
        public void Cargar(string filtro){var lista=servicio.Listar(filtro??"");vista.MostrarActividades(lista);var todas=String.IsNullOrWhiteSpace(filtro)?lista:servicio.Listar("");vista.MostrarResumen(todas.Count,todas.Sum(x=>x.Inscritos),todas.Count(x=>x.Estado=="Próxima"),todas.Count(x=>x.Estado=="Finalizada"));}
    }
}
