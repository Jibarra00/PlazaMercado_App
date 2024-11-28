    using Logic;
using Model;
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

        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                PanelAdmin.Visible = false;
                FrmPedidos.Visible = false;
                //showPedidos();
                showClienteDDL();
                showProductoDDL();
            }
            validatePermisoRol();
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

        private void validatePermisoRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("WFDefault.aspx");
                return;
            }
            // Obtener el rol del usuario
            var userRole = objUser.Rol.Nombre;

            if (userRole == "Administrador")
            {
                //LblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmPedidos.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPedidos.Visible = true;
                            BtnUpdate.Visible = true;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = true;// Se pone visible el panel
                            _showEditButton = true;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;// Se pone visible el boton eliminar dentro de la datatable
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Empleado")
            {
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUsuario.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermiso.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkPermisoRol.Visible = false;// Se oculta el enlace de Permiso Rol
                masterPage.linkCategoria.Visible = false;
                masterPage.linkComentario.Visible = false;
                masterPage.linkGestion.Visible = false;
                masterPage.linkCliente.Visible = false;
                masterPage.linkEmpleado.Visible = false;
                masterPage.linkRol.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmPedidos.Visible = false;// Se pone visible el formulario
                            BtnSave.Visible = false;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPedidos.Visible = true;
                            BtnUpdate.Visible = true;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = true;// Se pone visible el panel
                            _showEditButton = true;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;// Se pone visible el boton eliminar dentro de la datatable
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Cliente")
            {
                //LblMsg.Text = "Bienvenido, Secretaria!";
                masterPage.linkUsuario.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermiso.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkPermisoRol.Visible = false;// Se oculta el enlace de Permiso Rol
                masterPage.linkCategoria.Visible = false;
                masterPage.linkGestion.Visible = false;
                masterPage.linkRol.Visible = false;
                masterPage.linkCliente.Visible = false;
                masterPage.linkEmpleado.Visible = false;
                masterPage.linkProveedor.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmPedidos.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmPedidos.Visible = true;
                            BtnUpdate.Visible = true;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = true;// Se pone visible el panel
                            _showEditButton = true;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;// Se pone visible el boton eliminar dentro de la datatable
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }
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