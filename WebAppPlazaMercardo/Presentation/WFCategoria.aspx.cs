using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFCategoria : System.Web.UI.Page
    {
        CategoriaLog objCat = new CategoriaLog();
        private string _description;
        private int _idCategory;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showCategories();
                showCategoriesDDL();
            }
        }

        protected void showCategories()
        {
            DataSet ds = new DataSet();
            ds = objCat.showCategories();
            GVCategories.DataSource = ds;
            GVCategories.DataBind();
        }
        private void showCategoriesDDL()
        {
            DDLCategory.DataSource = objCat.showCategoriesDDL();
            DDLCategory.DataValueField = "cat_id"; //Nombre de la llave primaria
            DDLCategory.DataTextField = "cat_descripcion";
            DDLCategory.DataBind();
            DDLCategory.Items.Insert(0, "Seleccione");
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _description = TBDescription.Text;
            executed = objCat.saveCategory(_description);
            if (executed)
            {
                LblMsg.Text = "La descripción de la categoría ha sido guardada exitosamente!";
                showCategories();
            }
            else
            {
                LblMsg.Text = "Error al guardar :(!";
            }
        }
    }
}