using Data;
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
    public partial class WFGraficas : System.Web.UI.Page
    {
        //Crear los objetos
        ProductoLog objPro = new ProductoLog();
        UsuarioLog objUsu = new UsuarioLog();
        EmpleadoLog objEmp = new EmpleadoLog();
        PedidosLog objPed = new PedidosLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showCountProducts();
                showCountUsers();
                showCountEmployee();

            }
            validatePermisoRol();
        }
        [WebMethod]
        public static object ListCountProductsCategories()
        {
            ProductoLog objProd = new ProductoLog();

            // Se obtiene un DataSet que contiene la lista de productos que existen por categoria
            var dataSet = objProd.showCountProductsCategories();

            // Se crea una lista para almacenar las cantidades que de productos x categorias 
            var prodCatList = new List<object>();

            // Se itera sobre cada fila del DataSet.
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                prodCatList.Add(new
                {
                    CategoryName = row["Categoria"],
                    TotalProducts = row["TotalProductos"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos x categorias.
            return new { data = prodCatList };
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
                Response.Redirect("Default.aspx");
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

                            break;
                        case "ACTUALIZAR":

                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
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

                            break;
                        case "ACTUALIZAR":

                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
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

                            break;
                        case "ACTUALIZAR":

                            break;
                        case "MOSTRAR":

                            break;
                        case "ELIMINAR":

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

        [WebMethod]
        public static object ListProductsByDate()
        {
            PedidosLog objPed = new PedidosLog();

            List<ProductByDate> products = objPed.CountProductsByDate();
            var productList = new List<object>();

            // Iterar sobre los datos obtenidos y estructurarlos
            foreach (var product in products)
            {
                productList.Add(new
                {
                    Fecha = product.Fecha.ToString("yyyy-MM-dd"), // Formato de fecha
                    NombreProducto = product.NombreProducto,
                    Cantidad = product.Cantidad
                });
            }

            // Devolver un objeto en formato JSON
            return new { data = productList };
        }




        //Muestra cuantos productos existen
        private void showCountProducts()
        {
            int count = objPro.showCountProducts();
            LblCantProd.Text = count.ToString();
        }
        //Muestra cuantos usuarios existen
        public void showCountUsers()
        {
            int count = objUsu.showCountUsers();
            LblCantUsu.Text = count.ToString();
        }
        //Muestra cuantos empleados existen
        public void showCountEmployee()
        {
            int count = objEmp.showCountEmployee();
            LblCantEmp.Text = count.ToString();
        }


    }
}