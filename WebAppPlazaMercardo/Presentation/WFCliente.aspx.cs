using Logic;
using Model;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFCliente : System.Web.UI.Page
    {
        //Se crea el Objeto
        ClienteLog objCli = new ClienteLog();
        //Se definen los atributos
        private int _id;
        private string _names, _lastnames, _mail, _phone, _addres;

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
                FrmCliente.Visible = false;
                //showClients();
            }
            validatePermisoRol();
        }
        /*
 * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
 * parte de un servicio web, lo que significa que puede ser invocado de manera
 * remota a través de HTTP.
 */
        [WebMethod]
        public static object ListClients()
        {
            ClienteLog objCli = new ClienteLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objCli.showClient();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var clientsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                clientsList.Add(new
                {
                    ClientID = row["cli_id"],
                    Name = row["cli_nombres"],
                    lastName = row["cli_apellidos"],
                    Mail = row["cli_correo"],
                    Phone = row["cli_telefono"],
                    Addres = row["cli_direccion"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de clientes
            return new { data = clientsList };
        }


        [WebMethod]
        public static bool DeleteClient(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            ClienteLog objCli = new ClienteLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objCli.deleteClient(id);
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
                            FrmCliente.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmCliente.Visible = true;
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
                            FrmCliente.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmCliente.Visible = false;
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
                            FrmCliente.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmCliente.Visible = false;
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
            HFClientID.Value = "";
            TBName.Text = "";
            TBlastname.Text = "";
            TBMail.Text = "";
            TBPhone.Text = "";
            TBAddres.Text = "";
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFClientID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un cliente para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFClientID.Value);
            _names = TBName.Text;
            _lastnames = TBlastname.Text;
            _mail = TBMail.Text;
            _phone = TBPhone.Text;
            _addres = TBAddres.Text;


            executed = objCli.updateClient(_id, _names, _lastnames, _mail, _phone, _addres);

            if (executed)
            {
                LblMsg.Text = "El cliente se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _names = TBName.Text;
            _lastnames = TBlastname.Text;
            _mail = TBMail.Text;
            _phone = TBPhone.Text;
            _addres = TBAddres.Text;

            executed = objCli.saveClient(_names, _lastnames, _mail, _phone, _addres);
            if (executed)
            {
                LblMsg.Text = "El CLiente se guardo exitosamente";
            }
            else
            {
                LblMsg.Text = "Error al Guardar";
            }

        }
    }
}