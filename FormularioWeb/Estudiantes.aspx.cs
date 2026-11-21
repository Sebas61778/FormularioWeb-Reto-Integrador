using System;
using System.Web;
using FormularioWeb.Business;

namespace FormularioWeb
{
    public partial class AdministrarEstudiantes : System.Web.UI.Page
    {
        private readonly PortalService portal = new PortalService();
        protected void Page_Load(object sender, EventArgs e) { }
        private void Aviso(string texto, bool error) { lblMensaje.Text=HttpUtility.HtmlEncode(texto); lblMensaje.CssClass=error?"warning err":"warning success"; lblMensaje.Visible=true; }
        private void Limpiar() { txtCedula.Text=txtNombres.Text=txtApellidos.Text=txtCorreo.Text=txtTelefono.Text=txtPrograma.Text=txtClaveCuenta.Text=""; ddlSemestre.SelectedIndex=0; chkDatos.Checked=false; }
        protected void btnGuardar_Click(object sender, EventArgs e) { try { int semestre; if(!int.TryParse(ddlSemestre.SelectedValue,out semestre)) throw new InvalidOperationException("Selecciona el semestre."); if(!chkDatos.Checked) throw new InvalidOperationException("Debes aceptar el tratamiento de datos personales."); portal.Registrar(txtCedula.Text,txtNombres.Text,txtApellidos.Text,txtCorreo.Text,txtTelefono.Text,txtPrograma.Text,semestre,txtClaveCuenta.Text); Aviso("Cuenta creada correctamente. Ya puedes iniciar sesión en el portal.",false); Limpiar(); } catch(Exception ex) { Aviso(ex.Message,true); } }
        protected void btnNuevo_Click(object sender, EventArgs e) { Limpiar(); }
    }
}
