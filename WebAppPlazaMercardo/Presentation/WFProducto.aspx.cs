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
    public partial class WFProducto : System.Web.UI.Page
    {
        //Crea los objetos
        ProductoLog objProd = new ProductoLog();
        ProveedorLog objPro = new ProveedorLog();
        CategoriaLog objCat = new CategoriaLog();

        private int _id, _quantity, _fkCategory, _fkProvider;
        private string _code, _description;
        private double _price;
        private bool executed = false;
        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BTnSave.Visible = false;
                BTnUpdate.Visible = false;
                PanelAdmin.Visible = false;
                FrmProducto.Visible = false;
                //showProductos();
                showCategoriaDDL();
                showProvedoresDDL();
            }
            validatePermisoRol();
        }
        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        //lista de productos
        [WebMethod]
        public static object ListProducts()
        {
            ProductoLog objProd = new ProductoLog();

            var dataSet = objProd.showProducts();

            var productsList = new List<object>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                productsList.Add(new
                {
                    ProductID = row["pro_id"],
                    Code = row["pro_codigo"],
                    Description = row["pro_descripcion"],
                    Quantity = row["pro_cantidad"],
                    Price = row["pro_precio"],
                    FkCategory = row["tbl_categoria_cat_id"],
                    NameCategory = row["cat_descripcion"],
                    FkProvider = row["tbl_proveedor_prov_id"],
                    NameProvider = row["prov_nombre"]

                });
            }
            return new { data = productsList };
            //eliminar producto
        }

        

        [WebMethod]
        public static bool DeleteProduct(int id)
        {
            ProductoLog objProd = new ProductoLog();
            return objProd.deleteProducto(id);
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
                            FrmProducto.Visible = true;// Se pone visible el formulario
                            BTnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmProducto.Visible = true;
                            BTnUpdate.Visible = true;// Se pone visible el boton actualizar
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
                            FrmProducto.Visible = true;// Se pone visible el formulario
                            BTnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmProducto.Visible = true;
                            BTnUpdate.Visible = true;// Se pone visible el boton actualizar
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
                            FrmProducto.Visible = false;// Se pone visible el formulario
                            BTnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmProducto.Visible = false;
                            BTnUpdate.Visible = false;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = false;// Se pone visible el panel
                            _showEditButton = false;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
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

        private void showProvedoresDDL()
        {
            DDLProveedor.DataSource = objPro.showProveedorDDL();
            DDLProveedor.DataValueField = "prov_id";
            DDLProveedor.DataTextField = "prov_nombre";
            DDLProveedor.DataBind();
            DDLProveedor.Items.Insert(0, "Seleccione el proveedor");
        }
        private void showCategoriaDDL()
        {
            DDLCategoria.DataSource = objCat.showCategoriesDDL();
            DDLCategoria.DataValueField = "cat_id";
            DDLCategoria.DataTextField = "cat_descripcion";
            DDLCategoria.DataBind();
            DDLCategoria.Items.Insert(0, "Seleccione la categoria");
        }

        //Metodo para limpiar los texbox  y los DDL
        private void clear()
        {
            HFProductoID.Value = "";
            TBCodigo.Text = "";
            TBDescripcion.Text = "";
            TBCantidad.Text = "";
            TBPrecio.Text = "";
            DDLCategoria.SelectedIndex = 0;
            DDLProveedor.SelectedIndex = 0;
        }

        protected void BTnSave_Click(object sender, EventArgs e)
        {
            _code = TBCodigo.Text;
            _description = TBDescripcion.Text;
            _quantity = Convert.ToInt32(TBCantidad.Text);
            _price = Convert.ToDouble(TBPrecio.Text);
            _fkCategory = Convert.ToInt32(DDLCategoria.Text);
            _fkProvider = Convert.ToInt32(DDLProveedor.Text);


            executed = objProd.saveProducts(_code, _description, _quantity, _price, _fkCategory, _fkProvider);

            if (executed)
            {
                LblMsg.Text = "Se guardo el producto";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
        protected void BTnUpdate_Click(object sender, EventArgs e)
        {
            //verifica si se ha seleccionado un prodcuto para actualizar
            if (string.IsNullOrEmpty(HFProductoID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un producto para actualizar";
                return;
            }

            _id = Convert.ToInt32(HFProductoID.Value);
            _code = TBCodigo.Text;
            _description = TBDescripcion.Text;
            _quantity = Convert.ToInt32(TBCantidad.Text);
            _price = Convert.ToDouble(TBPrecio.Text);
            _fkCategory = Convert.ToInt32(DDLCategoria.Text);
            _fkProvider = Convert.ToInt32(DDLProveedor.Text);

            executed = objProd.updateProducts(_id, _code, _description, _quantity, _price, _fkCategory, _fkProvider);

            if (executed)
            {
                LblMsg.Text = "Se actualizo el producto";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }


    }
}