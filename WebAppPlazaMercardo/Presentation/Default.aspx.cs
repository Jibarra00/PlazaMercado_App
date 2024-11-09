using Logic;
using Model;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class Default : System.Web.UI.Page
    {
        UsuarioLog objUsuLog = new UsuarioLog();
        User objUser = new User();

        private string _correo;
        private string _contrasena;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnIniciar_Click(object sender, EventArgs e)
        {
            ICryptoService cryptoService = new PBKDF2();

            _correo = TBCorreo.Text;
            _contrasena = TBContrasena.Text;

            objUser = objUsuLog.showUsersMail(_correo);
            if (objUser != null) 
            { 
                if(objUser.State == "Activo")// Verifica si el usuario está activo
                {
                    Session["User"] = objUser;
                    string passEncryp = cryptoService.Compute(_contrasena, objUser.Salt);
                    if(cryptoService.Compare(objUser.Contrasena, passEncryp))
                    {
                        FormsAuthentication.RedirectFromLoginPage("WFInicio.aspx", true);
                        TBCorreo.Text = "";
                        TBContrasena.Text = "";
                    }
                    else
                    {
                        LblMsg.Text = "Correo o Contraseña Incorrectos!";
                    }
                }
                else
                {
                    LblMsg.Text = "El usuario no está activo. Contacte al administrador.";
                }
            }
            else
            {
                LblMsg.Text = "Correo o Contraseña Incorrectos!";
            }


        }
    }
}