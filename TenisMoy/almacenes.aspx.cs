using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrsClass;
using System.Web.UI.HtmlControls;

namespace TenisMoy
{
    public partial class almacenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Control asc = LoadControl("controles/Menup.ascx");
            cssmenu.Controls.Add(asc);
            FillTable();
        }
        public void FillTable()
        {
            List<string> campos = new List<string> { "uid", "id", "name" };
            XResponse resp = CrsData.TableSelect(campos, "almacenes", "", BuildType.LstDctStrStr);
            if (resp.error) { MessageBox.Alert("Ocurrio un error al cargar los datos: " + resp.msgerr, this.Page, this); return; }
            List<Dictionary<string, string>> data = (List<Dictionary<string, string>>)resp.objecto;
            HtmlTableRow row = new HtmlTableRow();
            foreach (Dictionary<string, string> item in data)
            {
                row = new HtmlTableRow();
                row.Cells.Add(new HtmlTableCell { InnerHtml = "<button id=\"btn" + item["uid"] + "\" class=\"btn btn - primary btn - sm\">Edit</button>" });
                //<asp:Button runat = "server" ID = "btnsave" OnClick = "btnsave_Click" CssClass = "btn btn-primary btn-lg" Text = "Crear" />
                row.Cells.Add(new HtmlTableCell { InnerText = item["id"] });
                row.Cells.Add(new HtmlTableCell { InnerText = item["name"] });
                tableSeek.Rows.Add(row);
            }
        }
    }
}