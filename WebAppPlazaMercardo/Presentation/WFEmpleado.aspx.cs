using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
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
        private string _identification, _names, _lastnames, _phone, _addres;
        private int _id;    

        

        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //showEmployees();
            }
        }

        //Metodo para mostrar todos los productos
        /*
      * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
      * parte de un servicio web, lo que significa que puede ser invocado de manera
      * remota a través de HTTP.
      */
        [WebMethod]
        public static object ListEmployees()
        {
            try
            {
                EmpleadoLog objEmp = new EmpleadoLog();

                // Se obtiene un DataSet que contiene la lista de empleados desde la base de datos.
                var dataSet = objEmp.showEmployees();

                // Validación para comprobar si hay datos en el DataSet
                if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                {
                    return new { data = new List<object>(), success = false, message = "No hay empleados para mostrar" };
                }

                // Se crea una lista para almacenar los empleados que se van a devolver.
                var employeesList = new List<object>();

                // Se itera sobre cada fila del DataSet (que representa un empleado).
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    employeesList.Add(new
                    {
                        EmployeeID = row["emp_id"],
                        Identification = row["emp_identificacion"],
                        Name = row["emp_nombres"],
                        lastName = row["emp_apellidos"],
                        Phone = row["emp_telefono"],
                        Addres = row["emp_direccion"]
                    });
                }

                // Devuelve un objeto en formato JSON que contiene la lista de empleados.
                return new { data = employeesList, success = true };
            }
            catch (Exception ex)
            {
                // Registro de errores para depuración
                return new { data = new List<object>(), success = false, message = ex.Message };
            }
        }


        [WebMethod]
        public static bool DeleteEmployee(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            EmpleadoLog objEmp = new EmpleadoLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objEmp.deleteEmployee(id);
        }

        private void clear()
        {
            HFEmployeeID.Value = "";
            TBCC.Text = "";
            TBname.Text = "";
            TBlastname.Text = "";
            TBphone.Text = "";
            TBaddres.Text = "";
        }

       

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un empleado para actualizar
            if (string.IsNullOrEmpty(HFEmployeeID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un Empleado para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFEmployeeID.Value);
            _identification = TBCC.Text;
            _names = TBname.Text;
            _lastnames = TBlastname.Text;
            _phone = TBphone.Text;
            _addres = TBaddres.Text;

            executed = objEmp.updateEmployee(_id, _identification, _names, _lastnames, _phone, _addres);

            if (executed)
            {
                LblMsg.Text = "El Empleado se actualizó exitosamente!";
                clear(); // Limpiar los campos después de actualizar
            }
            else
            {
                LblMsg.Text = "Error al actualizar.";
            }
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            {
                _identification = TBCC.Text;
                _names = TBname.Text;
                _lastnames = TBlastname.Text;
                _phone = TBphone.Text;
                _addres = TBaddres.Text;

                executed = objEmp.saveEmployee(_identification, _names, _lastnames, _phone, _addres);
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
