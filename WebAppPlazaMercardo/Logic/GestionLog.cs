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
    public class GestionLog
    {
        GestionDat objCat = new GestionDat();
        //Metodo para mostrar todas las gestiones
        public DataSet showManagement()
        {
            return objCat.showManagement();
        }
        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showManagementDDL()
        {
            return objCat.showManagementDDL();
        }
        //Metodo para guardar una nueva gestion
        public bool saveManagement(DateTime p_ges_fecha, string p_ges_descripcion, int _fkemployee, int _fkproduct)
        {
            return objCat.saveManagement(p_ges_fecha, p_ges_descripcion,  _fkemployee, _fkproduct);
        }
        //Metodo para actualizar una gestion
        public bool updateManagement(int p_ges_id, DateTime p_ges_fecha, string p_ges_descripcion, int _fkemployee, int _fkproduct)
        {
            return objCat.updateManagement(p_ges_id, p_ges_fecha, p_ges_descripcion,_fkemployee,_fkproduct);
        }
        //Metodo para borrar una gestion
        public bool deleteManagement(int p_ges_id)
        {
            return objCat.deleteManagement(p_ges_id);
        }
    }
}