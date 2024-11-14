using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace Presentation
{
    public partial class WFPermiso : System.Web.UI.Page
    {
        PermisoLog objPer = new PermisoLog();

        private int _id;
        private string _name, _description;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //showPermisos();
            }
        }
        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListPermiso()
        {
            PermisoLog objPer = new PermisoLog();

            var dataSet = objPer.showPermisos();

            var permisoList = new List<object>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                permisoList.Add(new
                {
                    PermisoID = row["per_id"],
                    Nombre = row["per_nombre"],
                    Descripcion = row["per_descripcion"]
                });
            }
            return new {data = permisoList};
        }

        [WebMethod]
        public static bool DeletePermiso(int id)
        {
            PermisoLog objPer = new PermisoLog();

            return objPer.deletePermiso(id);
        }

        private void clear()
        {
            HFPermisoID.Value = "";
            TBNombre.Text = "";
            TBDescripcion.Text = "";
        }

        

        protected void BTSave_Click(object sender, EventArgs e)
        {
            _name = TBNombre.Text;
            _description = TBDescripcion.Text;

            executed = objPer.savePermiso(_name, _description);
            if (executed)
            {
                LblMsj.Text = "Se guardo correctamente";
                clear();
            }
            else
            {
                LblMsj.Text = "Error al guardar ";
            }
        }

        protected void BTUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFPermisoID.Value))
            {
                LblMsj.Text = "No se ha seleccionado ningun permiso para actualizar.";
                return;
            }
            
            _id = Convert.ToInt32(HFPermisoID.Value);
            _name = TBNombre.Text;
            _description = TBDescripcion.Text;

            executed = objPer.updatePermiso(_id, _name, _description);

            if (executed)
            {
                LblMsj.Text = "Se actualizo correctamente";
                clear();
            }
            else
            {
                LblMsj.Text = "Error al actualizar ";
            }
        }


    }
}