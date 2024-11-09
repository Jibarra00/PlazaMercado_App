    using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
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

        private int _id, _fkcliente, _fkprducto;
        private string _state, _specification;
        private DateTime _date;

        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
                //showPedidos();
                showClienteDDL();
                showProductoDDL();
            }
        }


        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListPedidos()
        {
            PedidosLog objPed = new PedidosLog();
            // Se obtiene un DataSet que contiene la lista de pedidos desde la base de datos.
            var dataSet = objPed.showPedidos();
            // Se crea una lista para almacenar los pedidos que se van a devolver.
            var pedidosList = new List<object>();
            // Se itera sobre cada fila del DataSet (que representa un pedido)
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                pedidosList.Add(new
                {
                    PedidosID = row["ped_id"],
                    Fecha = Convert.ToDateTime(row["ped_fecha"]).ToString("yyyy-MM-dd"),
                    Estado = row["ped_estado"],
                    Especificadion = row["ped_especificacion"],
                    FkCliente = row["tbl_cliente_cli_id"],
                    nameCliente = row["cli_nombres"],
                    FkProducto = row["tbl_producto_pro_id"],
                    nameProducto = row["pro_descripcion"]
                });
            }
            return new { data = pedidosList };
        }

        [WebMethod]
        public static bool DeletePedido(int id)
        {
            // Crear una instancia de la clase de lógica de pedidos
            PedidosLog objPed = new PedidosLog();
            // Invocar al método para eliminar el pedido y devolver el resultado
            return objPed.DeletePedidos(id);
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
        private void clear()
        {
            HFPedidosID.Value = "";
            TBFecha.Text = "";
            DDLEstado.SelectedIndex = 0;
            TBEspecificacion.Text = "";
            DDLCliente.SelectedIndex = 0;
            DDLProducto.SelectedIndex = 0;
            

        }

        //Eventos cuando se ejecutan al dar click en los botones
        protected void BTSave_Click(object sender, EventArgs e)
        {
            _date = Convert.ToDateTime(TBFecha.Text);
            _state = DDLEstado.SelectedValue;
            _specification = TBEspecificacion.Text;
            _fkcliente = Convert.ToInt32(DDLCliente.SelectedValue);
            _fkprducto = Convert.ToInt32(DDLProducto.SelectedValue);

            executed = objPed.savePedidos(_date,_state,_specification,_fkcliente,_fkprducto);

            if (executed)
            {
                LblMsg.Text = "Se guardo el pedido correctamente";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
        protected void BTUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un pedido para actualizar
            if (string.IsNullOrEmpty(HFPedidosID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un pedido para actualizar";
                return;
                
            }
            _id = Convert.ToInt32(HFPedidosID.Value);
            _date = Convert.ToDateTime(TBFecha.Text);
            _state = DDLEstado.SelectedValue;
            _specification = TBEspecificacion.Text;
            _fkcliente = Convert.ToInt32(DDLCliente.SelectedValue);
            _fkprducto = Convert.ToInt32(DDLProducto.SelectedValue);

            executed = objPed.updatePedidos(_id,_date,_state,_specification,_fkcliente,_fkprducto);

            if (executed)
            {
                LblMsg.Text = "Se guardo el pedido correctamente";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }
    }
}