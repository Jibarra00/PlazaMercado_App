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
                showProveedor();
            }
        }
        //metodo para los provedores
        private void showProveedor()
        {
            DataSet ds= new DataSet();
            ds = objprov.showProveedor();
            GVProveedor.DataSource = ds;
            GVProveedor.DataBind();
            
        }
        //eventos que se ejecutan cuando se d aclic para los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nit = TBNit.Text;
            _nombre = TBNombre.Text;

            executed = objprov.saveProveedor(_nit, _nombre);

            if (executed)
            {
                LblMsg.Text = "El rol se guardo exitosamente";
                showProveedor();

            }
            else 
            {
                LblMsg.Text = "Error al guardar";
            
            }


        }
    }
}