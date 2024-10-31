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
    public class RolLog
    {
        RolDat objrol = new RolDat();

        public DataSet showrolDDL()
        {

            return objrol.showrolDDL();
        }

        //metodo para mostrar roles
        public DataSet showRol()
        {
            return objrol.showRol();

        }
        //metodo para guardar rol
        public bool saveRol(string _nombre, string _description)
        {
            return objrol.saveRol(_nombre, _description);
        }
        //metodo para ctualizar rol
        public bool updateRol(int _id, string _nombre, string _description)
        {
            return objrol.updateRol(_id, _nombre, _description);
        }
        //metodo para eliminar rol
        public bool deleteRol(int _idRol)
        {
            return objrol.deleteRol(_idRol);
        }
    }
}