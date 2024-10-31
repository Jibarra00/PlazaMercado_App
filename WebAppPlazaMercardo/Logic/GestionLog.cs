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
        public bool saveManagement(string p_ges_descripcion, DateTime p_ges_fecha)
        {
            return objCat.saveManagement(p_ges_descripcion, p_ges_fecha);
        }
        //Metodo para actualizar una gestion
        public bool updateManagement(int p_ges_id, DateTime p_ges_fecha, string p_ges_descripcion)
        {
            return objCat.updateManagement(p_ges_id, p_ges_fecha, p_ges_descripcion);
        }
        //Metodo para borrar una gestion
        public bool deleteManagement(int p_ges_id)
        {
            return objCat.deleteManagement(p_ges_id);
        }
    }
}