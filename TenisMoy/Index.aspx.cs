using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace TenisMoy
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string username = usuario.Value;
            string password = contrasenia.Value;
            if (username.Length.Equals(0) || password.Length.Equals(0))
            { MessageBox.Show(lbresponse, divResponse, "Complete la información"); return; }
            XResponse resp = CrsData.TableSelect(new List<string> { "iduser", "username", "tienda" }, "usuarios", "username = '" + username + "' and password = '" + password + "'", BuildType.LstDctStrDyn);
            if (resp.error) { MessageBox.Error(lbresponse, divResponse, resp.coderr); return; }
            if (resp.empty) { MessageBox.Show(lbresponse, divResponse, "Datos de sesion incorrectos"); return; }
            List<Dictionary<string, dynamic>> tabla = (List<Dictionary<string, dynamic>>)resp.objecto;
            XUserLoged _UserLoged = new XUserLoged
            {
                uid = (int)tabla[0]["iduser"],
                tienda = (string)tabla[0]["tienda"],
                name = (string)tabla[0]["username"]
            };
            Session["_UserLoged"] = _UserLoged;
            Response.Redirect("Principal.aspx");
        }
    }
}