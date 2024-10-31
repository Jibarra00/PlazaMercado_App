using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Data;

namespace Logic
{
    public class ProveedorLog
    {
        ProveedorDat objprov = new ProveedorDat();
        
        
        public DataSet showProveedorDDL()
        {
            
            return objprov.showProvidersDDL();
        }
        //metodo para mostrar de los provedores
        public DataSet showProveedor()
        {
            
            return objprov.showProveedor();
        }
        //metodo para guardar proveedor
        public bool saveProveedor(string _nit, string _nombre)
        {
            return objprov.saveProveedor(_nit, _nombre);
        }
        //metodo para actualizar provedor

        public bool updateProveedor(int _id, string _nit, string _nombre)
        {
            return objprov.updateProveedor(_id, _nit, _nombre);
        }

        //metodo para eliminar provedor
        public bool deleteProveedor(int _idProveedor)
        {
            return objprov.deleteProveedor(_idProveedor);

        }


        }
    }