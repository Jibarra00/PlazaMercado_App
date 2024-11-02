using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFUsuario : System.Web.UI.Page
    {
        //Crear los objetos
        UsuarioLog objUsu = new UsuarioLog();
        ClienteLog objCli = new ClienteLog();
        RolLog objRol = new RolLog();
        EmpleadoLog objEmp = new EmpleadoLog();

        private int _id, _fkrol, _fkempleado, _fkcliente;
        private string _email, _password, _salt, _state;
        private DateTime _Create_Date;

        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showUsuario();
                showClienteDDL();
                showEmpleadoDDL();
                showRolDDL();
            }
            
        }
        private void showUsuario()
        {
            DataSet ds = new DataSet();
            ds = objUsu.showUsuario();
            GVUsuarios.DataSource = ds;
            GVUsuarios.DataBind();
        }
        private void showClienteDDL()
        {
            DDLCliente.DataSource = objCli.showClientDDL();
            DDLCliente.DataValueField = "cli_id";
            DDLCliente.DataTextField = "cli_nombres";
            DDLCliente.DataBind();
            DDLCliente.Items.Insert(0, "Seleccione");
        }
        private void showRolDDL()
        {
            DDLRol.DataSource = objRol.showrolDDL();
            DDLRol.DataValueField = "rol_id";
            DDLRol.DataTextField = "rol_nombre";
            DDLRol.DataBind();
            DDLRol.Items.Insert(0, "Seleccione Rol");
        }
        private void showEmpleadoDDL()
        {
            DDLEmpleado.DataSource = objEmp.showEmployeesDDL();
            DDLEmpleado.DataValueField = "emp_id";
            DDLEmpleado.DataTextField = "emp_nombres";
            DDLEmpleado.DataBind();
            DDLEmpleado.Items.Insert(0, "Seleccione empleado");
        }

        //Eventos cuando se ejecutan al dar click en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {

            _email = TBEmail.Text;
            _password = TBPassword.Text;
            _salt = TBSalt.Text;
            _state = TBSalt.Text;
            _Create_Date = Convert.ToDateTime(TBDate.Text);
            _fkrol = Convert.ToInt32(DDLRol.SelectedValue);
            _fkempleado = Convert.ToInt32(DDLEmpleado.SelectedValue);
            _fkcliente = Convert.ToInt32(DDLCliente.SelectedValue);

            executed = objUsu.saveUsuario(_email, _password, _salt, _state, _Create_Date, _fkrol, _fkempleado, _fkcliente);

            
                if (executed)
                {
                    LblMsg.Text = "El usuario se creo correctamente";
                    showUsuario();
                }
                else
                {
                    LblMsg.Text = "Error al guardar";
                }
            
            
        }
    }
}