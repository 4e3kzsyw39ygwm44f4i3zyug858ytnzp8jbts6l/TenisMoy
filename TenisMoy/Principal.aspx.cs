using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TenisMoy
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control asc = LoadControl("controles/Menup.ascx");
            cssmenu.Controls.Add(asc);
        }
    }
}