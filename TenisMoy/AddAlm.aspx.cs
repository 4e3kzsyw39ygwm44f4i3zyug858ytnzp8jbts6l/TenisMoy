using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrsClass;
namespace TenisMoy
{
    public partial class AddAlm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Control asc = LoadControl("controles/Menup.ascx");
            cssmenu.Controls.Add(asc);

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string name = txtCodalm.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(lbresponse, divResponse, "Escriba un nombre!!");
                return;
            }
            XResponse resp = CrsData.TableSelect(new List<string> { "nvalor", "uid" }, "control", "", BuildType.LstDctStrDyn);
            if (resp.error || resp.empty) { MessageBox.Error(lbresponse, divResponse, resp.coderr); return; }
            List<Dictionary<string, dynamic>> data = (List<Dictionary<string, dynamic>>)resp.objecto;
            int prefix = data[0]["nvalor"];
            int recontrol = data[0]["uid"];
            //insert in almacenes
            Dictionary<string, dynamic> campos = new Dictionary<string, dynamic>();
            campos.Add("id", "A" + prefix);
            campos.Add("name", name);
            campos.Add("status", "1");
            resp = CrsData.TableInsert("almacenes", campos);
            if (resp.error) { MessageBox.Error(lbresponse, divResponse, resp.coderr); return; }
            //create tabla de stocks
            string alias = "Stk_A" + prefix;
            bool? b = CrsData.ChkTable(alias);
            if (b == null) { MessageBox.Show(lbresponse, divResponse, "Se creo el almacen pero no Stk_Ax.\r\n favor de llamar a sistemas"); return; }
            if (b.Equals(false))
            {
                resp = CrsData.CreateTable(CodexDBF.TableCode.Stk_Ax, alias);
                if (resp.error) { MessageBox.Error(lbresponse, divResponse, resp.coderr, "No se creo la tabla stock Error:"); return; }
            }
            /*create tabla de catalogo
            alias = "Cat_A" + prefix;
             b = CrsData.ChkTable(alias);
            if (b == null) { MessageBox.Show(lbresponse, divResponse, "Se creo el almacen pero no Cat_Ax.\r\n favor de llamar a sistemas"); return; }
            if (b.Equals(false))
            {
                resp = CrsData.CreateTable(CodexDBF.TableCode.Stk_Ax, alias);
                if (resp.error) { MessageBox.MsgError(lbresponse, divResponse, resp.coderr, "No se creo la tabla stock Error:"); return; }
            }*/
            //update control
            prefix++;
            CrsData.TableUpdate("control", new Dictionary<string, dynamic> { { "nvalor", prefix } }, "uid = '" + recontrol + "'");
            MessageBox.Success(lbresponse, divResponse, "Se creo el almacen!!");
        }
    }
}