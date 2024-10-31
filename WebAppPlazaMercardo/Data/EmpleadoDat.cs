using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class EmpleadoDat
    {
        Persistence objPer = new Persistence();

        //Metodo para mostrar todos los Empleados
        public DataSet showEmployees()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectEmployees";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        //Metodo para mostrar unicamente el id y la descripcion de los Empleados
        public DataSet showEmployeesDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectEmployeesDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        //Metodo para guardar un nuevo Empleado
        public bool saveEmployee(string e_identification,string e_names,
            string e_lastnames,string e_phone,string e_addres)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertEmployee"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("emp_identificacion", MySqlDbType.VarString).Value = e_identification;
            objSelectCmd.Parameters.Add("emp_nombres", MySqlDbType.VarString).Value = e_names;
            objSelectCmd.Parameters.Add("emp_apellidos", MySqlDbType.VarString).Value = e_lastnames;
            objSelectCmd.Parameters.Add("emp_telefono", MySqlDbType.VarString).Value = e_phone;
            objSelectCmd.Parameters.Add("emp_direccion", MySqlDbType.VarString).Value = e_addres;


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

        //Metodo para actualizar un Empleado
        public bool updateEmployee(int e_id, string e_identification, string e_names,
            string e_lastnames, string e_phone, string e_addres)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateEmployee"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("emp_id", MySqlDbType.Int32).Value = e_id;
            objSelectCmd.Parameters.Add("emp_identificacion", MySqlDbType.VarString).Value = e_identification;
            objSelectCmd.Parameters.Add("emp_nombres", MySqlDbType.VarString).Value = e_names;
            objSelectCmd.Parameters.Add("emp_apellidos", MySqlDbType.VarString).Value = e_lastnames;
            objSelectCmd.Parameters.Add("emp_telefono", MySqlDbType.VarString).Value = e_phone;
            objSelectCmd.Parameters.Add("emp_direccion", MySqlDbType.VarString).Value = e_addres;

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

        //Metodo para borrar una Empleado
        public bool deleteEmployee(int e_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteClient"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("emp_id", MySqlDbType.Int32).Value = e_id;

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