using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class Main : System.Web.UI.MasterPage
    {
        /*
         * HtmlAnchor, es una clase que proporciona una forma de manipular enlaces HTML
         * directamente desde el código en el servidor.
         */
        public HtmlAnchor linkCategoria;
        public HtmlAnchor linkCliente;
        public HtmlAnchor linkComentario;
        public HtmlAnchor linkEmpleado;
        public HtmlAnchor linkGestion;
        public HtmlAnchor linkPedidos;
        public HtmlAnchor linkPermiso;
        public HtmlAnchor linkPermisoRol;
        public HtmlAnchor linkProducto;
        public HtmlAnchor linkProveedor;
        public HtmlAnchor linkRol;
        public HtmlAnchor linkUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}