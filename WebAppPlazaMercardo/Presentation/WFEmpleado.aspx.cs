using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFEmpleado : System.Web.UI.Page
    {
        //Se crea el Objeto
        EmpleadoLog objEmp = new EmpleadoLog();

        //Se definen los atributos
        private string _identification, _names, _lastnames, _phone, _addres;
        private int _id;    

        

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
                FrmEmpleado.Visible = false;
                //showEmployees();
            }
            validatePermisoRol();
        }

        //Metodo para mostrar todos los productos
        /*
      * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
      * parte de un servicio web, lo que significa que puede ser invocado de manera
      * remota a través de HTTP.
      */
        [WebMethod]
        public static object ListEmployees()
        {
            try
            {
                EmpleadoLog objEmp = new EmpleadoLog();

                // Se obtiene un DataSet que contiene la lista de empleados desde la base de datos.
                var dataSet = objEmp.showEmployees();

                // Validación para comprobar si hay datos en el DataSet
                if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                {
                    return new { data = new List<object>(), success = false, message = "No hay empleados para mostrar" };
                }

                // Se crea una lista para almacenar los empleados que se van a devolver.
                var employeesList = new List<object>();

                // Se itera sobre cada fila del DataSet (que representa un empleado).
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    employeesList.Add(new
                    {
                        EmployeeID = row["emp_id"],
                        Identification = row["emp_identificacion"],
                        Name = row["emp_nombres"],
                        lastName = row["emp_apellidos"],
                        Phone = row["emp_telefono"],
                        Addres = row["emp_direccion"]
                    });
                }

                // Devuelve un objeto en formato JSON que contiene la lista de empleados.
                return new { data = employeesList, success = true };
            }
            catch (Exception ex)
            {
                // Registro de errores para depuración
                return new { data = new List<object>(), success = false, message = ex.Message };
            }
        }


        [WebMethod]
        public static bool DeleteEmployee(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            EmpleadoLog objEmp = new EmpleadoLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objEmp.deleteClient(id);
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
                            FrmEmpleado.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmEmpleado.Visible = true;
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
                            FrmEmpleado.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmEmpleado.Visible = false;
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
                            FrmEmpleado.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmEmpleado.Visible = false;
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

        private void clear()
        {
            HFEmployeeID.Value = "";
            TBCC.Text = "";
            TBname.Text = "";
            TBlastname.Text = "";
            TBphone.Text = "";
            TBaddres.Text = "";
        }

       

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un empleado para actualizar
            if (string.IsNullOrEmpty(HFEmployeeID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un Empleado para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFEmployeeID.Value);
            _identification = TBCC.Text;
            _names = TBname.Text;
            _lastnames = TBlastname.Text;
            _phone = TBphone.Text;
            _addres = TBaddres.Text;

            executed = objEmp.updateEmployee(_id, _identification, _names, _lastnames, _phone, _addres);

            if (executed)
            {
                LblMsg.Text = "El Empleado se actualizó exitosamente!";
                clear(); // Limpiar los campos después de actualizar
            }
            else
            {
                LblMsg.Text = "Error al actualizar.";
            }
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            {
                _identification = TBCC.Text;
                _names = TBname.Text;
                _lastnames = TBlastname.Text;
                _phone = TBphone.Text;
                _addres = TBaddres.Text;

                executed = objEmp.saveEmployee(_identification, _names, _lastnames, _phone, _addres);
                if (executed)
                {
                    LblMsg.Text = "El Empleado se guardo exitosamente";
                }
                else
                {
                    LblMsg.Text = "Error al Guardar";
                }


            }


        }   
    }
}
