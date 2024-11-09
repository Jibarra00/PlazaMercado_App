using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace Data
{
    public class UsuarioDat
    {
        // Se crea una instancia de la clase Persistence para manejar la conexión a la base de datos.
        Persistence objPer = new Persistence();
        public DataSet showUsuario()
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
            objSelectCmd.CommandText = "spSelectUsuarios";

            // Se indica que se trata de un procedimiento almacenado.
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se establece el comando de selección del adaptador de datos.
            objAdapter.SelectCommand = objSelectCmd;

            // Se llena el DataSet con los resultados de la consulta.
            objAdapter.Fill(objData);

            // Se cierra la conexión después de obtener los datos.
            objPer.closeConnection();

            // Se devuelve el DataSet que contiene de los usuarios.
            return objData;
        }
        //Metodo para guardar un nuevo Usuario
        public bool saveUsuario(string _email,string _password, string _salt, string _state, DateTime _Create_Date, int _fkrol, int _fkempleado, int _fkcliente)
        {
            // Se inicializa una variable para indicar si la operación se ejecutó correctamente.
            bool executed = false;
            int row;// Variable para almacenar el número de filas afectadas por la operación.

            // Se crea un comando MySQL para insertar un nuevo usuario utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertUsuario"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del usuario.
            objSelectCmd.Parameters.Add("u_email", MySqlDbType.VarString).Value = _email;
            objSelectCmd.Parameters.Add("u_password", MySqlDbType.VarString).Value = _password;
            objSelectCmd.Parameters.Add("u_salt", MySqlDbType.VarString).Value = _salt;
            objSelectCmd.Parameters.Add("u_state", MySqlDbType.VarString).Value = _state;
            objSelectCmd.Parameters.Add("u_create_date", MySqlDbType.Datetime).Value = _Create_Date;
            objSelectCmd.Parameters.Add("fkrol", MySqlDbType.Int32).Value = _fkrol;
            objSelectCmd.Parameters.Add("fkempleado", MySqlDbType.Int32).Value = _fkempleado;
            objSelectCmd.Parameters.Add("fkcliente", MySqlDbType.Int32).Value = _fkcliente;


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
        //Metodo para actulizar un usuario
        public bool updateUsuario(int _id, string _email, string _password, string _salt, string _state, DateTime _Create_Date, int _fkrol, int _fkempleado, int _fkcliente)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateUsuario"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del usuario.
            objSelectCmd.Parameters.Add("u_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("u_email", MySqlDbType.VarString).Value = _email;
            objSelectCmd.Parameters.Add("u_password", MySqlDbType.VarString).Value = _password;
            objSelectCmd.Parameters.Add("u_salt", MySqlDbType.VarString).Value = _salt;
            objSelectCmd.Parameters.Add("u_state", MySqlDbType.VarString).Value = _state;
            objSelectCmd.Parameters.Add("u_create_date", MySqlDbType.DateTime).Value = _Create_Date;
            objSelectCmd.Parameters.Add("fkrol", MySqlDbType.Int32).Value = _fkrol;
            objSelectCmd.Parameters.Add("fkempleado", MySqlDbType.Int32).Value = _fkempleado;
            objSelectCmd.Parameters.Add("fkcliente", MySqlDbType.Int32).Value = _fkcliente;

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
        //Metodo para borrar un Usuario
        public bool deleteUsuario(int _idUsuario)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteUsuario"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("u_id", MySqlDbType.Int32).Value = _idUsuario;

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