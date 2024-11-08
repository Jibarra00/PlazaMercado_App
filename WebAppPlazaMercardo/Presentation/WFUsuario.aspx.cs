using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
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
                //showUsuario();
                showClienteDDL();
                showEmpleadoDDL();
                showRolDDL();
            }
            
        }
        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListUsuario()
        {
            UsuarioLog objUsu = new UsuarioLog();

            // Se obtiene un DataSet que contiene la lista de los usuarios desde la base de datos.
            var dataSet = objUsu.showUsuario();
            // Se crea una lista para almacenar los usuarios que se van a devolver.
            var usuarioList = new List<object>();
            // Se itera sobre cada fila del DataSet (que representa un usuario).
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                usuarioList.Add(new
                {
                    UsuarioID = row["usu_id"],
                    Correo = row["usu_contrasena"],
                    Salt = row["usu_salt"],
                    Estado = row["usu_estado"],
                    Fecha_creacion = row["usu_fecha_creacion"],
                    FkRol = row["tbl_rol_rol_id"],
                    nameRol = row["rol_nombre"],
                    FkEmpleado = row["tbl_empleado_emp_id"],
                    nameEmpleado = row["emp_nombres"],
                    FkCliente = row["tbl_cliente_cli_id"],
                    nameCliente = row["cli_nombres"]
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de usuarios.
            return new {data = usuarioList};
        }

        

        //metodo para eliminar un usuario por su id
        [WebMethod]
        public static bool DeleteUsuario(int id)
        {
            // Crear una instancia de la clase de lógica de un usuario
            UsuarioLog objUsu = new UsuarioLog();
            // invocar al metodo para eliminar un usuario y devolver el resultado
            return objUsu.deleteUsuario(id);

        }
        private void showClienteDDL()
        {
            DDLCliente.DataSource = objCli.showClientDDL();
            DDLCliente.DataValueField = "cli_id";
            DDLCliente.DataTextField = "cli_nombres";
            DDLCliente.DataBind();
            DDLCliente.Items.Insert(0, "Seleccione cliente");
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

        //Metodo para limpiar los Texbox y los DDL
        private void clear()
        {
            HFUsuarioID.Value = "";
            TBEmail.Text = "";
            TBPassword.Text = "";
            TBSalt.Text = "";
            TBState.Text = "";
            TBDate.Text = "";
            DDLRol.SelectedIndex = 0;
            DDLEmpleado.SelectedIndex = 0;
            DDLCliente.SelectedIndex = 0;
            
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
                    clear();
                }
                else
                {
                    LblMsg.Text = "Error al guardar";
                }
            
            
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un usuario para actualizar
            if (string.IsNullOrEmpty(HFUsuarioID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un usuario para actualizar";
                return;
            }
            _id = Convert.ToInt32(HFUsuarioID.Value);
            _email = TBEmail.Text;
            _password = TBPassword.Text;
            _salt = TBSalt.Text;
            _state = TBSalt.Text;
            _Create_Date = Convert.ToDateTime(TBDate.Text);
            _fkrol = Convert.ToInt32(DDLRol.SelectedValue);
            _fkempleado = Convert.ToInt32(DDLEmpleado.SelectedValue);
            _fkcliente = Convert.ToInt32(DDLCliente.SelectedValue);

            executed = objUsu.updateUsuario(_id,_email,_password,_salt,_state,_Create_Date,_fkrol,_fkempleado,_fkcliente);

            if (executed)
            {
                LblMsg.Text = "El usuario se actualizo correctamente";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }


    }
}