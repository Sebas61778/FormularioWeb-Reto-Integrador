<%@ Page Title="Registro Encuentro Académico"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="FormularioWeb._Default" %>

<asp:Content ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style>
        .formulario {
            max-width: 900px;
            margin: 30px auto;
            padding: 30px;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.15);
        }

        .titulo {
            text-align: center;
            margin-bottom: 10px;
        }

        .subtitulo {
            text-align: center;
            color: #666666;
            margin-bottom: 30px;
        }

        .seccion {
            margin-top: 25px;
            margin-bottom: 15px;
            padding-bottom: 5px;
            border-bottom: 2px solid #337ab7;
            color: #337ab7;
        }

        .fila {
            display: flex;
            gap: 20px;
            margin-bottom: 15px;
        }

        .campo {
            flex: 1;
        }

        .campo label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .entrada {
            width: 100%;
            padding: 9px;
            border: 1px solid #cccccc;
            border-radius: 5px;
            box-sizing: border-box;
        }

        .error {
            color: #d9534f;
            font-size: 13px;
            display: block;
            margin-top: 4px;
        }

        .botones {
            text-align: center;
            margin-top: 30px;
        }

        .boton {
            padding: 10px 25px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-weight: bold;
        }

        .registrar {
            background-color: #337ab7;
            color: white;
        }

        .limpiar {
            background-color: #eeeeee;
        }

        .cancelar {
            background-color: #d9534f;
            color: white;
        }

        .resultado {
            margin-top: 25px;
            padding: 20px;
            background-color: #eaf7ea;
            border: 1px solid #a8d5a8;
            border-radius: 8px;
            color: #286628;
        }

        .validaciones {
            margin-top: 20px;
        }
    </style>

    <div class="formulario">

        <h1 class="titulo">REGISTRO AL ENCUENTRO ACADÉMICO</h1>

        <p class="subtitulo">
            Formulario ASP.NET Web Forms - Proyecto de Parcial
        </p>

        <!-- DATOS PERSONALES -->
        <h3 class="seccion">Datos personales</h3>

        <div class="fila">

 



            
<div class="campo">
    <label for="txtCedula">Cédula *</label>

    <asp:TextBox ID="txtCedula" runat="server"
        CssClass="entrada"
        placeholder="Escriba su número de cédula"
        MaxLength="20"></asp:TextBox>

    <asp:RequiredFieldValidator
        ID="rfvCedula"
        runat="server"
        ControlToValidate="txtCedula"
        ErrorMessage="Ingrese su número de cédula."
        Text="* Campo obligatorio."
        CssClass="error">
    </asp:RequiredFieldValidator>
