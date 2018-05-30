using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CrsClass;

namespace TenisMoy
{
    public partial class AddFam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Control asc = LoadControl("controles/Menup.ascx");
            cssmenu.Controls.Add(asc);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            string name = txtnombre.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(lbresponse, divResponse, "Escriba un nombre!!");
                return;
            }
            //insert in familias
            Dictionary<string, dynamic> campos = new Dictionary<string, dynamic>();
            campos.Add("nombre", name);
            campos.Add("type", "1");
            XResponse resp = CrsData.TableInsert("familias", campos);
            if (resp.error) { MessageBox.Error(lbresponse, divResponse, resp.coderr); return; }

            MessageBox.Success(lbresponse, divResponse, "Se creo la Familia!!");
        }
    }
}