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
    public partial class WFPermisoRol : System.Web.UI.Page
    {
        PermisoLog objPer = new PermisoLog();
        RolLog objRol = new RolLog();
        PermisoRolLog objPerRol = new PermisoRolLog();

        private int _id, _fkRol, _fkPermiso;
        private DateTime _date;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                showPermisoDDL();
                showRolDDL();
            }
        }

        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */
        [WebMethod]
        public static object ListPermisoRol()
        {
            PermisoRolLog objPerRol = new PermisoRolLog();

            var dataSet = objPerRol.showPermisoRol();

            var permisosRolList = new List<object>();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                permisosRolList.Add(new
                {
                    PermisoRolID = row["rol_permiso"],
                    FkPermiso = row["tbl_permiso_per_id"],
                    namePermiso = row["per_nombre"],
                    FkRol = row["tbl_rol_rol_id"],
                    nameRol = row["rol_nombre"],
                    Date = Convert.ToDateTime(row["per_rol_fecha_asignacion"]).ToString("yyyy-MM-dd ddd")
                });
            }
            return new { data = permisosRolList };
        }
        [WebMethod]

        public static bool DeletePermisosRol(int id)
        {
            PermisoRolLog objPerRol = new PermisoRolLog();

            return objPerRol.deletePermisoRol(id);
        }

        private void showPermisoDDL()
        {
            DDLPermisos.DataSource = objPer.showPermisoDDL();
            DDLPermisos.DataValueField = "per_id";
            DDLPermisos.DataTextField = "per_nombre";
            DDLPermisos.DataBind();
            DDLPermisos.Items.Insert(0, "Seleccione el Rol");
        }


        private void showRolDDL()
        {
            DDLRoles.DataSource = objRol.showrolDDL();
            DDLRoles.DataValueField = "rol_id";
            DDLRoles.DataTextField = "rol_nombre";
            DDLRoles.DataBind();
            DDLRoles.Items.Insert(0, "Seleccione el Rol");
        }

        private void clear()
        {
            HFPermisoRolID.Value = "";
            DDLPermisos.SelectedIndex = 0;
            DDLRoles.SelectedIndex = 0;
            TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            _fkPermiso = Convert.ToInt32(DDLPermisos.SelectedValue);
            _fkRol = Convert.ToInt32(DDLRoles.SelectedValue);
            _date = DateTime.Parse(TBFecha.Text);

            executed = objPerRol.savePermisoRol(_fkPermiso, _fkRol,_date);

            if (executed)
            {
                LblMsg.Text = "El permiso rol se guardo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFPermisoRolID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un permiso rol para actualizar";
                return;
            }

            _id = Convert.ToInt32(HFPermisoRolID.Value);
            _fkPermiso = Convert.ToInt32(DDLPermisos.SelectedValue);
            _fkRol = Convert.ToInt32(DDLRoles.SelectedValue);
            _date = DateTime.Parse(TBFecha.Text);

            executed = objPerRol.updatePermisoRol(_id, _fkPermiso, _fkRol, _date);

            if (executed)
            {
                LblMsg.Text = "El permiso rol se actualizo exitosamente!";
                clear();//Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizo";
            }
        }
    }
}