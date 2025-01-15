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
    public partial class WFGestion : System.Web.UI.Page
    {
        GestionLog objGes = new GestionLog();
        EmpleadoLog objEmp = new EmpleadoLog();
        ProductoLog objPro = new ProductoLog();

        private DateTime p_ges_fecha;
        private string p_ges_descripcion;
        private int _fkemployee, p_ges_id;
        private int _fkproduct;
        private bool executed = false;

        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmGestion.Visible = false;
                PanelAdmin.Visible = false;
                //showManagement();
                showEmployeesDDL();
                showProductsDDL();
            }
            validatePermisoRol();
        }

        [WebMethod]
        public static object ListManagement()
        {
            GestionLog objGes = new GestionLog();
            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objGes.showManagement();
            // Se crea una lista para almacenar los productos que se van a devolver.
            var managementList = new List<object>();
            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                managementList.Add(new
                {
                    ManagementID = row["ges_id"],
                    Description = row["ges_descripcion"],
                    Date = Convert.ToDateTime(row["ges_fecha"]).ToString("yyyy-MM-dd"),
                    Employee = row["tbl_empleado_emp_id"],
                    Product = row["tbl_producto_pro_id"],
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = managementList };
        }

        [WebMethod]
        public static bool DeleteManagement(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            GestionLog objGes = new GestionLog();
            // Invocar al método para eliminar el producto y devolver el resultado
            return objGes.deleteManagement(id);
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
                            FrmGestion.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmGestion.Visible = true;
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
                            FrmGestion.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmGestion.Visible = false;
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
                            FrmGestion.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmGestion.Visible = false;
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
            HFManagementID.Value = "";
            TBDescription.Text = "";
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DDLEmployee.SelectedIndex = 0;
            DDLProduct.SelectedIndex = 0;
        }

        private void showEmployeesDDL()
        {
            DDLEmployee.DataSource = objEmp.showEmployeesDDL();
            DDLEmployee.DataValueField = "emp_id"; //Nombre de la llave primaria
            DDLEmployee.DataBind();
            DDLEmployee.Items.Insert(0, "Seleccione");
        }
        private void showProductsDDL()
        {
            DDLProduct.DataSource = objPro.showProductoDDL();
            DDLProduct.DataValueField = "pro_id"; //Nombre de la llave primaria
            DDLProduct.DataBind();
            DDLProduct.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            p_ges_fecha = Convert.ToDateTime(TBDate.Text);
            p_ges_descripcion = TBDescription.Text;
            _fkemployee = Convert.ToInt32(DDLEmployee.SelectedValue);
            _fkproduct = Convert.ToInt32(DDLProduct.SelectedValue);

            executed = objGes.saveManagement(p_ges_descripcion, p_ges_fecha, _fkemployee, _fkproduct);

            if (executed)
            {
                LblMsg.Text = "Se guardó la gestión exitosamente!";
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            {
                // Verifica si se ha seleccionado un producto para actualizar
                if (string.IsNullOrEmpty(HFManagementID.Value))
                {
                    LblMsg.Text = "No se ha seleccionado una gestión para actualizar.";
                    return;
                }
                p_ges_id = Convert.ToInt32(HFManagementID.Value);
                p_ges_descripcion = TBDescription.Text;
                p_ges_fecha = DateTime.Parse(TBDate.Text);
                _fkemployee = Convert.ToInt32(DDLEmployee.SelectedValue);
                _fkproduct = Convert.ToInt32(DDLProduct.SelectedValue);
                executed = objGes.updateManagement(p_ges_id,p_ges_fecha,p_ges_descripcion);
                if (executed)
                {
                    LblMsg.Text = "La gestión se actualizó exitosamente!";
                    clear(); //Se invoca el metodo para limpiar los campos 
                }
                else
                {
                    LblMsg.Text = "Error al actualizar";
                }
            }
        }
    }
}