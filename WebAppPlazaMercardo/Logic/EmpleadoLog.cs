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
    public class EmpleadoLog
    {
        EmpleadoDat objEmp = new EmpleadoDat();

        //Metodo para mostrar todos los Empleados
        public DataSet showEmployees()
        {
            return objEmp.showEmployees();
        }

        //Metodo para mostrar unicamente el id 
        public DataSet showEmployeesDDL()
        {
            return objEmp.showEmployeesDDL();
        }

        //Metodo para guardar un nuevo Empleado
        public bool saveEmployee(string e_identification, string e_names,
            string e_lastnames, string e_phone, string e_addres)
        {
            return objEmp.saveEmployee( e_identification, e_names,
            e_lastnames, e_phone, e_addres);
        }

        //Metodo para actualizar un Empleado
        public bool updateEmployee(int e_id, string e_identification, string e_names,
            string e_lastnames, string e_phone, string e_addres)
        {
            return objEmp.updateEmployee(e_id, e_identification, e_names,
            e_lastnames, e_phone, e_addres);
        }
        //Metodo para borrar un cliente
        public bool deleteClient(int e_id)
        {
            return objEmp.deleteEmployee(e_id);
        }

    }
}
  