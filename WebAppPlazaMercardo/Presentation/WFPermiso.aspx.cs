using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace Presentation
{
    public partial class WFPermiso : System.Web.UI.Page
    {
        PermisoLog objPer = new PermisoLog();

        private int _id;
        private string _name, _description;
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
                FrmPermiso.Visible = false;
                //showPermisos();
            }
            validatePermisoRol();
        }
        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListPermiso()
        {
            PermisoLog objPer = new PermisoLog();

            var dataSet = objPer.showPermisos();

            var permisoList = new List<object>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                permisoList.Add(new
                {
                    PermisoID = row["per_id"],
                    Nombre = row["per_nombre"],
                    Descripcion = row["per_descripcion"]
                });
            }
            return new {data = permisoList};
        }

        [WebMethod]
        public static bool DeletePermiso(int id)
        {
            PermisoLog objPer = new PermisoLog();

            return objPer.deletePermiso(id);
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
                            FrmPermiso.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPermiso.Visible = true;
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
                            FrmPermiso.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPermiso.Visible = false;
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
                            FrmPermiso.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPermiso.Visible = false;
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
            HFPermisoID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
        }

        

        protected void BTSave_Click(object sender, EventArgs e)
        {
            _name = TBNombre.Text;
            _description = TBDescripcion.Text;

            executed = objPer.savePermiso(_name, _description);
            if (executed)
            {
                LblMsg.Text = "Se guardo correctamente";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }
        }

        protected void BTUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFPermisoID.Value))
            {
                LblMsg.Text = "No se ha seleccionado ningun permiso para actualizar.";
                return;
            }
            
            _id = Convert.ToInt32(HFPermisoID.Value);
            _name = TBNombre.Text;
            _description = TBDescripcion.Text;

            executed = objPer.updatePermiso(_id, _name, _description);

            if (executed)
            {
                LblMsg.Text = "Se actualizo correctamente";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar ";
            }
        }


    }
}