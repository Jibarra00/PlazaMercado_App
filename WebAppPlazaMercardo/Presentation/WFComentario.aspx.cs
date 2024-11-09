using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Services;
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
        private int p_com_clasificacion, _fkclient, _fkproduct, p_com_id;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //showComment();
                //showCommentDDL();
                showClientsDDL();
                showProductoDDL();
            }
        }


        [WebMethod]
        public static object ListComments()
        {

            ComentarioLog objCom = new ComentarioLog();
            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objCom.showComment();
            // Se crea una lista para almacenar los productos que se van a devolver.
            var commentsList = new List<object>();
            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                commentsList.Add(new
                {
                    CommentID = row["com_id"],
                    Text = row["com_text"],
                    Date = Convert.ToDateTime(row["com_fecha"]).ToString("yyyy-MM-dd"),
                    Classification = row["com_clasificacion"],
                    Client = row["tbl_cliente_cli_id"],
                    Product = row["tbl_producto_pro_id"],
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = commentsList };
        }

        [WebMethod]
        public static bool DeleteComment(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            ComentarioLog objCom = new ComentarioLog();
            // Invocar al método para eliminar el producto y devolver el resultado
            return objCom.deleteComment(id);
        }

        private void clear()
        {
            HFCommentID.Value = "";
            TBText.Text = "";
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBClassification.Text = "";
            DDLClient.SelectedIndex = 0;
            DDLProducto.SelectedIndex = 0;
        }

        private void showClientsDDL()
        {
            DDLClient.DataSource = objCli.showClientDDL();
            DDLClient.DataValueField = "cli_id"; //Nombre de la llave primaria
            DDLClient.DataTextField = "cli_nombres";
            DDLClient.DataBind();
            DDLClient.Items.Insert(0, "Seleccione");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFCommentID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un comentario para actualizar.";
                return;
            }
            p_com_id = Convert.ToInt32(HFCommentID.Value);
            p_com_clasificacion = Convert.ToInt32(TBClassification.Text);
            p_com_text = TBText.Text;
            p_com_fecha = DateTime.Parse(TBDate.Text);
            _fkclient = Convert.ToInt32(DDLClient.SelectedValue);
            _fkproduct = Convert.ToInt32(DDLProducto.SelectedValue);

            executed = objco.updateComment(p_com_id, p_com_clasificacion, p_com_text, p_com_fecha, _fkclient, _fkproduct);
            if (executed)
            {
                LblMsg.Text = "El comentario se actualizó exitosamente!";

                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        private void showProductoDDL()
        {
            DDLProducto.DataSource = objpro.showProductoDDL();
            DDLProducto.DataValueField = "pro_id"; //Nombre de la llave primaria
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
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
    }
}