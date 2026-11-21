using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FormularioWeb.Business;
using FormularioWeb.Models;
using FormularioWeb.Views;
using FormularioWeb.Presenters;

namespace FormularioWeb
{
    public partial class Actividades : System.Web.UI.Page, IActividadesView
    {
        private readonly ActividadService service = new ActividadService();
        private readonly EstudianteService estudianteService = new EstudianteService();
        private ActividadesPresenter presenter;
        protected void Page_Init(object sender,EventArgs e){presenter=new ActividadesPresenter(this,service);}
        protected void Page_Load(object sender, EventArgs e) { if(!IsPostBack){ Cargar(); CargarEstudiantes(); CargarAdminEstudiantes(); }if(hfId.Value=="0")EstablecerFechaMinima(); }
        private void EstablecerFechaMinima(){txtFechaInicio.Attributes["min"]=DateTime.Today.ToString("yyyy-MM-dd");if(txtFechaInicio.Text==DateTime.Today.ToString("yyyy-MM-dd"))txtHoraInicio.Attributes["min"]=DateTime.Now.ToString("HH:mm");else txtHoraInicio.Attributes.Remove("min");}
        private void Cargar(){presenter.Cargar(txtBuscar.Text.Trim());}
        public void MostrarActividades(System.Collections.Generic.IList<ActividadDto> datos){gvActividades.DataSource=datos;gvActividades.DataBind();}
        public void MostrarResumen(int total,int inscritos,int proximas,int finalizadas){lblTotal.Text=total.ToString();lblInscritos.Text=inscritos.ToString();lblProximas.Text=proximas.ToString();lblFinalizadas.Text=finalizadas.ToString();}
        private void CargarEstudiantes(){ ddlEstudiante.DataSource=service.Estudiantes();ddlEstudiante.DataTextField="Nombre";ddlEstudiante.DataValueField="Id";ddlEstudiante.DataBind();ddlEstudiante.Items.Insert(0,new ListItem("Selecciona estudiante","")); }
        private void Mensaje(string s,string tipo){lblMensaje.Text=HttpUtility.HtmlEncode(s);lblMensaje.CssClass="msg "+tipo;lblMensaje.Attributes["role"]="alert";lblMensaje.Attributes["aria-live"]="polite";lblMensaje.Visible=true;}
        private void Mensaje(string s){Mensaje(s,"info");}
        private void Exito(string s){Mensaje(s,"success");}
        private void MostrarError(string s){Mensaje(s,"error");}
        protected void btnGuardar_Click(object sender,EventArgs e){try{var a=new ActividadDto{Id=int.Parse(hfId.Value),Titulo=txtTitulo.Text.Trim(),Descripcion=txtDescripcion.Text.Trim(),Tipo=ddlTipo.SelectedValue,FechaInicio=DateTime.Parse(txtFechaInicio.Text),FechaFin=DateTime.Parse(txtFechaFin.Text),HoraInicio=txtHoraInicio.Text,HoraFin=txtHoraFin.Text,Modalidad=ddlModalidad.SelectedValue,Lugar=txtLugar.Text.Trim(),Cupo=int.Parse(txtCupo.Text),Estado=ddlEstado.SelectedValue};bool nuevo=a.Id==0;service.Guardar(a);Exito(nuevo?"La actividad se creó correctamente.":"Los cambios de la actividad se guardaron correctamente.");Limpiar();Cargar();}catch(Exception ex){MostrarError(ex is FormatException?"Completa fechas, horas y cupo con valores válidos.":ex.Message);} }
        private void Limpiar(){hfId.Value="0";txtTitulo.Text=txtDescripcion.Text=txtFechaInicio.Text=txtFechaFin.Text=txtHoraInicio.Text=txtHoraFin.Text=txtCupo.Text=txtLugar.Text="";ddlTipo.SelectedIndex=ddlModalidad.SelectedIndex=ddlEstado.SelectedIndex=0;EstablecerFechaMinima();}
        protected void btnLimpiar_Click(object sender,EventArgs e){Limpiar();}
        protected void btnBuscar_Click(object sender,EventArgs e){gvActividades.PageIndex=0;Cargar();}
        protected void btnTodas_Click(object sender,EventArgs e){txtBuscar.Text="";gvActividades.PageIndex=0;Cargar();}
        protected void gvActividades_PageIndexChanging(object sender,GridViewPageEventArgs e){gvActividades.PageIndex=e.NewPageIndex;Cargar();}
        protected void gvActividades_RowDataBound(object sender,GridViewRowEventArgs e){if(e.Row.RowType!=DataControlRowType.DataRow)return;var estado=e.Row.FindControl("lblEstadoFila") as Label;if(estado==null)return;var valor=estado.Text.Trim().ToLowerInvariant();estado.CssClass="estado"+(valor=="activa"?" activa":valor=="finalizada"?" finalizada":valor=="cancelada"?" cancelada":"");}
        private void CargarAdminEstudiantes(){var datos=estudianteService.Listar(txtBuscarEstudiante.Text.Trim());gvEstudiantesAdmin.DataSource=datos;gvEstudiantesAdmin.DataBind();lblTotalEstudiantes.Text=datos.Rows.Count+" estudiante(s)";}
        protected void btnBuscarEstudiante_Click(object sender,EventArgs e){CargarAdminEstudiantes();}
        protected void btnTodosEstudiantes_Click(object sender,EventArgs e){txtBuscarEstudiante.Text="";CargarAdminEstudiantes();}
        protected void gvEstudiantesAdmin_RowDataBound(object sender,GridViewRowEventArgs e){if(e.Row.RowType!=DataControlRowType.DataRow)return;int cantidad=Convert.ToInt32(System.Web.UI.DataBinder.Eval(e.Row.DataItem,"InscripcionesActivas"));foreach(Control control in e.Row.Cells[e.Row.Cells.Count-1].Controls){var boton=control as LinkButton;if(boton==null||boton.CommandName!="eliminar")continue;string aviso=cantidad>0?"Este estudiante tiene "+cantidad+" inscripción(es) activa(s). Si continúas, se eliminarán el estudiante, sus inscripciones, reseñas y cuenta. ¿Deseas continuar?":"¿Eliminar este estudiante y su cuenta?";boton.OnClientClick="return confirm('"+aviso.Replace("'","\\'")+"');";}}
        protected void gvEstudiantesAdmin_RowCommand(object sender,GridViewCommandEventArgs e){int id=Convert.ToInt32(e.CommandArgument);try{if(e.CommandName=="editar"){var x=estudianteService.Obtener(id);if(x==null)throw new InvalidOperationException("No se encontró el estudiante.");hfEstudianteId.Value=x.Id.ToString();txtAdminCedula.Text=x.Cedula;txtAdminNombres.Text=x.Nombres;txtAdminApellidos.Text=x.Apellidos;txtAdminCorreo.Text=x.Correo;txtAdminTelefono.Text=x.Telefono;txtAdminPrograma.Text=x.Programa;ddlAdminSemestre.SelectedValue=x.Semestre.ToString();pnlEditarEstudiante.Visible=true;}else if(e.CommandName=="eliminar"){estudianteService.Eliminar(id);pnlEditarEstudiante.Visible=false;CargarAdminEstudiantes();AvisoEstudiantes("Estudiante y datos relacionados eliminados correctamente.",false);}}catch(Exception ex){AvisoEstudiantes(ex.Message,true);}}
        protected void btnGuardarEstudiante_Click(object sender,EventArgs e){try{estudianteService.Guardar(new EstudianteDto{Id=Convert.ToInt32(hfEstudianteId.Value),Cedula=txtAdminCedula.Text.Trim(),Nombres=txtAdminNombres.Text.Trim(),Apellidos=txtAdminApellidos.Text.Trim(),Correo=txtAdminCorreo.Text.Trim(),Telefono=txtAdminTelefono.Text.Trim(),Programa=txtAdminPrograma.Text.Trim(),Semestre=int.Parse(ddlAdminSemestre.SelectedValue)});pnlEditarEstudiante.Visible=false;CargarAdminEstudiantes();AvisoEstudiantes("Datos del estudiante actualizados correctamente.",false);}catch(Exception ex){AvisoEstudiantes(ex.Message,true);}}
        protected void btnCancelarEdicionEstudiante_Click(object sender,EventArgs e){pnlEditarEstudiante.Visible=false;}
        private void AvisoEstudiantes(string texto,bool error){lblMensajeEstudiantes.Text=HttpUtility.HtmlEncode(texto);lblMensajeEstudiantes.CssClass=error?"msg error":"msg success";lblMensajeEstudiantes.Visible=true;}
        protected void gvActividades_RowCommand(object sender,GridViewCommandEventArgs e){int id=int.Parse(e.CommandArgument.ToString());try{var a=service.Obtener(id);if(a==null){Mensaje("No se encontró la actividad.");return;}if(e.CommandName=="editar"){hfId.Value=a.Id.ToString();txtTitulo.Text=a.Titulo;txtDescripcion.Text=a.Descripcion;ddlTipo.SelectedValue=a.Tipo;txtFechaInicio.Text=a.FechaInicio.ToString("yyyy-MM-dd");txtFechaInicio.Attributes.Remove("min");txtFechaFin.Text=a.FechaFin.ToString("yyyy-MM-dd");txtHoraInicio.Text=a.HoraInicio;txtHoraFin.Text=a.HoraFin;ddlModalidad.SelectedValue=a.Modalidad;txtCupo.Text=a.Cupo.ToString();txtLugar.Text=a.Lugar;ddlEstado.SelectedValue=a.Estado;}
        else if(e.CommandName=="ver"){ViewState["ActividadId"]=id;lblDetalle.Text="<table class='detail-table'>"+Detalle("Título",a.Titulo)+Detalle("Tipo",a.Tipo)+Detalle("Descripción",a.Descripcion)+Detalle("Fecha",a.FechaInicio.ToString("dd/MM/yyyy")+" – "+a.FechaFin.ToString("dd/MM/yyyy"))+Detalle("Hora",a.HoraInicio+" – "+a.HoraFin)+Detalle("Modalidad",a.Modalidad)+Detalle("Lugar",a.Lugar)+Detalle("Cupos",a.Cupo-a.Inscritos+" disponibles de "+a.Cupo)+Detalle("Estado",a.Estado)+"</table>";CargarInscritos(id);}
        else if(e.CommandName=="eliminar"){service.Eliminar(id);Exito("La actividad se eliminó correctamente.");Cargar();}}catch(Exception ex){MostrarError(ex.Message);} }
        private string Detalle(string etiqueta,string valor){return "<tr><th>"+HttpUtility.HtmlEncode(etiqueta)+":</th><td>"+HttpUtility.HtmlEncode(valor??"")+"</td></tr>";}
        private void CargarInscritos(int id){var inscritos=service.Inscritos(id);gvInscritos.DataSource=inscritos;gvInscritos.DataBind();lblTotalInscritos.Text=inscritos.Rows.Count.ToString();}
        protected void gvInscritos_RowCommand(object sender,GridViewCommandEventArgs e){if(e.CommandName!="retirar")return;try{int idActividad=Convert.ToInt32(ViewState["ActividadId"]);service.EliminarInscripcion(Convert.ToInt32(e.CommandArgument));CargarInscritos(idActividad);Cargar();Exito("La inscripción se retiró correctamente.");}catch(Exception ex){MostrarError(ex.Message);}}
        protected void btnInscribir_Click(object sender,EventArgs e){try{int id=Convert.ToInt32(ViewState["ActividadId"]);service.Inscribir(id,int.Parse(ddlEstudiante.SelectedValue));Exito("El estudiante quedó inscrito correctamente.");Cargar();CargarInscritos(id);}catch(Exception ex){MostrarError(ex.Message);} }
        private static string CampoCsv(string valor){return "\""+(valor??"").Replace("\"","\"\"")+"\"";}
        protected void btnReportes_Click(object sender,EventArgs e){
            var actividades=service.Listar(txtBuscar.Text.Trim());
            var csv=new StringBuilder();
            csv.AppendLine("Actividad;Tipo;Fecha de inicio;Fecha de fin;Cupo;Inscritos;Cupos disponibles;Estado");
            foreach(var a in actividades){
                csv.Append(CampoCsv(a.Titulo)).Append(';').Append(CampoCsv(a.Tipo)).Append(';')
                   .Append(a.FechaInicio.ToString("dd/MM/yyyy")).Append(';')
                   .Append(a.FechaFin.ToString("dd/MM/yyyy")).Append(';')
                   .Append(a.Cupo).Append(';').Append(a.Inscritos).Append(';')
                   .Append(Math.Max(0,a.Cupo-a.Inscritos)).Append(';')
                   .Append(CampoCsv(a.Estado)).AppendLine();
            }
            Response.Clear();
            Response.Buffer=true;
            Response.ContentEncoding=Encoding.UTF8;
            Response.ContentType="text/csv; charset=utf-8";
            Response.AddHeader("Content-Disposition","attachment; filename=ReporteActividades.csv");
            Response.Write("\uFEFF"+csv.ToString());
            Response.End();
        }
    }
}



