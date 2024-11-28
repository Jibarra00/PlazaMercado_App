using Logic;
using Model;
using SimpleCrypto;
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

        private int _id, _fkrol, _fkempleado;
        private string _email, _password, _salt, _state, _encryptedPassword;
        private DateTime _Create_Date;

        private bool executed = false;
        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                PanelAdmin.Visible = false;
                FrmUsuarios.Visible = false;
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //showUsuario();
                
                showEmpleadoDDL();
                showRolDDL();
            }
            validatePermisoRol();
            
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
                    Correo = row["usu_correo"],
                    Contraseña = row["usu_contrasena"],
                    Salt = row["usu_salt"],
                    Estado = row["usu_estado"],
                    Fecha_creacion = Convert.ToDateTime(row["usu_fecha_creacion"]).ToString("yyyy-MM-dd"),
                    FkRol = row["tbl_rol_rol_id"],
                    nameRol = row["rol_nombre"],
                    FkEmpleado = row["tbl_empleado_emp_id"],
                    nameEmpleado = row["emp_nombres"],
                    
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

        private void validatePermisoRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("WFDefault.aspx");
                return;
            }
            // Obtener el rol del usuario
            var userRole = objUser.Rol.Nombre;

            if (userRole == "Administrador")
            {
                //LblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmUsuarios.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmUsuarios.Visible = true;
                            BtnUpdate.Visible = true;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = true;// Se pone visible el panel
                            _showEditButton = true;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;// Se pone visible el boton eliminar dentro de la datatable
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Empleado")
            {
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUsuario.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermiso.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkPermisoRol.Visible = false;// Se oculta el enlace de Permiso Rol
                masterPage.linkCategoria.Visible = false;
                masterPage.linkComentario.Visible = false;
                masterPage.linkGestion.Visible = false;
                masterPage.linkCliente.Visible = false;
                masterPage.linkEmpleado.Visible = false;
                masterPage.linkRol.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmUsuarios.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmUsuarios.Visible = false;
                            BtnUpdate.Visible = false;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = false;// Se pone visible el panel
                            _showEditButton = false;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = false;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = false;
                            _showDeleteButton = false;// Se pone visible el boton eliminar dentro de la datatable
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Cliente")
            {
                //LblMsg.Text = "Bienvenido, Secretaria!";
                masterPage.linkUsuario.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermiso.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkPermisoRol.Visible = false;// Se oculta el enlace de Permiso Rol
                masterPage.linkCategoria.Visible = false;
                masterPage.linkGestion.Visible = false;
                masterPage.linkRol.Visible = false;
                masterPage.linkCliente.Visible = false;
                masterPage.linkEmpleado.Visible = false;
                masterPage.linkPedidos.Visible = false;
                masterPage.linkProveedor.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmUsuarios.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmUsuarios.Visible = false;
                            BtnUpdate.Visible = false;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = false;// Se pone visible el panel
                            _showEditButton = false;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = false;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = false;
                            _showDeleteButton = false;// Se pone visible el boton eliminar dentro de la datatable
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }
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
            DDLState.SelectedIndex = 0;
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DDLRol.SelectedIndex = 0;
            DDLEmpleado.SelectedIndex = 0;
            
        }


        //Eventos cuando se ejecutan al dar click en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            /*
             * PBKDF2: Password-Based Key Derivation Function 2, es un algoritmo para proteger contraseñas,
             * ya que es seguro contra ataques de fuerza bruta, genera un hash mediante múltiples iteraciones
             */
            ICryptoService cryptoService = new PBKDF2();

            _email = TBEmail.Text;
            _password = TBPassword.Text;
            _salt = cryptoService.GenerateSalt();// Se generar un salt único para esa contraseña.
            _encryptedPassword = cryptoService.Compute(_password);// Se generar un hash de la contraseña.

            _state = DDLState.SelectedValue;
            _Create_Date = DateTime.Parse(TBDate.Text);
            _fkrol = Convert.ToInt32(DDLRol.SelectedValue);
            _fkempleado = Convert.ToInt32(DDLEmpleado.SelectedValue);

            executed = objUsu.saveUsuario(_email, _encryptedPassword, _salt, _state, _Create_Date, _fkrol, _fkempleado);

            
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

            ICryptoService cryptoService = new PBKDF2();

            _id = Convert.ToInt32(HFUsuarioID.Value);
            _email = TBEmail.Text;
            _password = TBPassword.Text;
            _salt = cryptoService.GenerateSalt();// Se generar un salt único para esa contraseña.
            _encryptedPassword = cryptoService.Compute(_password);// Se generar un hash de la contraseña.


            _state = DDLState.SelectedValue;
            _Create_Date = DateTime.Parse(TBDate.Text);
            _fkrol = Convert.ToInt32(DDLRol.SelectedValue);
            _fkempleado = Convert.ToInt32(DDLEmpleado.SelectedValue);

            executed = objUsu.updateUsuario(_id,_email, _encryptedPassword, _salt,_state,_Create_Date,_fkrol,_fkempleado);

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