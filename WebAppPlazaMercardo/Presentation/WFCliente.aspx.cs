using Logic;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFCliente : System.Web.UI.Page
    {
        //Se crea el Objeto
        ClienteLog objCli = new ClienteLog();
        //Se definen los atributos
        private string _names, _lastnames, _mail, _phone, _addres;

        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showClients();
            }
        }
        private void showClients()
        {
            DataSet ds = new DataSet();
            ds = objCli.showClient();
            GVClients.DataSource = ds;
            GVClients.DataBind();

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _names = TBName.Text;
            _lastnames = TBlastname.Text;
            _mail = TBMail.Text;
            _phone = TBPhone.Text;
            _addres = TBAddres.Text;

            executed = objCli.saveClient(_names, _lastnames, _mail, _phone, _addres);
            if (executed)
            {
                LblMsg.Text = "El CLiente se guardo exitosamente";
            }
            else
            {
                LblMsg.Text = "Error al Guardar";
            }

        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}