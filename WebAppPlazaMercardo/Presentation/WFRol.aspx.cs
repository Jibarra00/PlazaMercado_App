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
                //showRol();
                
            }
                

        }
        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListRol()
        {

            RolLog objRol = new RolLog();
            //se obtine el dataset  que  contiene la lista  de roles de la base de datos.
            var dataSet = objRol.showRol();
            //se crea una lista para almacenar los roles que se van a devolver
            var RolList = new List<object>();
            //seitera sobre cada fila del dataset(que se presentan un rol).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                RolList.Add(new
                {
                    rolID = row["rol_id"],
                    Nombre = row["rol_nombre"],
                    Descripcion = row["rol_descripcion"]

                });
               
            }
            //devuelve un objeto en formato JSON que contiene la lista de roles.
            return new {data = RolList};
        }
        [WebMethod]
        public static bool DeleteRol(int id) 
        {
            // Crear una instancia de la clase de lógica de roles
            RolLog objRol = new RolLog();

            // Invocar al método para eliminar el rol y devolver el resultado
            return objRol.deleteRol(id);
        }
        //metodo para limpiar los texbox 
        private void clear() 
        {
            HFRolID.Value = "";
            TBNombre.Text = "";
            TBdescripcion.Text = "";
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
                clear();

            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //verificar si se ha seleccioando  un rol para actualizar
            if (string.IsNullOrEmpty(HFRolID.Value))
            {
                LblMsg.Text = "No se ha sellecionado un rol para actualizar";
                return;
            }
            _id = Convert.ToInt32(HFRolID.Value);
            _nombre = TBNombre.Text;
            _descripcion = TBdescripcion.Text ;

            executed = objRol.updateRol(_id, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "Se actuaslizo el Rol";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }

        }


    }
}