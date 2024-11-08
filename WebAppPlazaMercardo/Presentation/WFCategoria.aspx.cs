using Logic;
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
                //showCategories();
                //showCategoriesDDL();
            }
        }

        [WebMethod]
        public static object ListCategories()
        {
            CategoriaLog objCat = new CategoriaLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objCat.showCategories();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var categoriesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                categoriesList.Add(new
                {
                    CategoryID = row["cat_id"],
                    Description = row["cat_descripcion"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = categoriesList };
        }

        [WebMethod]
        public static bool DeleteCategory(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            CategoriaLog objCat = new CategoriaLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objCat.deleteCategory(id);
        }

        private void clear()
        {
            HFCategoryID.Value = "";
            TBDescription.Text = "";
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _description = TBDescription.Text;
            executed = objCat.saveCategory(_description);

            if (executed)
            {
                LblMsg.Text = "La descripción de la categoría ha sido guardada exitosamente!";
               
            }
            else
            {
                LblMsg.Text = "Error al guardar :(!";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFCategoryID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }
            _idCategory = Convert.ToInt32(HFCategoryID.Value);
            _description = TBDescription.Text;

            executed = objCat.updateCategory(_idCategory, _description);

            if (executed)
            {
                LblMsg.Text = "El producto se actualizo exitosamente!";
                
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}