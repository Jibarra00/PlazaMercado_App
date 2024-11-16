using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFProveedor : System.Web.UI.Page
    {
        //crear los objetos
        ProveedorLog objprov = new ProveedorLog();

        private int _id;
        private string _nit, _nombre;
        private bool executed = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                //Aqui se invocan todos los metodos
                //showProveedor();
            }
        }
        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListProveedor()
        {
            ProveedorLog objProv = new ProveedorLog();

            var dataSet = objProv.showProveedor();

            var proveedorList = new List<Object>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                proveedorList.Add(new
                {
                    ProveedorID = row["prov_id"],
                    Nit = row["prov_nit"],
                    Nombre = row["prov_nombre"]

                });
            }
                return new {data = proveedorList};


            }
            [WebMethod]
            public static bool DeleteProveedor(int id)
            {
                // Crear una instancia de la clase de lógica de productos
                ProveedorLog objProd = new ProveedorLog();

                // Invocar al método para eliminar el producto y devolver el resultado
                return objProd.deleteProveedor(id);
            }
        private void clear() 
        {
            HFProveedorID.Value = "";
            TBNit.Text = "";
            TBNombre.Text = "";
           
        }

        

        //eventos que se ejecutan cuando se d aclic para los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nit = TBNit.Text;
            _nombre = TBNombre.Text;

            executed = objprov.saveProveedor(_nit, _nombre);

            if (executed)
            {
                LblMsg.Text = "El proveedor se guardo exitosamente";
                clear();

            }
            else 
            {
                LblMsg.Text = "Error al guardar";
            
            }


        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFProveedorID.Value)) 
            {
                LblMsg.Text = "No se ha seleccionado un proveedor para actualizar";
                return;
            
            }
            _id = Convert.ToInt32(HFProveedorID.Value);
            _nit = TBNit.Text;
            _nombre = TBNombre.Text;

            executed = objprov.updateProveedor(_id, _nit, _nombre);

            if (executed)
            {
                LblMsg.Text = "Se actualizo el Proveedor";
                clear() ;

            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }

        }




    }
}