using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace FormularioWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        // ==========================================
        // BOTÓN REGISTRAR
        // ==========================================

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                string cedula = txtCedula.Text.Trim();
                string nombres = txtNombres.Text.Trim();
                string apellidos = txtApellidos.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                string programa = ddlPrograma.SelectedValue;
                string semestre = ddlSemestre.SelectedValue;
                string participacion = rblParticipacion.SelectedValue;
                string jornada = ddlJornada.SelectedValue;

                string temas = ObtenerTemasSeleccionados();

                string comentarios = txtComentarios.Text.Trim();

                string codigoRegistro =
                    "EVT-" + DateTime.Now.ToString("yyyyMMddHHmmss");


                string cadenaConexion = ConfigurationManager
                    .ConnectionStrings["ConexionFormulario"]
                    .ConnectionString;

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    string consulta = @"INSERT INTO Estudiantes
    (Cedula, CodigoRegistro, Nombres, Apellidos, Correo,
     Telefono, Programa, Semestre, Participacion, Jornada,
     TemasInteres, Comentarios, AceptaTratamiento)
    VALUES
    (@Cedula, @CodigoRegistro, @Nombres, @Apellidos, @Correo,
     @Telefono, @Programa, @Semestre, @Participacion, @Jornada,
     @TemasInteres, @Comentarios, @AceptaTratamiento)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Cedula", cedula);
                        comando.Parameters.AddWithValue("@CodigoRegistro", codigoRegistro);
                        comando.Parameters.AddWithValue("@Nombres", nombres);
                        comando.Parameters.AddWithValue("@Apellidos", apellidos);
                        comando.Parameters.AddWithValue("@Correo", correo);
                        comando.Parameters.AddWithValue("@Telefono", telefono);
                        comando.Parameters.AddWithValue("@Programa", programa);
                        comando.Parameters.AddWithValue("@Semestre", int.Parse(semestre));
                        comando.Parameters.AddWithValue("@Participacion", participacion);
                        comando.Parameters.AddWithValue("@Jornada", jornada);
                        comando.Parameters.AddWithValue("@TemasInteres", temas);
                        comando.Parameters.AddWithValue(
                            "@Comentarios",
                            string.IsNullOrWhiteSpace(comentarios)
                                ? (object)DBNull.Value
                                : comentarios);
                        comando.Parameters.AddWithValue("@AceptaTratamiento", chkAcepta.Checked);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }


                lblMensaje.Text =
                    "<h3>¡Registro realizado correctamente!</h3>" +
                    "<p><strong>Cédula:</strong> " + Server.HtmlEncode(cedula) + "</p>" +
                    "<p><strong>Hola, " + Server.HtmlEncode(nombres) +
                    " " + Server.HtmlEncode(apellidos) + ".</strong></p>" +

                    "<p>Tu participación fue registrada correctamente.</p>" +

                    "<p><strong>Código de registro:</strong> " +
                    codigoRegistro + "</p>" +

                    "<p><strong>Correo:</strong> " +
                    Server.HtmlEncode(correo) + "</p>" +

                    "<p><strong>Teléfono:</strong> " +
                    Server.HtmlEncode(telefono) + "</p>" +

                    "<p><strong>Programa:</strong> " +
                    Server.HtmlEncode(programa) + "</p>" +

                    "<p><strong>Semestre:</strong> " +
                    Server.HtmlEncode(semestre) + "</p>" +

                    "<p><strong>Participación:</strong> " +
                    Server.HtmlEncode(participacion) + "</p>" +

                    "<p><strong>Jornada:</strong> " +
                    Server.HtmlEncode(jornada) + "</p>" +

                    "<p><strong>Temas de interés:</strong> " +
                    Server.HtmlEncode(temas) + "</p>";

                if (!string.IsNullOrEmpty(comentarios))
                {
                    lblMensaje.Text +=
                        "<p><strong>Comentarios:</strong> " +
                        Server.HtmlEncode(comentarios) + "</p>";
                }

                pnlResultado.Visible = true;
            }
            else
            {
                pnlResultado.Visible = false;
            }
        }


        // ==========================================
        // OBTENER TEMAS SELECCIONADOS
        // ==========================================

        private string ObtenerTemasSeleccionados()
        {
            List<string> temas = new List<string>();

            foreach (ListItem item in cblTemas.Items)
            {
                if (item.Selected)
                {
                    temas.Add(item.Text);
                }
            }

            return string.Join(", ", temas);
        }


        // ==========================================
        // VALIDAR PARTICIPACIÓN
        // ==========================================

        protected void cvParticipacion_ServerValidate(
            object source,
            ServerValidateEventArgs args)
        {
            args.IsValid = rblParticipacion.SelectedIndex >= 0;
        }


        // ==========================================
        // VALIDAR TEMAS
        // ==========================================

        protected void cvTemas_ServerValidate(
            object source,
            ServerValidateEventArgs args)
        {
            bool seleccionado = false;

            foreach (ListItem item in cblTemas.Items)
            {
                if (item.Selected)
                {
                    seleccionado = true;
                    break;
                }
            }

            args.IsValid = seleccionado;
        }


        // ==========================================
        // VALIDAR COMENTARIOS
        // ==========================================

        protected void cvComentarios_ServerValidate(
            object source,
            ServerValidateEventArgs args)
        {
            string comentarios = txtComentarios.Text.Trim();

            args.IsValid = comentarios.Length <= 300;
        }


        // ==========================================
        // VALIDAR ACEPTACIÓN
        // ==========================================

        protected void cvAcepta_ServerValidate(
            object source,
            ServerValidateEventArgs args)
        {
            args.IsValid = chkAcepta.Checked;
        }


        // ==========================================
        // BOTÓN LIMPIAR
        // ==========================================

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";

            ddlPrograma.SelectedIndex = 0;
            ddlSemestre.SelectedIndex = 0;
            ddlJornada.SelectedIndex = 0;

            rblParticipacion.ClearSelection();

            foreach (ListItem item in cblTemas.Items)
            {
                item.Selected = false;
            }

            txtComentarios.Text = "";

            chkAcepta.Checked = false;

            pnlResultado.Visible = false;
            lblMensaje.Text = "";

            foreach (BaseValidator validator in Page.Validators)
            {
                validator.IsValid = true;
            }
        }


        // ==========================================
        // BOTÓN CANCELAR
        // ==========================================

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";

            ddlPrograma.SelectedIndex = 0;
            ddlSemestre.SelectedIndex = 0;
            ddlJornada.SelectedIndex = 0;

            rblParticipacion.ClearSelection();

            foreach (ListItem item in cblTemas.Items)
            {
                item.Selected = false;
            }

            txtComentarios.Text = "";

            chkAcepta.Checked = false;

            pnlResultado.Visible = true;

            lblMensaje.Text =
                "<h3>Registro cancelado</h3>" +
                "<p>No se ha realizado ningún registro.</p>";

            foreach (BaseValidator validator in Page.Validators)
            {
                validator.IsValid = true;
            }
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string cedulaBuscar = txtBuscarCedula.Text.Trim();

            if (string.IsNullOrWhiteSpace(cedulaBuscar))
            {
                lblMensaje.Text = "Ingresa una cédula para buscar.";
                pnlResultado.Visible = true;
                return;
            }

            string cadenaConexion = ConfigurationManager
                .ConnectionStrings["ConexionFormulario"]
                .ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = @"SELECT * FROM Estudiantes
                            WHERE Cedula = @Cedula";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Cedula", cedulaBuscar);
                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            txtCedula.Text = lector["Cedula"].ToString();
                            txtNombres.Text = lector["Nombres"].ToString();
                            txtApellidos.Text = lector["Apellidos"].ToString();
                            txtCorreo.Text = lector["Correo"].ToString();
                            txtTelefono.Text = lector["Telefono"].ToString();

                            ddlPrograma.SelectedValue = lector["Programa"].ToString();
                            ddlSemestre.SelectedValue = lector["Semestre"].ToString();

                            rblParticipacion.SelectedValue =
                                lector["Participacion"].ToString();

                            ddlJornada.SelectedValue = lector["Jornada"].ToString();

                            txtComentarios.Text =
                                lector["Comentarios"] == DBNull.Value
                                ? ""
                                : lector["Comentarios"].ToString();

                            chkAcepta.Checked =
                                Convert.ToBoolean(lector["AceptaTratamiento"]);

                            string temasGuardados =
                                lector["TemasInteres"] == DBNull.Value
                                ? ""
                                : lector["TemasInteres"].ToString();

                            foreach (ListItem item in cblTemas.Items)
                            {
                                item.Selected = temasGuardados
                                    .Split(',')
                                    .Select(t => t.Trim())
                                    .Contains(item.Text.Trim());
                            }

                            lblMensaje.Text =
                                "Registro encontrado. Puedes modificar sus datos.";
                            pnlResultado.Visible = true;
                        }
                        else
                        {
                            lblMensaje.Text =
                                "No se encontró un registro con esa cédula.";
                            pnlResultado.Visible = true;
                        }
                    }
                }
            }
        }




        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string cedulaOriginal = txtBuscarCedula.Text.Trim();

            if (string.IsNullOrWhiteSpace(cedulaOriginal))
            {
                lblMensaje.Text = "Primero busca el registro que deseas actualizar.";
                pnlResultado.Visible = true;
                return;
            }

            string cadenaConexion = ConfigurationManager
                .ConnectionStrings["ConexionFormulario"]
                .ConnectionString;

            string consulta = @"UPDATE Estudiantes
        SET Cedula = @Cedula,
            Nombres = @Nombres,
            Apellidos = @Apellidos,
            Correo = @Correo,
            Telefono = @Telefono,
            Programa = @Programa,
            Semestre = @Semestre,
            Participacion = @Participacion,
            Jornada = @Jornada,
            TemasInteres = @TemasInteres,
            Comentarios = @Comentarios,
            AceptaTratamiento = @AceptaTratamiento
        WHERE Cedula = @CedulaOriginal";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@CedulaOriginal", cedulaOriginal);
                comando.Parameters.AddWithValue("@Cedula", txtCedula.Text.Trim());
                comando.Parameters.AddWithValue("@Nombres", txtNombres.Text.Trim());
                comando.Parameters.AddWithValue("@Apellidos", txtApellidos.Text.Trim());
                comando.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                comando.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                comando.Parameters.AddWithValue("@Programa", ddlPrograma.SelectedValue);
                comando.Parameters.AddWithValue("@Semestre", int.Parse(ddlSemestre.SelectedValue));
                comando.Parameters.AddWithValue("@Participacion", rblParticipacion.SelectedValue);
                comando.Parameters.AddWithValue("@Jornada", ddlJornada.SelectedValue);
                comando.Parameters.AddWithValue("@TemasInteres", ObtenerTemasSeleccionados());
                comando.Parameters.AddWithValue(
                    "@Comentarios",
                    string.IsNullOrWhiteSpace(txtComentarios.Text)
                        ? (object)DBNull.Value
                        : txtComentarios.Text.Trim());
                comando.Parameters.AddWithValue("@AceptaTratamiento", chkAcepta.Checked);

                conexion.Open();
                int filas = comando.ExecuteNonQuery();

                lblMensaje.Text = filas > 0
                    ? "Registro actualizado correctamente."
                    : "No se encontró el registro que deseas actualizar.";

                pnlResultado.Visible = true;
            }
        }




        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string cedulaBuscar = txtBuscarCedula.Text.Trim();

            if (string.IsNullOrWhiteSpace(cedulaBuscar))
            {
                lblMensaje.Text = "Primero busca el registro que deseas eliminar.";
                pnlResultado.Visible = true;
                return;
            }

            string cadenaConexion = ConfigurationManager
                .ConnectionStrings["ConexionFormulario"]
                .ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = "DELETE FROM Estudiantes WHERE Cedula = @Cedula";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Cedula", cedulaBuscar);

                    conexion.Open();
                    int filas = comando.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        lblMensaje.Text = "Registro eliminado correctamente.";

                        txtBuscarCedula.Text = "";
                        txtCedula.Text = "";
                        txtNombres.Text = "";
                        txtApellidos.Text = "";
                        txtCorreo.Text = "";
                        txtTelefono.Text = "";
                        txtComentarios.Text = "";

                        pnlResultado.Visible = true;
                    }
                    else
                    {
                        lblMensaje.Text = "No se encontró el registro que deseas eliminar.";
                        pnlResultado.Visible = true;
                    }
                }
            }
        }

    }

}
