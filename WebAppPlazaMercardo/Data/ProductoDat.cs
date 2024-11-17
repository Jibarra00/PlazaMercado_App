using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{

    public class ProductoDat
    {
        // Se crea una instancia de la clase Persistence para manejar la conexión a la base de datos.
        Persistence objPer = new Persistence();

        //Metodo para mostrar unicamente el id y la descripcion de los Provedores, en el DropDownList
        public DataSet showProductoDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectProductDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para mostrar los productos desde la base de datos.
        public DataSet showProducts()
        {
            // Se crea un adaptador de datos para MySQL.
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();

            // Se crea un DataSet para almacenar los resultados de la consulta.
            DataSet objData = new DataSet();

            // Se crea un comando MySQL para seleccionar los productos utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();

            // Se establece la conexión del comando utilizando el método openConnection() de Persistence.
            objSelectCmd.Connection = objPer.openConnection();

            // Se especifica el nombre del procedimiento almacenado a ejecutar.
            objSelectCmd.CommandText = "spSelectProducts";

            // Se indica que se trata de un procedimiento almacenado.
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se establece el comando de selección del adaptador de datos.
            objAdapter.SelectCommand = objSelectCmd;

            // Se llena el DataSet con los resultados de la consulta.
            objAdapter.Fill(objData);

            // Se cierra la conexión después de obtener los datos.
            objPer.closeConnection();

            // Se devuelve el DataSet que contiene los productos.
            return objData;
        }

        //Metodo para guardar un nuevo Producto
        public bool saveProducts(string _code, string _description, int _quantity, double _price, int _fkCategory, int _fkProvider)
        {
            // Se inicializa una variable para indicar si la operación se ejecutó correctamente.
            bool executed = false;
            int row;// Variable para almacenar el número de filas afectadas por la operación.

            // Se crea un comando MySQL para insertar un nuevo producto utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertProduct"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del producto.
            objSelectCmd.Parameters.Add("p_codigo", MySqlDbType.VarString).Value = _code;
            objSelectCmd.Parameters.Add("p_descripcion", MySqlDbType.VarString).Value = _description;
            objSelectCmd.Parameters.Add("p_cantidad", MySqlDbType.Int32).Value = _quantity;
            objSelectCmd.Parameters.Add("p_precio", MySqlDbType.Double).Value = _price;
            objSelectCmd.Parameters.Add("p_fkcategory", MySqlDbType.Int32).Value = _fkCategory;
            objSelectCmd.Parameters.Add("p_fkprovider", MySqlDbType.Int32).Value = _fkProvider;

            try
            {
                // Se ejecuta el comando y se obtiene el número de filas afectadas.
                row = objSelectCmd.ExecuteNonQuery();

                // Si se inserta una fila correctamente, se establece executed a true.
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                // Si ocurre un error durante la ejecución del comando, se muestra en la consola.
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            // Se devuelve el valor de executed para indicar si la operación se ejecutó correctamente.
            return executed;
        }

        //Metodo para actulizar un producto
        public bool updateProducts(int _id, string _code, string _description, int _quantity, double _price, int _fkCategory, int _fkProvider)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateProduct"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del producto.
            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_codigo", MySqlDbType.VarString).Value = _code;
            objSelectCmd.Parameters.Add("p_descripcion", MySqlDbType.VarString).Value = _description;
            objSelectCmd.Parameters.Add("p_cantidad", MySqlDbType.Int32).Value = _quantity;
            objSelectCmd.Parameters.Add("p_precio", MySqlDbType.Double).Value = _price;
            objSelectCmd.Parameters.Add("p_fkcategory", MySqlDbType.Int32).Value = _fkCategory;
            objSelectCmd.Parameters.Add("p_fkprovider", MySqlDbType.Int32).Value = _fkProvider;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }
        //Metodo para borrar un producto
        public bool deleteProducto(int _idPro)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteProduct"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _idPro;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;

        }

    }

}
    


