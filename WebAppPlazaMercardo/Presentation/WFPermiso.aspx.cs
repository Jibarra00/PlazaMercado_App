using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPermiso : System.Web.UI.Page
    {
        PermisoLog objPer = new PermisoLog();

        private int id;
        private string _name, _description;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showPermisos();
            }
        }
        private void showPermisos()
        {
            DataSet ds = new DataSet();
            ds = objPer.showPermisos();
            GVPermisos.DataSource = ds;
            GVPermisos.DataBind();
            
        }

        protected void BTSave_Click(object sender, EventArgs e)
        {
            _name = TBNombre.Text;
            _description = TBNombre.Text;

            executed = objPer.savePermiso(_name, _description);
            if (executed)
            {
                LblMsj.Text = "Se guardo correctamente";
                showPermisos();
            }
            else
            {
                LblMsj.Text = "Error al guardar ";
            }
        }
    }
}