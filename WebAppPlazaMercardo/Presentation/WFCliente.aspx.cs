using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFCliente : System.Web.UI.Page
    {
        //Se crea el Objeto
        ClienteLog objCli = new ClienteLog();
        //Se definen los atributos
        private int _id;
        private string _names, _lastnames, _mail, _phone, _addres;

        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //showClients();
            }
        }
        /*
 * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
 * parte de un servicio web, lo que significa que puede ser invocado de manera
 * remota a través de HTTP.
 */
        [WebMethod]
        public static object ListClients()
        {
            ClienteLog objCli = new ClienteLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objCli.showClient();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var clientsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                clientsList.Add(new
                {
                    ClientID = row["cli_id"],
                    Name = row["cli_nombres"],
                    lastName = row["cli_apellidos"],
                    Mail = row["cli_correo"],
                    Phone = row["cli_telefono"],
                    Addres = row["cli_direccion"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de clientes
            return new { data = clientsList };
        }


        [WebMethod]
        public static bool DeleteClient(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            ClienteLog objCli = new ClienteLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objCli.deleteClient(id);
        }

        private void clear()
        {
            HFClientID.Value = "";
            TBName.Text = "";
            TBlastname.Text = "";
            TBMail.Text = "";
            TBPhone.Text = "";
            TBAddres.Text = "";
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HFClientID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un cliente para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFClientID.Value);
            _names = TBName.Text;
            _lastnames = TBlastname.Text;
            _mail = TBMail.Text;
            _phone = TBPhone.Text;
            _addres = TBAddres.Text;


            executed = objCli.updateClient(_id, _names, _lastnames, _mail, _phone, _addres);

            if (executed)
            {
                LblMsg.Text = "El cliente se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
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
    }
}