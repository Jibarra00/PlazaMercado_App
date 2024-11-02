using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFEmpleado : System.Web.UI.Page
    {
        //Se crea el Objeto
        EmpleadoLog objEmp = new EmpleadoLog();

        //Se definen los atributos
        private string e_identification, e_names, e_lastnames, e_phone, e_addres;

        

        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showEmployees();
            }
        }
        private void showEmployees()
        {
            DataSet ds = new DataSet();
            ds = objEmp.showEmployees();
            GVClients.DataSource = ds;
            GVClients.DataBind();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            {
                e_identification = TBId.Text;
                e_names = TBName.Text;
                e_lastnames = TBLastname.Text;
                e_phone = TBPhone.Text;
                e_addres = TBAddres.Text;

                executed = objEmp.saveEmployee(e_identification, e_names, e_lastnames, e_phone, e_addres);
                if (executed)
                {
                    LblMsg.Text = "El Empleado se guardo exitosamente";
                }
                else
                {
                    LblMsg.Text = "Error al Guardar";
                }


            }


        }   
    }
}
