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
    public class PermisoLog
    {
        PermisoDat objPer = new PermisoDat();

        public DataSet showPermisos()
        {
            return objPer.showPermisos();
        }
        public DataSet showPermisoDDL()
        {
            return objPer.showPermisoDDL();
        }
        public bool savePermiso(string _name, string _description)
        {
            return objPer.savePermiso(_name, _description);
        }
        public bool updatePermiso(int _id, string _name, string _description)
        {
            return objPer.updatePermiso(_id, _name, _description);
        }
        public bool deletePermiso(int _idPermiso)
        {
            return objPer.deletePermiso(_idPermiso);
        }
    }
}