using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;

namespace Logic
{
    public class ProductoLog
    {
        ProductoDat objpro = new ProductoDat();

        //Metodo para mostrar unicamente el id y la descripcion de los Productos
        public DataSet showProductoDDL()
        {

            return objpro.showProductoDDL();
        }
        // metodo para mostrar los productos
        public DataSet showProducts()
        {

            return objpro.showProducts();
        }
        //metodo para guardar los productos
        public bool saveProducts(string _code, string _description, int _quantity, double _price, int _fkCategory, int _fkProvider)
        {
            return objpro.saveProducts(_code, _description, _quantity, _price, _fkCategory, _fkProvider);
        }
        //metodo para actualizar productos
        public bool updateProducts(int _id, string _code, string _description, int _quantity, double _price, int _fkCategory, int _fkProvider)
        {
            return objpro.updateProducts(_id, _code, _description, _quantity, _price, _fkCategory, _fkProvider);
        }
        //metodo para eliminar producto

        public bool deleteProducto(int _idPro)
        {
            return objpro.deleteProducto(_idPro);
        }
    }
}
