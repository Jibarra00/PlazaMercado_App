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
    public partial class WFGestion : System.Web.UI.Page
    {
        GestionLog objGes = new GestionLog();
        EmpleadoLog objEmp = new EmpleadoLog();
        ProductoLog objPro = new ProductoLog();

        private DateTime p_ges_fecha;
        private string p_ges_descripcion;
        private int _fkemployee, p_ges_id;
        private int _fkproduct;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //showManagement();
                showEmployeesDDL();
                showProductsDDL();
            }
        }

        [WebMethod]
        public static object ListManagement()
        {
            GestionLog objGes = new GestionLog();
            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objGes.showManagement();
            // Se crea una lista para almacenar los productos que se van a devolver.
            var managementList = new List<object>();
            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                managementList.Add(new
                {
                    ManagementID = row["ges_id"],
                    Description = row["ges_descripcion"],
                    Date = Convert.ToDateTime(row["ges_fecha"]).ToString("yyyy-MM-dd"),
                    Employee = row["tbl_empleado_emp_id"],
                    Product = row["tbl_producto_pro_id"],
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = managementList };
        }

        [WebMethod]
        public static bool DeleteManagement(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            GestionLog objGes = new GestionLog();
            // Invocar al método para eliminar el producto y devolver el resultado
            return objGes.deleteManagement(id);
        }

        private void clear()
        {
            HFManagementID.Value = "";
            TBDescription.Text = "";
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DDLEmployee.SelectedIndex = 0;
            DDLProduct.SelectedIndex = 0;
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
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            {
                // Verifica si se ha seleccionado un producto para actualizar
                if (string.IsNullOrEmpty(HFManagementID.Value))
                {
                    LblMsg.Text = "No se ha seleccionado una gestión para actualizar.";
                    return;
                }
                p_ges_id = Convert.ToInt32(HFManagementID.Value);
                p_ges_descripcion = TBDescription.Text;
                p_ges_fecha = DateTime.Parse(TBDate.Text);
                _fkemployee = Convert.ToInt32(DDLEmployee.SelectedValue);
                _fkproduct = Convert.ToInt32(DDLProduct.SelectedValue);
                executed = objGes.updateManagement(p_ges_id, p_ges_fecha, p_ges_descripcion, _fkemployee, _fkproduct);
                if (executed)
                {
                    LblMsg.Text = "La gestión se actualizó exitosamente!";
                    clear(); //Se invoca el metodo para limpiar los campos 
                }
                else
                {
                    LblMsg.Text = "Error al actualizar";
                }
            }
        }
    }
}