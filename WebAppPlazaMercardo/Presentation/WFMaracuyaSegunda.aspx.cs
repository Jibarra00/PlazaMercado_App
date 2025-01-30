using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFMaracuyaSegunda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnPedido_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://localhost:44331/WFPedidos.aspx");
        }
    }
}