</div>


            <div class="campo">

                <label>Nombres *</label>

                <asp:TextBox ID="txtNombres"
                    runat="server"
                    CssClass="entrada"
                    placeholder="Escriba sus nombres">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvNombres"
                    runat="server"
                    ControlToValidate="txtNombres"
                    ErrorMessage="Ingrese sus nombres."
                    Text="* Campo obligatorio."
                    CssClass="error">
                </asp:RequiredFieldValidator>

            </div>

            <div class="campo">

                <label>Apellidos *</label>

                <asp:TextBox ID="txtApellidos"
                    runat="server"
                    CssClass="entrada"
                    placeholder="Escriba sus apellidos">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvApellidos"
                    runat="server"
                    ControlToValidate="txtApellidos"
                    ErrorMessage="Ingrese sus apellidos."
                    Text="* Campo obligatorio."
                    CssClass="error">
                </asp:RequiredFieldValidator>

            </div>

        </div>


        <!-- CONTACTO -->
        <h3 class="seccion">Información de contacto</h3>

        <div class="fila">

            <div class="campo">

                <label>Correo electrónico *</label>

                <asp:TextBox ID="txtCorreo"
                    runat="server"
                    CssClass="entrada"
                    placeholder="ejemplo@correo.com">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvCorreo"
                    runat="server"
                    ControlToValidate="txtCorreo"
                    ErrorMessage="Ingrese su correo electrónico."
                    Text="* Campo obligatorio."
                    CssClass="error">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="revCorreo"
                    runat="server"
                    ControlToValidate="txtCorreo"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    ErrorMessage="Ingrese un correo válido."
                    Text="* Correo inválido."
                    CssClass="error">
                </asp:RegularExpressionValidator>

            </div>


            <div class="campo">

                <label>Teléfono *</label>

                <asp:TextBox ID="txtTelefono"
                    runat="server"
                    CssClass="entrada"
                    placeholder="3000000000">
                </asp:TextBox>

                <asp:RequiredFieldValidator
                    ID="rfvTelefono"
                    runat="server"
                    ControlToValidate="txtTelefono"
                    ErrorMessage="Ingrese su teléfono."
                    Text="* Campo obligatorio."
                    CssClass="error">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="revTelefono"
                    runat="server"
                    ControlToValidate="txtTelefono"
                    ValidationExpression="^\d{10}$"
                    ErrorMessage="El teléfono debe contener exactamente 10 dígitos."
                    Text="* Teléfono inválido."
                    CssClass="error">
                </asp:RegularExpressionValidator>

            </div>

        </div>


        <!-- INFORMACIÓN ACADÉMICA -->
        <h3 class="seccion">Información académica</h3>

        <div class="fila">

            <div class="campo">

                <label>Programa académico *</label>

                <asp:DropDownList ID="ddlPrograma"
                    runat="server"
                    CssClass="entrada">

                    <asp:ListItem Text="Seleccione un programa" Value="">
                    </asp:ListItem>

                    <asp:ListItem Text="Ingeniería de Sistemas"
                        Value="Ingeniería de Sistemas">
                    </asp:ListItem>

                    <asp:ListItem Text="Ingeniería Industrial"
                        Value="Ingeniería Industrial">
                    </asp:ListItem>

                    <asp:ListItem Text="Ingeniería Electrónica"
                        Value="Ingeniería Electrónica">
                    </asp:ListItem>

                    <asp:ListItem Text="Administración de Empresas"
                        Value="Administración de Empresas">
                    </asp:ListItem>

                </asp:DropDownList>

                <asp:RequiredFieldValidator
                    ID="rfvPrograma"
                    runat="server"
                    ControlToValidate="ddlPrograma"
                    InitialValue=""
                    ErrorMessage="Seleccione un programa."
                    Text="* Seleccione un programa."
                    CssClass="error">
                </asp:RequiredFieldValidator>

            </div>


            <div class="campo">

                <label>Semestre *</label>

                <asp:DropDownList ID="ddlSemestre"
                    runat="server"
                    CssClass="entrada">

                    <asp:ListItem Text="Seleccione" Value="">
                    </asp:ListItem>

                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>

                </asp:DropDownList>

                <asp:RequiredFieldValidator
                    ID="rfvSemestre"
                    runat="server"
                    ControlToValidate="ddlSemestre"
                    InitialValue=""
                    ErrorMessage="Seleccione el semestre."
                    Text="* Seleccione un semestre."
                    CssClass="error">
                </asp:RequiredFieldValidator>

            </div>

        </div>


        <!-- PARTICIPACIÓN -->
        <h3 class="seccion">Participación</h3>

        <div class="fila">

            <div class="campo">

                <label>Tipo de participación *</label>

                <asp:RadioButtonList
                    ID="rblParticipacion"
                    runat="server"
                    RepeatDirection="Vertical">

                    <asp:ListItem Text="Asistente" Value="Asistente">
                    </asp:ListItem>

                    <asp:ListItem Text="Ponente" Value="Ponente">
                    </asp:ListItem>

                    <asp:ListItem Text="Organizador" Value="Organizador">
                    </asp:ListItem>

                </asp:RadioButtonList>

                <asp:CustomValidator
                    ID="cvParticipacion"
                    runat="server"
                    ValidateEmptyText="true"
                    ErrorMessage="Seleccione un tipo de participación."
                    Text="* Seleccione una opción."
                    CssClass="error"
                    OnServerValidate="cvParticipacion_ServerValidate">
                </asp:CustomValidator>

            </div>


            <div class="campo">

                <label>Jornada preferida *</label>

                <asp:DropDownList
                    ID="ddlJornada"
                    runat="server"
                    CssClass="entrada">

                    <asp:ListItem Text="Seleccione" Value="">
                    </asp:ListItem>

                    <asp:ListItem Text="Mañana" Value="Mañana">
                    </asp:ListItem>

                    <asp:ListItem Text="Tarde" Value="Tarde">
                    </asp:ListItem>

                    <asp:ListItem Text="Noche" Value="Noche">
                    </asp:ListItem>

                </asp:DropDownList>

                <asp:RequiredFieldValidator
                    ID="rfvJornada"
                    runat="server"
                    ControlToValidate="ddlJornada"
                    InitialValue=""
                    ErrorMessage="Seleccione una jornada."
                    Text="* Seleccione una jornada."
                    CssClass="error">
                </asp:RequiredFieldValidator>

            </div>

        </div>


        <!-- TEMAS -->
        <h3 class="seccion">Temas de interés</h3>

        <asp:CheckBoxList
            ID="cblTemas"
            runat="server"
            RepeatDirection="Horizontal">

            <asp:ListItem Text="Inteligencia Artificial"
                Value="Inteligencia Artificial">
            </asp:ListItem>

            <asp:ListItem Text="Ciberseguridad"
                Value="Ciberseguridad">
            </asp:ListItem>

            <asp:ListItem Text="Bases de Datos"
                Value="Bases de Datos">
            </asp:ListItem>

            <asp:ListItem Text="Desarrollo Web"
                Value="Desarrollo Web">
            </asp:ListItem>

        </asp:CheckBoxList>

        <asp:CustomValidator
            ID="cvTemas"
            runat="server"
            ValidateEmptyText="true"
            ErrorMessage="Seleccione al menos un tema."
            Text="* Seleccione al menos un tema."
            CssClass="error"
            OnServerValidate="cvTemas_ServerValidate">
        </asp:CustomValidator>


        <!-- COMENTARIOS -->
        <h3 class="seccion">Observaciones</h3>

        <asp:TextBox
            ID="txtComentarios"
            runat="server"
            CssClass="entrada"
            TextMode="MultiLine"
            Rows="4"
            placeholder="Escriba una observación o expectativa">
        </asp:TextBox>

        <asp:CustomValidator
            ID="cvComentarios"
            runat="server"
            ValidateEmptyText="true"
            ErrorMessage="Los comentarios no pueden superar los 300 caracteres."
            Text="* Máximo 300 caracteres."
            CssClass="error"
            OnServerValidate="cvComentarios_ServerValidate">
        </asp:CustomValidator>


        <!-- AUTORIZACIÓN -->
        <h3 class="seccion">Autorización</h3>

        <asp:CheckBox
            ID="chkAcepta"
            runat="server"
            Text="Acepto el tratamiento de mis datos para fines académicos." />

        <asp:CustomValidator
            ID="cvAcepta"
            runat="server"
            ErrorMessage="Debe aceptar el tratamiento de datos."
            Text="* Debe aceptar el tratamiento de datos."
            CssClass="error"
            OnServerValidate="cvAcepta_ServerValidate">
        </asp:CustomValidator>


        <!-- RESUMEN DE VALIDACIONES -->
        <div class="validaciones">

            <asp:ValidationSummary
                ID="ValidationSummary1"
                runat="server"
                HeaderText="Revise los siguientes campos:"
                CssClass="error" />

        </div>


        <!-- BOTONES -->
        <div class="botones">

            <asp:Button
                ID="btnRegistrar"
                runat="server"
                Text="REGISTRAR"
                CssClass="boton registrar"
                OnClick="btnRegistrar_Click" />

            <asp:Button
                ID="btnLimpiar"
                runat="server"
                Text="LIMPIAR"
                CssClass="boton limpiar"
                CausesValidation="false"
                OnClick="btnLimpiar_Click" />

            <asp:Button
                ID="btnCancelar"
                runat="server"
                Text="CANCELAR"
                CssClass="boton cancelar"
                CausesValidation="false"
                OnClick="btnCancelar_Click" />

            
           <asp:Button 
                ID="btnActualizar" runat="server"
                Text="ACTUALIZAR"
                CssClass="boton btn-registrar"
                OnClick="btnActualizar_Click" />

           <asp:Button 
                ID="btnEliminar" runat="server"
                Text="ELIMINAR"
                CssClass="boton btn-cancelar"
                OnClick="btnEliminar_Click"
                OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este registro?');" />


        </div>

        <div class="campo">
    <label for="txtBuscarCedula">Buscar registro por cédula</label>

    <asp:TextBox ID="txtBuscarCedula" runat="server"
        CssClass="entrada"
        placeholder="Ingrese la cédula del registro" />

    
<asp:Button ID="btnBuscar" runat="server"
    Text="BUSCAR"
    CssClass="btn btn-primary"
    CausesValidation="false"
    OnClick="btnBuscar_Click" />

</div>


        <!-- RESULTADO -->
        <asp:Panel
            ID="pnlResultado"
            runat="server"
            CssClass="resultado"
            Visible="false">

            <asp:Label
                ID="lblMensaje"
                runat="server">
            </asp:Label>

        </asp:Panel>

    </div>

</asp:Content>