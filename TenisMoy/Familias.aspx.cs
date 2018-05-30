using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using CrsClass;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace TenisMoy
{
    public partial class Familias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Control asc = LoadControl("controles/Menup.ascx");
            cssmenu.Controls.Add(asc);
            FillTable();
        }
        void EditFam(object sender, EventArgs e)
        {
            Session["_Xparam"] = CrsUtil.ConvertTo<int>(((Button)sender).ID.Replace("btn", ""));
            Response.Redirect("EditFam.aspx");
        }
        public void FillTable()
        {
            XResponse resp = CrsData.TableSelect(new List<string> { "uid", "nombre" }, "familias", "type = '1'", BuildType.LstDctStrStr);
            if (resp.error) { MessageBox.Alert("Ocurrio un error al cargar los datos: " + resp.msgerr, this.Page, this); return; }
            List<Dictionary<string, string>> data = (List<Dictionary<string, string>>)resp.objecto;
            HtmlTableRow row = new HtmlTableRow();
            Button btn;
            foreach (Dictionary<string, string> item in data)
            {
                btn = new Button();
                btn.CssClass = "btn btn-primary btn-sm";
                btn.ID = "btn" + item["uid"];
                btn.Text = "Edit";
                btn.Click += EditFam;
                row = new HtmlTableRow();
                HtmlTableCell cel = new HtmlTableCell();
                cel.Controls.Add(btn);
                row.Cells.Add(cel);
                row.Cells.Add(new HtmlTableCell { InnerText = item["uid"] });
                row.Cells.Add(new HtmlTableCell { InnerText = item["nombre"] });
                tableSeek.Rows.Add(row);
            }
        }
    }
}