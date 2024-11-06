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
    public partial class WFRol : System.Web.UI.Page
    {
        //crear los objetos
        RolLog objRol = new RolLog();

        private int _id;
        private string _nombre, _descripcion;
        private bool executed = false;





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los metodos
                showRol();
            }
                

        }

        

        //metodo para los roles
        private void showRol()
        {
            DataSet ds = new DataSet();
            ds = objRol.showRol();
            GVRol.DataSource = ds;
            GVRol.DataBind();

        }
        //eventos que se ejecutan cuando se da clic en los botones

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBNombre.Text;
            _descripcion = TBdescripcion.Text;

            executed = objRol.saveRol(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "el rol se guardo exitosamente";
                showRol();

            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }


            


        
        }

    }
}