using System.Collections.Generic;
using FormularioWeb.Models;

namespace FormularioWeb.Views
{
    public interface IActividadesView
    {
        void MostrarActividades(IList<ActividadDto> actividades);
        void MostrarResumen(int total, int inscritos, int proximas, int finalizadas);
    }
}
