using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class ClienteDat
    {
        Persistence objPer = new Persistence();

        //Metodo para mostrar todos los clientes
        public DataSet showClients()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectClients";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para mostrar unicamente el id y la descripcion de los Clientes, en el DropDownList
        public DataSet showClientsDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectClientsDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }


        public bool saveClient(string c_names, string c_lastnames, string c_mail, string c_phone, string c_addres)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertClient"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("c_names", MySqlDbType.VarString).Value = c_names;
            objSelectCmd.Parameters.Add("c_lastnames", MySqlDbType.VarString).Value = c_lastnames;
            objSelectCmd.Parameters.Add("c_mail", MySqlDbType.VarString).Value = c_mail;
            objSelectCmd.Parameters.Add("c_phone", MySqlDbType.VarString).Value = c_phone;
            objSelectCmd.Parameters.Add("c_addres", MySqlDbType.VarString).Value = c_addres;

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


        //Metodo para actualizar un cliente
        public bool updateClient(int c_id, string c_names, string c_lastnames,
            string c_mail, string c_phone, string c_addres)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateCategory"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("cli_id", MySqlDbType.Int32).Value = c_id;
            objSelectCmd.Parameters.Add("cli_nombres", MySqlDbType.VarString).Value = c_names;
            objSelectCmd.Parameters.Add("cli_apellidos", MySqlDbType.VarString).Value = c_lastnames;
            objSelectCmd.Parameters.Add("cli_correo", MySqlDbType.VarString).Value = c_mail;
            objSelectCmd.Parameters.Add("cli_telefono", MySqlDbType.VarString).Value = c_phone;
            objSelectCmd.Parameters.Add("cli_direccion", MySqlDbType.VarString).Value = c_addres;

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

        //Metodo para borrar una Categoria
        public bool deleteClient(int c_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteClient"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("cli_id", MySqlDbType.Int32).Value = c_id;

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
