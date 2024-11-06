using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
                
                showProductos();
                showCategoriaDDL();
                showProvedoresDDL();
            }
        }
        private void showProductos()
        {
            DataSet ds = new DataSet();
            ds = objProd.showProducts();
            GVProductos.DataSource = ds;
            GVProductos.DataBind();
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
                showProductos();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
    }
}