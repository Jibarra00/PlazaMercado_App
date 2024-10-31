using System;
using Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;

namespace Logic
{
    public class CategoriaLog
    {
        CategoriaDat objCat = new CategoriaDat();

        //Metodo para mostrar todas las Categorias
        public DataSet showCategories()
        {
            return objCat.showCategories();
        }

        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showCategoriesDDL()
        {
            return objCat.showCategoriesDDL();
        }
        //Metodo para guardar una nueva Categoria
        public bool saveCategory(string _description)
        {
            return objCat.saveCategory(_description);
        }
        //Metodo para actualizar una Categoria
        public bool updateCategory(int _idCategory, string _description)
        {
            return objCat.updateCategory(_idCategory, _description);
        }
        //Metodo para borrar una Categoria
        public bool deleteCategory(int _idCategory)
        {
            return objCat.deleteCategory(_idCategory);
        }
    }
}