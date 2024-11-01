using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Presentation
{
    public partial class WFComentario : System.Web.UI.Page
    {
        ClienteLog objCli = new ClienteLog();
        ProductoLog objpro = new ProductoLog();
        ComentarioLog objco = new ComentarioLog();

        private string p_com_text;
        private DateTime p_com_fecha;
        private int p_com_clasificacion, _fkclient, _fkproduct;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showComment();
                showCommentDDL();
                showClientsDDL();
                showProductoDDL();
            }
        }

        private void showComment()
        {
            DataSet ds = new DataSet();
            ds = objco.showComment();
            GVComment.DataSource = ds;
            GVComment.DataBind();
        }

        private void showCommentDDL()
        {
            DDLComment.DataSource = objco.showComentDDL();
            DDLComment.DataValueField = "com_id"; //Nombre de la llave primaria
            DDLComment.DataTextField = "com_text";
            DDLComment.DataBind();
            DDLComment.Items.Insert(0, "Seleccione");
        }

        private void showClientsDDL()
        {
            DDLClient.DataSource = objCli.showClientDDL();
            DDLClient.DataValueField = "cli_id"; //Nombre de la llave primaria
            DDLClient.DataTextField = "cli_nombres";
            DDLClient.DataTextField = "cli_apellidos";
            DDLClient.DataBind();
            DDLClient.Items.Insert(0, "Seleccione");
        }

        private void showProductoDDL()
        {
            DDLProducto.DataSource = objpro.showProductoDDL();
            DDLProducto.DataValueField = "pro_id"; //Nombre de la llave primaria
            DDLProducto.DataValueField = "pro_codigo";
            DDLProducto.DataTextField = "pro_descripcion";
            DDLProducto.DataBind();
            DDLProducto.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            p_com_text = TBText.Text;
            p_com_fecha = Convert.ToDateTime(TBDate.Text);
            p_com_clasificacion = Convert.ToInt32(TBClassification.Text);
            _fkclient = Convert.ToInt32(DDLClient.SelectedValue);
            _fkproduct = Convert.ToInt32(DDLProducto.SelectedValue);

            executed = objco.saveComment(p_com_text, p_com_fecha, p_com_clasificacion, _fkclient, _fkproduct);

            if (executed)
            {
                LblMsg.Text = "Se ha guardado el comentario exitosamente!";
                showComment();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
    }
}