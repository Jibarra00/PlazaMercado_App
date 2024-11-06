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
    public partial class WFPedidos : System.Web.UI.Page
    {
        //Crear los objetos
        ClienteLog objCli = new ClienteLog();
        ProductoLog objProd = new ProductoLog();
        PedidosLog objPed = new PedidosLog();

        private int id, _fkcliente, _fkprducto;
        private string _state, _specification;
        private DateTime _date;

        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
                showPedidos();
                showClienteDDL();
                showProductoDDL();
            }
        }

        

        private void showPedidos()
        {
            DataSet ds = new DataSet();
            ds = objPed.showPedidos();
            GVPedidos.DataSource = ds;
            GVPedidos.DataBind();
        }
        private void showClienteDDL()
        {
            DDLCliente.DataSource = objCli.showClientDDL();
            DDLCliente.DataValueField = "cli_id";
            DDLCliente.DataTextField = "cli_nombres";
            DDLCliente.DataBind();
            DDLCliente.Items.Insert(0, "Seleccione el cliente");
        }
        private void showProductoDDL()
        {
           
           DDLProducto.DataSource = objProd.showProductoDDL();
            DDLProducto.DataValueField = "pro_id";
            DDLProducto.DataTextField = "codigoDescripcion";
            DDLProducto.DataBind();
            DDLProducto.Items.Insert(0, "Seleccione el producto");
        }

        //Eventos cuando se ejecutan al dar click en los botones
        protected void BTSave_Click(object sender, EventArgs e)
        {
            _date = Convert.ToDateTime(TBFecha.Text);
            _state = TBEstado.Text;
            _specification = TBEspecificacion.Text;
            _fkcliente = Convert.ToInt32(DDLCliente.SelectedValue);
            _fkprducto = Convert.ToInt32(DDLProducto.SelectedValue);

            executed = objPed.savePedidos(_date,_state,_specification,_fkcliente,_fkprducto);

            if (executed)
            {
                LblMsg.Text = "Se guardo el pedido correctamente";
                showPedidos();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
    }
}