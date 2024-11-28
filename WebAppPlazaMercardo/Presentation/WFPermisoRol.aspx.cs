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
    public partial class WFPermisoRol : System.Web.UI.Page
    {
        PermisoLog objPer = new PermisoLog();
        RolLog objRol = new RolLog();
        PermisoRolLog objPerRol = new PermisoRolLog();

        private int _id, _fkRol, _fkPermiso;
        private DateTime _date;
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
                FrmPermisoRol.Visible = false;
                TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                showPermisoDDL();
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
        public static object ListPermisoRol()
        {
            PermisoRolLog objPerRol = new PermisoRolLog();

            var dataSet = objPerRol.showPermisoRol();

            var permisosRolList = new List<object>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                permisosRolList.Add(new
                {
                    PermisoRolID = row["rol_permiso"],
                    FkPermiso = row["tbl_permiso_per_id"],
                    namePermiso = row["per_nombre"],
                    FkRol = row["tbl_rol_rol_id"],
                    nameRol = row["rol_nombre"],
                    Date = Convert.ToDateTime(row["per_rol_fecha_asignacion"]).ToString("yyyy-MM-dd ddd")
                });
            }
            return new { data = permisosRolList };
        }
        [WebMethod]

        public static bool DeletePermisosRol(int id)
        {
            PermisoRolLog objPerRol = new PermisoRolLog();

            return objPerRol.deletePermisoRol(id);
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
                            FrmPermisoRol.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPermisoRol.Visible = true;
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
                            FrmPermisoRol.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPermisoRol.Visible = false;
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
                            FrmPermisoRol.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPermisoRol.Visible = false;
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
        private void showPermisoDDL()
        {
            DDLPermisos.DataSource = objPer.showPermisoDDL();
            DDLPermisos.DataValueField = "per_id";
            DDLPermisos.DataTextField = "per_nombre";
            DDLPermisos.DataBind();
            DDLPermisos.Items.Insert(0, "Seleccione el Rol");
        }


        private void showRolDDL()
        {
            DDLRoles.DataSource = objRol.showrolDDL();
            DDLRoles.DataValueField = "rol_id";
            DDLRoles.DataTextField = "rol_nombre";
            DDLRoles.DataBind();
            DDLRoles.Items.Insert(0, "Seleccione el Rol");
        }

        private void clear()
        {
            HFPermisoRolID.Value = "";
            DDLPermisos.SelectedIndex = 0;
            DDLRoles.SelectedIndex = 0;
            TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            _fkPermiso = Convert.ToInt32(DDLPermisos.SelectedValue);
            _fkRol = Convert.ToInt32(DDLRoles.SelectedValue);
            _date = DateTime.Parse(TBFecha.Text);

            executed = objPerRol.savePermisoRol(_fkPermiso, _fkRol,_date);

            if (executed)
            {
                LblMsg.Text = "El permiso rol se guardo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFPermisoRolID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un permiso rol para actualizar";
                return;
            }

            _id = Convert.ToInt32(HFPermisoRolID.Value);
            _fkPermiso = Convert.ToInt32(DDLPermisos.SelectedValue);
            _fkRol = Convert.ToInt32(DDLRoles.SelectedValue);
            _date = DateTime.Parse(TBFecha.Text);

            executed = objPerRol.updatePermisoRol(_id, _fkPermiso, _fkRol, _date);

            if (executed)
            {
                LblMsg.Text = "El permiso rol se actualizo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizo";
            }
        }
    }
}