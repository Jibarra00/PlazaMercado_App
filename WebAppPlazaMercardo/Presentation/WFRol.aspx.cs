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

namespace Presentation
{
    public partial class WFRol : System.Web.UI.Page
    {
        //crear los objetos
        RolLog objRol = new RolLog();

        private int _id;
        private string _nombre, _descripcion;
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
                FrmRol.Visible = false;
                //aqui se invocan todos los metodos
                //showRol();
                
            }

            validatePermisoRol();
        }
        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListRol()
        {

            RolLog objRol = new RolLog();
            //se obtine el dataset  que  contiene la lista  de roles de la base de datos.
            var dataSet = objRol.showRol();
            //se crea una lista para almacenar los roles que se van a devolver
            var RolList = new List<object>();
            //seitera sobre cada fila del dataset(que se presentan un rol).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                RolList.Add(new
                {
                    rolID = row["rol_id"],
                    Nombre = row["rol_nombre"],
                    Descripcion = row["rol_descripcion"]

                });
               
            }
            //devuelve un objeto en formato JSON que contiene la lista de roles.
            return new {data = RolList};
        }
        [WebMethod]
        public static bool DeleteRol(int id) 
        {
            // Crear una instancia de la clase de lógica de roles
            RolLog objRol = new RolLog();

            // Invocar al método para eliminar el rol y devolver el resultado
            return objRol.deleteRol(id);
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
                            FrmRol.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmRol.Visible = true;
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
                            FrmRol.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmRol.Visible = false;
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
                            FrmRol.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmRol.Visible = false;
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

        //metodo para limpiar los texbox 
        private void clear() 
        {
            HFRolID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
        }

        


        //eventos que se ejecutan cuando se da clic en los botones

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text;

            executed = objRol.saveRol(_nombre,_descripcion);

            if (executed)
            {
                LblMsg.Text = "el rol se guardo exitosamente";
                clear();

            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //verificar si se ha seleccioando  un rol para actualizar
            if (string.IsNullOrEmpty(HFRolID.Value))
            {
                LblMsg.Text = "No se ha sellecionado un rol para actualizar";
                return;
            }
            _id = Convert.ToInt32(HFRolID.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBDescripcion.Text ;

            executed = objRol.updateRol(_id, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "Se actuaslizo el Rol";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }

        }


    }
}