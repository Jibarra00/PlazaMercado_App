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
    public partial class WFGestion : System.Web.UI.Page
    {
        GestionLog objGes = new GestionLog();
        EmpleadoLog objEmp = new EmpleadoLog();
        ProductoLog objPro = new ProductoLog();

        private DateTime p_ges_fecha;
        private string p_ges_descripcion;
        private int _fkemployee;
        private int _fkproduct;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showManagement();
                showEmployeesDDL();
                showProductsDDL();
            }
        }
        private void showManagement()
        {
            DataSet ds = new DataSet();
            ds = objGes.showManagement();
            GVManagement.DataSource = ds;
            GVManagement.DataBind();
        }

        private void showEmployeesDDL()
        {
            DDLEmployee.DataSource = objEmp.showEmployeesDDL();
            DDLEmployee.DataValueField = "emp_id"; //Nombre de la llave primaria
            DDLEmployee.DataBind();
            DDLEmployee.Items.Insert(0, "Seleccione");
        }
        private void showProductsDDL()
        {
            DDLProduct.DataSource = objPro.showProductoDDL();
            DDLProduct.DataValueField = "pro_id"; //Nombre de la llave primaria
            DDLProduct.DataBind();
            DDLProduct.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            p_ges_fecha = Convert.ToDateTime(TBDate.Text);
            p_ges_descripcion = TBDescription.Text;
            _fkemployee = Convert.ToInt32(DDLEmployee.SelectedValue);
            _fkproduct = Convert.ToInt32(DDLProduct.SelectedValue);

            executed = objGes.saveManagement(p_ges_descripcion, p_ges_fecha, _fkemployee, _fkproduct);

            if (executed)
            {
                LblMsg.Text = "Se guardó la gestión exitosamente!";
                showManagement();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
    }
}