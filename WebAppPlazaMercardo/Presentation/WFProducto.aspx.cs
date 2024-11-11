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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //showProductos();
                showCategoriaDDL();
                showProvedoresDDL();
            }
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