using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class PermisoRolLog
    {
        PermisoRolDat objPerRol = new PermisoRolDat();

        public DataSet showPermisoRol()
        {
            return objPerRol.showPermisoRol();
        }

        public bool savePermisoRol(int _fkPermiso, int _fkRol,DateTime _date)
        {
            return objPerRol.savePermisoRol(_fkPermiso, _fkRol,  _date);
        }
        
        public bool updatePermisoRol(int _id, int _fkPermiso, int _fkRol, DateTime _date)
        {
            return objPerRol.updatePermisoRol(_id, _fkPermiso, _fkRol,_date);
        }

        public bool deletePermisoRol(int _idPermisoRol)
        {
            return objPerRol.deletePermisoRol(_idPermisoRol);
        }
    }
}