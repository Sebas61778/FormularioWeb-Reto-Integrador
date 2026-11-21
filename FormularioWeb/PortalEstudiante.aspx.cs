using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FormularioWeb.Business;

namespace FormularioWeb
{
    public partial class PortalEstudiante : System.Web.UI.Page
    {
        private readonly PortalService service=new PortalService();
        protected void Page_Load(object sender,EventArgs e){if(!IsPostBack)ActualizarVista();}
        private void Aviso(string texto,bool error){lblMensaje.Text=HttpUtility.HtmlEncode(texto);lblMensaje.CssClass=error?"portalmsg portalerror":"portalmsg";lblMensaje.Visible=true;}
        private void ActualizarVista(){bool autenticado=Session["EstudianteId"]!=null;pnlAcceso.Visible=!autenticado;pnlEstudiante.Visible=autenticado;if(autenticado){lblNombre.Text=HttpUtility.HtmlEncode(Convert.ToString(Session["EstudianteNombre"]));CargarListas();}}
        private void CargarListas(){int id=Convert.ToInt32(Session["EstudianteId"]);gvDisponibles.DataSource=service.Disponibles(id);gvDisponibles.DataBind();gvMisInscripciones.DataSource=service.Inscripciones(id);gvMisInscripciones.DataBind();}
        protected void btnLogin_Click(object sender,EventArgs e){try{string nombre;var id=service.IniciarSesion(txtLoginCedula.Text.Trim(),txtLoginClave.Text,out nombre);if(!id.HasValue)throw new InvalidOperationException("Cédula o contraseña incorrecta.");Session["EstudianteId"]=id.Value;Session["EstudianteNombre"]=nombre;ActualizarVista();Aviso("Sesión iniciada correctamente.",false);}catch(Exception ex){Aviso(ex.Message,true);}}
        protected void btnSalir_Click(object sender,EventArgs e){Session.Remove("EstudianteId");Session.Remove("EstudianteNombre");ActualizarVista();Aviso("Cerraste sesión.",false);}
        protected void gvDisponibles_RowCommand(object sender,GridViewCommandEventArgs e){if(e.CommandName!="inscribir")return;try{service.Inscribir(Convert.ToInt32(e.CommandArgument),Convert.ToInt32(Session["EstudianteId"]));CargarListas();Aviso("Te inscribiste correctamente. La actividad ya aparece en Mis inscripciones.",false);}catch(Exception ex){Aviso(ex.Message,true);}}
        protected void gvMisInscripciones_RowDataBound(object sender,GridViewRowEventArgs e){if(e.Row.RowType!=DataControlRowType.DataRow)return;var fecha=(DateTime)DataBinder.Eval(e.Row.DataItem,"FechaFin");var hora=(TimeSpan)DataBinder.Eval(e.Row.DataItem,"HoraFin");bool yaReseno=Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem,"YaReseno"));bool finalizada=fecha.Date+hora<=DateTime.Now;var boton=e.Row.FindControl("btnCalificar") as LinkButton;var estado=e.Row.FindControl("lblEstadoResena") as Label;if(boton!=null)boton.Visible=finalizada&&!yaReseno;if(estado!=null){estado.Text=yaReseno?"Ya calificada":finalizada?"":"Disponible al finalizar";estado.CssClass="review-state";}}
        protected void gvMisInscripciones_RowCommand(object sender,GridViewCommandEventArgs e){if(e.CommandName!="calificar")return;ViewState["ActividadParaResena"]=Convert.ToInt32(e.CommandArgument);pnlCalificar.Visible=true;}
        protected void btnEnviarResena_Click(object sender,EventArgs e){try{int actividad=Convert.ToInt32(ViewState["ActividadParaResena"]);int estudiante=Convert.ToInt32(Session["EstudianteId"]);service.GuardarResena(actividad,estudiante,int.Parse(ddlCalificacion.SelectedValue),txtComentarioResena.Text);pnlCalificar.Visible=false;txtComentarioResena.Text="";CargarListas();Aviso("Gracias por calificar la actividad.",false);}catch(Exception ex){Aviso(ex.Message.Contains("UQ_ResenaActividad")?"Ya enviaste una calificación para esta actividad.":ex.Message,true);}}
        protected void btnCancelarResena_Click(object sender,EventArgs e){pnlCalificar.Visible=false;txtComentarioResena.Text="";}
    }
}
