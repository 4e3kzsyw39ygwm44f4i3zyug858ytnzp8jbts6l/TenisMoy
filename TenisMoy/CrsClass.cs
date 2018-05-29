using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;

namespace CrsClass
{
    public class XtrCursor
    {
        public string response { get; set; }
        public bool error { get; set; }
        public string msgerr { get; set; }
        public bool empty { get; set; }
        public List<Row> rows { get; set; }
        public class Row
        {
            public string recno { get; set; }
            public string sort { get; set; }
            public List<string> lData { get; set; }
            public Dictionary<string, string> dData { get; set; }
        }
    }
    public struct XtrDataConnect
    {
        public bool connected;
        public MySqlConnection connection;
        public string msg;
    }
    public struct XResponse
    {
        public bool error;
        public bool empty;
        public string msgerr;
        public string coderr;
        public int coderesp;
        public string response;//string 
        public object objecto;
    }
    public struct Tabla
    {
        public string alias;//usado en update e insert
        public string where;//clausula where para update o insert
        public bool error;
        public XResponse response;
        public Dictionary<string, dynamic> datos;
    }
    public enum BuildType
    {
        LstDctStrStr = 12,
        LstDctStrDyn = 11,
        TablaArray = 1,
        DataTable = 2,
        LstCrsseek = 3
    }
    /*methods*/
    public class MessageBox
    {
        public static void Error(Label lb, Panel pn, string coderr, string msg = "")
        {
            if (string.IsNullOrEmpty(msg)) { msg = Resources.Resources.ErrorGen; }
            lb.Text = msg + coderr;
            pn.CssClass = "container alert alert-danger col-lg-10 col-sm-12 col-md-12";
            pn.Visible = true;
        }
        public static void Show(Label lb, Panel pn, string msg)
        {
            lb.Text = msg;
            pn.CssClass = "container alert alert-warning col-lg-10 col-sm-12 col-md-12";
            pn.Visible = true;
        }
        public static void Success(Label lb, Panel pn, string msg)
        {
            lb.Text = msg;
            pn.CssClass = "container alert alert-success col-lg-10 col-sm-12 col-md-12";
            pn.Visible = true;
        }
        public static void Alert(string ex, Page pg, object obj)
        {// MessageBox.Alert("Ocurrio un error" + resp.msgerr, this.Page, this); 
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }

    /*public class Security
    {
        public static bool Access(string idcontrol)
        {
            MainWindow frmain = (MainWindow)Application.Current.MainWindow;

            MainWindow._UserLoged globauser = (MainWindow._UserLoged)frmain.GetValue(MainWindow._UserLogged);
             XResponse resp = CrsData.TableSelect(new List<string> { "users","block"}, "access", "idcontrol = \"" + idcontrol + "\"",12);
            if (resp.error || resp.empty) { LeonoraCore.Core.MsgInBar("Acceso Denegado!!!."); return false; }
            List<Dictionary<string, string>> data = (List<Dictionary<string, string>>)resp.objecto;
            if(data.Count.Equals(0)|| !data[0].ContainsKey("users")) { LeonoraCore.Core.MsgInBar("Acceso Denegado!!!."); return false; }
            string[] access= data[0]["users"].Split(',');
            if (data[0]["block"].Equals("1")) { LeonoraCore.Core.MsgInBar("Elemento bloqueado!!!."); return false; }
            if (!access.Contains(globauser.uid.ToString())) { LeonoraCore.Core.MsgInBar("Acceso Denegado!!!."); return false; }
            else { return true; }
        }
        public static MainWindow._UserLoged GetUserLogged()
        {
            MainWindow frmain = (MainWindow)Application.Current.MainWindow;
            return (MainWindow._UserLoged)frmain.GetValue(MainWindow._UserLogged);
        }
    }*/
    public class Excel
    {
        public static void Exportar(DataTable data)
        {
            /* SaveFileDialog fichero = new SaveFileDialog();
             fichero.Filter = "Excel (*.xlsx)|*.xlsx";
             if (!fichero.ShowDialog().Equals(true)) { return; }
             CrsExcel.ExportToExcel(data, fichero.FileName);*/
        }
    }
    public class CrsXml
    {
        public static string GetTag(string Cadena, string Buscar)
        {
            int posi, posf, leng;
            posi = Cadena.IndexOf("[" + Buscar + "]");
            posf = Cadena.IndexOf("[/" + Buscar + "]");
            if (posf == -1) { return ""; }
            posi += Buscar.Length + 2;
            leng = posf - posi;
            string valor = Cadena.Substring(posi, leng);
            return valor;
        }
        public static bool ExistTag(string Cadena, string Buscar)
        {// devuelve true o false si existe o no un tag
            int posi;
            posi = Cadena.IndexOf("[" + Buscar + "]");
            return (posi >= 0);
        }
        public static string BuildTag(string text, string tag)
        {
            return "[" + tag + "]" + text + "[/" + tag + "]";
        }
    }




    /// <summary>
    /// Usado en buscador y el tableselect para devolver una lista 
    /// </summary>
    public class CrsSeek
    {
        public string codigo { get; set; }
        public string descrip { get; set; }
        public int uid { get; set; }
    }
    public class CrsCore
    {
        public static string SaveLog(string msg)
        {
            string code = CrsUtil.RandString(6);
            XUserLoged usrlog = (XUserLoged)HttpContext.Current.Session["_UserLoged"];
            if (usrlog.uid.Equals(0)) { usrlog.name = "no user"; }
            CrsData.TableInsert("log", new Dictionary<string, dynamic> { { "message", msg.Replace("'", "\"") }, { "codigo", code }, { "uiduser", usrlog.uid.ToString() }, { "username", usrlog.name } });
            return code;
        }
        public static XResponse ResponseError(string code)
        {
            return new XResponse { error = true, coderr = code };
        }
    }
    public class CrsUtil
    {
        public static string RandString(int cont)
        {
            string CODE = "";
            Random n = new Random(Guid.NewGuid().GetHashCode());
            int val = 0;
            while (cont > 0)
            {
                val = n.Next(47, 123);
                if ((val > 48 && val < 57) || (val > 97 && val < 123))
                {
                    CODE += (char)val;
                    cont--;
                }
            }
            return CODE;
        }
        public static string RandNum(int cont)
        {
            string CODE = "";
            Random n = new Random(Guid.NewGuid().GetHashCode());
            int val = 0;
            while (cont > 0)
            {
                val = n.Next(47, 57);
                if ((val > 48 && val < 57))
                {
                    CODE += (char)val;
                    cont--;
                }
            }
            return CODE;
        }
        public static bool Is0NullorEmpty(string param)
        {//devuelve true si es cero null o vacio
            bool resp = false;
            resp = string.IsNullOrEmpty(param);
            if (resp) { return resp; }
            decimal tmp;
            decimal.TryParse(param, out tmp);
            return tmp.Equals(0);
        }
        public static dynamic ConvertTo<T>(string value) where T : struct
        {
            Type f = typeof(T);
            switch (f.Name)
            {
                case "Double":
                    double vdouble;
                    double.TryParse(value, out vdouble);
                    return vdouble;
                case "Int32":
                case "Int64":
                    int vint;
                    int.TryParse(value, out vint);
                    return vint;
                case "Decimal":
                    decimal vdecimal;
                    decimal.TryParse(value, out vdecimal);
                    return vdecimal;
                default:
                    return true;
            }
        }
        public static bool IsEmpty<T>(string value) where T : struct
        {
            Type f = typeof(T);
            switch (f.Name)
            {
                case "Double":
                    double vdouble;
                    if (!double.TryParse(value, out vdouble)) { return true; }
                    return (vdouble <= 0);
                case "Int32":
                case "Int64":
                    int vint;
                    if (!int.TryParse(value, out vint)) { return true; }
                    return (vint <= 0);
                case "Decimal":
                    decimal vdecimal;
                    if (!decimal.TryParse(value, out vdecimal)) { return true; }
                    return (vdecimal <= 0);
                default:
                    return true;
            }
        }
        public static bool Is<T>(string value) where T : struct
        {//revisa si un valor es de un type espesifico aunque sea 0, 0=true
            Type f = typeof(T);
            switch (f.Name)
            {
                case "Double":
                    double vdouble;
                    return double.TryParse(value, out vdouble);
                case "Int32":
                case "Int64":
                    int vint;
                    return int.TryParse(value, out vint);
                case "Decimal":
                    decimal vdecimal;
                    return decimal.TryParse(value, out vdecimal);
                case "DateTime":
                    DateTime tdecimal;
                    return DateTime.TryParse(value, out tdecimal);
                default:
                    return true;
            }
        }
        public static string GetDictforError(Dictionary<string, string> campos)
        {
            if (campos.Count == 0) { return ""; }
            string salida = "";
            List<string> llaves = campos.Keys.ToList<string>();
            List<string> valores = campos.Values.ToList<string>();
            for (int a = 0; a < campos.Count; a++) { salida += llaves[a] + ":" + valores[a] + ","; }
            return salida.Substring(0, salida.Length - 1);
        }
        public static string GetDictforError(Dictionary<string, dynamic> campos)
        {
            if (campos.Count == 0) { return ""; }
            string salida = "";
            List<string> llaves = campos.Keys.ToList<string>();
            List<dynamic> valores = campos.Values.ToList<dynamic>();
            for (int a = 0; a < campos.Count; a++) { salida += llaves[a] + ":" + Convert.ToString(valores[a]) + ","; }
            return salida.Substring(0, salida.Length - 1);
        }
        public static string GetTimeStp()
        {//devuelve timestamp en formato string
            long n = Int64.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmssms"));
            return Convert.ToString(n);
        }
        public static int GetIntFromName(string name)
        {//devuelve el int despues del name 
            return Convert.ToInt16(name.Substring(name.Length - 1));
        }
        public static string ValidDecimal(string Texto)
        {
            string Cadena = "";
            int Band = 0;
            int Tam = Texto.Length;
            if (Tam > 0)
            {
                for (int i = 0; i < Tam; i++)
                {
                    if (Convert.ToInt32(Texto[i]) == 46)
                    {
                        if (Band == 0)
                        {
                            Cadena += Texto[i];
                        }
                        Band += 1;
                    }
                    else if (!Char.IsDigit(Texto[i]))
                    {

                    }
                    else
                    {
                        Cadena += Texto[i];
                    }

                }
            }
            return Cadena;
        }
        public static string ValidInt(string Texto)
        {
            string Cadena = "";
            int Tam = Texto.Length;
            if (Tam > 0)
            {
                for (int i = 0; i < Tam; i++)
                {
                    if (Char.IsDigit(Texto[i]))
                    {
                        Cadena += Texto[i];
                    }
                }
            }
            return Cadena;
        }
        /// <summary>
        /// devuelve el uid si ya existe sino devuelve -1 (la tabla debe tener un campo uid)
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="campo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetUID(string alias, string campo, string value)
        {
            string where = campo + " ='" + value.Trim() + "'";
            XResponse resp = CrsData.TableSelect(new List<string> { "uid" }, alias, where, BuildType.TablaArray);
            if (resp.empty || resp.error) { return -1; }
            Tabla[] tabla = (Tabla[])resp.objecto;
            return tabla[0].datos["uid"];
        }
        public static bool Duplicated(string alias, string campo, string value)
        {
            string where = campo + " ='" + value.Trim() + "'";
            XResponse resp = CrsData.TableSelect(new List<string> { campo }, alias, where, 0);
            return !resp.empty;
        }
    }
    public class CrsData
    {
        public static bool? ChkTable(string table)
        {
            if (string.IsNullOrEmpty(table)) { CrsCore.SaveLog("ChkTable Falta dbase o tabla"); return null; }
            XtrDataConnect mysql = new XtrDataConnect();
            if (!connect(ref mysql)) { CrsCore.SaveLog("ChkTable connect fail:" + mysql.msg); return null; }
            //armando query 
            string lcquery = string.Format("SELECT COUNT(*) AS count FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '{1}'", constantes.database, table);
            MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
            try
            {
                MySqlDataReader myreader = mycomand.ExecuteReader();
                if (!myreader.HasRows) { mysql.connection.Close(); return false; }
                List<Dictionary<string, dynamic>> data = (List<Dictionary<string, dynamic>>)BuildOut(BuildType.LstDctStrDyn, myreader);
                mysql.connection.Close();
                return (data[0]["count"] > 0);
            }
            catch (Exception e)
            {
                CrsCore.SaveLog("ChkTable catch:" + e.Message);
                mysql.connection.Close();
                return null;
            }

        }


        public static XResponse CreateTable(CodexDBF.TableCode codex, string name = "")
        {
            //ARMANDO QUERY
            XResponse resp = CodexDBF.GetStringTable(codex, name);
            if (resp.error) { return resp; }
            string lcquery = resp.response;
            //enviando query
            XtrDataConnect mysql = new XtrDataConnect();
            if (!connect(ref mysql)) { return CrsCore.ResponseError(CrsCore.SaveLog("CreateTable connect fail:" + mysql.msg)); }
            MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
            try
            {
                mycomand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                mysql.connection.Close();
                return CrsCore.ResponseError(CrsCore.SaveLog("CreateTable catch:" + lcquery + " " + ex.Message));
            }
            mysql.connection.Close();
            return new XResponse();
        }
        private static object BuildOut(BuildType output, MySqlDataReader myreader)
        {
            DataTable dtstruct = new DataTable();
            object salida = new object();
            DataTable datatable = new DataTable();
            Tabla[] tabla = new Tabla[0];
            switch (output)
            {
                case BuildType.LstDctStrStr://List<Dictionary<string, string>>
                    List<Dictionary<string, string>> ltabla2 = new List<Dictionary<string, string>>();
                    dtstruct = myreader.GetSchemaTable();
                    Dictionary<string, string> datos2;
                    //llenando la tabla
                    while (myreader.Read())
                    {
                        datos2 = new Dictionary<string, string>();
                        for (int a = 0; a < myreader.FieldCount; a++)
                        { datos2.Add((dtstruct.Rows[a])["ColumnName"].ToString(), Convert.ToString(myreader.GetValue(a))); }
                        ltabla2.Add(datos2);
                    }
                    salida = ltabla2;
                    break;
                case BuildType.LstDctStrDyn://List<Dictionary<string, dynamic>>
                    List<Dictionary<string, dynamic>> ltabla = new List<Dictionary<string, dynamic>>();
                    dtstruct = myreader.GetSchemaTable();
                    Dictionary<string, dynamic> datos;
                    //llenando la tabla
                    while (myreader.Read())
                    {
                        datos = new Dictionary<string, dynamic>();
                        for (int a = 0; a < myreader.FieldCount; a++)
                        { datos.Add((dtstruct.Rows[a])["ColumnName"].ToString(), myreader.GetValue(a)); }
                        ltabla.Add(datos);
                    }
                    salida = ltabla;
                    break;
                case BuildType.TablaArray://Tabla[]
                    dtstruct = myreader.GetSchemaTable();
                    string[] fields = new string[myreader.FieldCount];
                    int count = -1;
                    string valor = "";
                    foreach (DataRow myField in dtstruct.Rows)
                    {
                        count++;
                        valor = myField["ColumnName"].ToString();
                        fields[count] = valor;
                    }
                    //llenando la tabla
                    int tmpi = 0;
                    while (myreader.Read())
                    {
                        tmpi = tabla.Length;
                        Array.Resize(ref tabla, tmpi + 1);
                        tabla[tmpi].datos = new Dictionary<string, dynamic>();
                        for (int a = 0; a < myreader.FieldCount; a++)
                        {
                            tabla[tmpi].datos.Add(fields[a], myreader.GetValue(a));
                        }
                    }
                    /*int p = tabla[0].datos["uid"] + 5;*/
                    salida = tabla;
                    break;

                case BuildType.DataTable://DataTable 
                    datatable.Load(myreader);
                    salida = datatable;
                    break;
                case BuildType.LstCrsseek://list<Crsseek> usado para buscador  
                    List<CrsSeek> lista = new List<CrsSeek>();
                    //llenando la tabla
                    while (myreader.Read())
                    {
                        lista.Add(new CrsSeek()
                        {
                            codigo = (string)myreader.GetValue(0),
                            descrip = (string)myreader.GetValue(1),
                        });
                    }
                    salida = lista;
                    break;
                default:
                    return CrsCore.ResponseError(CrsCore.SaveLog("BuildOut Tipo de output invaldo"));
            }
            return salida;
        }
        public static XResponse TableSelect(List<string> campos, string from, string where, BuildType output)
        {
            XResponse salida = new XResponse();
            if (string.IsNullOrEmpty(from)) { return CrsCore.ResponseError(CrsCore.SaveLog("TableSelect Falta from")); }
            DataTable dtstruct = new DataTable();
            XtrDataConnect mysql = new XtrDataConnect();
            if (!connect(ref mysql)) { return CrsCore.ResponseError(CrsCore.SaveLog("TableSelect connect fail:" + mysql.msg)); }
            //armando query
            string lcquery = "select ";
            string lcampos = "";
            string lcfrom = " from " + from;
            string lcwhere = " where deleted = 0";
            lcwhere += (string.IsNullOrEmpty(where)) ? "" : " and " + where;
            for (int a = 0; a < campos.Count; a++)
            { lcampos += campos[a] + ","; }
            lcampos = lcampos.Substring(0, lcampos.Length - 1);
            lcquery += lcampos + lcfrom + lcwhere + " LIMIT 0,100";
            MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
            MySqlDataReader myreader = mycomand.ExecuteReader();
            salida.empty = !myreader.HasRows;
            if (output == 0)
            {
                object[] sql = new object[2];
                sql[0] = myreader;
                sql[1] = mysql.connection;
                salida.objecto = sql;
            }
            else
            {
                salida.objecto = BuildOut(output, myreader);
            }
            if (!output.Equals(0))
            {
                myreader.Close();
                mysql.connection.Close();
            }
            return salida;
        }
        public static object TraslateFields(object valor)
        {
            object newval = "";
            Type tipo = valor.GetType();
            switch (tipo.Name)
            {
                case "DateTime":
                    DateTime dt = (DateTime)valor;
                    if (dt.Year.Equals(1899)) { newval = null; }
                    else { newval = dt.ToString("yyyy-MM-dd HH:mm:ss.fff"); }
                    break;
                case "Boolean":
                    bool b = (bool)valor;
                    newval = (b.Equals(true)) ? "1" : "0";
                    break;
                default:
                    newval = valor;
                    break;
            }
            return newval;
        }
        public static XResponse GetFieldByUID(string campo, string alias, int uid)
        {
            XResponse resp = CrsData.TableSelect(new List<string> { campo }, "productos", "uid = " + uid, BuildType.TablaArray);
            if (resp.error || resp.empty) { return resp; }
            Tabla[] tabla = (Tabla[])resp.objecto;
            resp.response = (string)tabla[0].datos[campo];
            return resp;
        }
        /*public static XResponse TableDelete(string alias, int uid)
        {
            Dictionary<string, dynamic> datos = new Dictionary<string, dynamic>();
            datos.Add("deleted", "1");
            string where = "uid = " + uid.ToString();
            Tabla send = new Tabla();
            send.alias = alias;
            send.datos = datos;
            send.where = where;
            return TableUpdate(send);
        }*/
        /*public static XResponse KillData(string alias, string where)
        {
            if (string.IsNullOrEmpty(alias)) { return CrsCore.ResponseError(600, "alias esta vacio"); }
            if (string.IsNullOrEmpty(where)) { return CrsCore.ResponseError(600, "where esta vacio"); }
            //ARMANDO QUERY
            string lcquery = "BEGIN;DELETE FROM " + alias + " WHERE " + where + ";COMMIT;";
            //enviando query
            XtrDataConnect mysql = new XtrDataConnect();
            if (!connect(ref mysql)) { return CrsCore.ResponseError(200, mysql.msg); }
            MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
            int colaf = 0;
            try
            {
                colaf = mycomand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                mysql.connection.Close();
                return CrsCore.ResponseError(201, "Query:" + lcquery + " sqlsay:" + ex.Message);
            }
            mysql.connection.Close();
            return CrsCore.Response();
        }*/
        public static XResponse TableUpdate(string alias, Dictionary<string, dynamic> campos, string where)
        {
            if (campos.Count == 0) { return CrsCore.ResponseError(CrsCore.SaveLog("TableUpdatecampos esta vacia")); }
            if (string.IsNullOrEmpty(alias)) { return CrsCore.ResponseError(CrsCore.SaveLog("tableupdate alias esta vacia")); }
            if (string.IsNullOrEmpty(where)) { return CrsCore.ResponseError(CrsCore.SaveLog("tableupdate where esta vacia")); }

            //ARMANDO QUERY
            string lcquery = "BEGIN;";
            where = "where " + where;
            lcquery += "update " + alias + " set ";
            string valor = "";
            foreach (KeyValuePair<string, dynamic> colum in campos)
            {
                object rigival = TraslateFields(colum.Value);
                valor = Convert.ToString(rigival);
                if (rigival == null) { continue; }
                lcquery += colum.Key + " = '" + valor + "',";
            }//compatibilidad con datos importados de carevi
            lcquery = lcquery.Substring(0, lcquery.Length - 1);//quitando ultima coma 
            lcquery += " " + where + ";";//cerrando instruccion

            lcquery += " COMMIT;";
            //enviando query
            XtrDataConnect mysql = new XtrDataConnect();
            if (!connect(ref mysql)) { return CrsCore.ResponseError(CrsCore.SaveLog("tableupdate connect fail:" + mysql.msg)); }
            MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
            int colaf = 0;
            try
            {
                colaf = mycomand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                mysql.connection.Close();
                return CrsCore.ResponseError(CrsCore.SaveLog("tableupdate catch:" + lcquery + " sqlsay:" + ex.Message));
            }
            mysql.connection.Close();
            return new XResponse { response = colaf.ToString() };
        }
        /*        public static XResponse TableUpdate(List<Tabla> tabla)
                {
                    if (tabla.Count == 0) { return CrsCore.ResponseError(600, "tabla esta vacia"); }
                    //ARMANDO QUERY
                    string lcquery = "BEGIN;";
                    string where = "";
                    foreach (Tabla item in tabla)
                    {
                        if (string.IsNullOrEmpty(item.alias)) { continue; }
                        where = (string.IsNullOrEmpty(item.where)) ? "" : "where " + item.where;
                        lcquery += "update " + item.alias + " set ";
                        string valor = "";
                        foreach (KeyValuePair<string, dynamic> colum in item.datos)
                        {
                            object rigival = TraslateFields(colum.Value);
                            valor = Convert.ToString(rigival);
                            if (rigival == null) { continue; }
                            lcquery += colum.Key + " = '" + valor + "',";
                        }//compatibilidad con datos importados de carevi
                        lcquery = lcquery.Substring(0, lcquery.Length - 1);//quitando ultima coma 
                        lcquery += " " + where + ";";//cerrando instruccion
                    }
                    lcquery += " COMMIT;";
                    //enviando query
                    XtrDataConnect mysql = new XtrDataConnect();
                    if (!connect(ref mysql)) { return CrsCore.ResponseError(200, mysql.msg); }
                    MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
                    int colaf = 0;
                    try
                    {
                        colaf = mycomand.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        mysql.connection.Close();
                        return CrsCore.ResponseError(201, "Query:" + lcquery + " sqlsay:" + ex.Message);
                    }
                    mysql.connection.Close();
                    return CrsCore.Response(colaf, "termino!!");
                }*/
        /*public static XResponse TableUpdate(Tabla tabla)
        {
            return TableUpdate(new List<Tabla> { tabla });
        }*/
        /*public static XResponse TableInsUpd(Tabla tabla)
        {
            return TableInsUpd(new List<Tabla>() { tabla });
        }*/
        /*   public static XResponse TableInsert(Tabla tabla)
           {
               return TableInsert(new List<Tabla>() { tabla }, true);
           }*/
        /*  public static XResponse TableInsert(List<Tabla> lista)
          {
              return TableInsert(lista, true);
          }*/
        public static XResponse TableInsert(string alias, Dictionary<string, dynamic> campos)
        {
            if (campos.Count == 0) { return CrsCore.ResponseError(CrsCore.SaveLog("tableinsert campos esta vacia")); }
            if (string.IsNullOrEmpty(alias)) { return CrsCore.ResponseError(CrsCore.SaveLog("tableinsert alias esta vacia")); }

            //ARMANDO QUERY
            string lcquery = "BEGIN;";
            string lccampos = "";
            string lcvalues = "";
            //for (int r = 0; r < lista.Length; r++)

            lcquery += "insert into " + alias;
            lccampos = " (";
            lcvalues = " values(";
            string valor = "";
            //agregando camps
            foreach (KeyValuePair<string, dynamic> colum in campos)
            {
                object rigival = TraslateFields(colum.Value);
                if (rigival == null) { continue; }
                valor = Convert.ToString(rigival);
                lccampos += colum.Key + ",";
                lcvalues += "'" + valor + "',";
            }
            lccampos = lccampos.Substring(0, lccampos.Length - 1) + ")";//quitando ultima coma 
            lcvalues = lcvalues.Substring(0, lcvalues.Length - 1) + ")";//quitando ultima coma 
            lcquery += lccampos + lcvalues + ";";//cerrando instruccion

            lcquery += "COMMIT;";
            //enviando query
            XtrDataConnect mysql = new XtrDataConnect();
            if (!connect(ref mysql)) { return CrsCore.ResponseError(CrsCore.SaveLog("tableinsert connect fail:" + mysql.msg)); }
            MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
            int colaf = 0;
            try
            {
                colaf = mycomand.ExecuteNonQuery();
                //MySqlDataReader myreader = mycomand.ExecuteReader();
                //while (myreader.Read()) { colaf = (int)myreader.GetValue(0); }
            }
            catch (MySqlException ex)
            {
                mysql.connection.Close();
                return CrsCore.ResponseError(CrsCore.SaveLog("tableinsert catch:" + lcquery + " " + ex.Message));
            }
            mysql.connection.Close();
            return new XResponse { response = colaf.ToString() };
        }
        /*public static XResponse TableInsert(List<Tabla> lista, bool retUid)
        {
            if (lista.Count == 0) { return CrsCore.ResponseError(600, "tabla esta vacia"); }
            //ARMANDO QUERY
            string lcquery = "BEGIN;";
            string lccampos = "";
            string lcvalues = "";
            //for (int r = 0; r < lista.Length; r++)
            foreach (Tabla tabla in lista)
            {
                if (string.IsNullOrEmpty(tabla.alias)) { continue; }
                lcquery += "insert into " + tabla.alias;
                lccampos = " (";
                lcvalues = " values(";
                string valor = "";
                //agregando camps
                foreach (KeyValuePair<string, dynamic> colum in tabla.datos)
                {
                    object rigival = TraslateFields(colum.Value);
                    if (rigival == null) { continue; }
                    valor = Convert.ToString(rigival);
                    lccampos += colum.Key + ",";
                    lcvalues += "'" + valor + "',";
                }
                lccampos = lccampos.Substring(0, lccampos.Length - 1) + ")";//quitando ultima coma 
                lcvalues = lcvalues.Substring(0, lcvalues.Length - 1) + ")";//quitando ultima coma 
                lcquery += lccampos + lcvalues + ";";//cerrando instruccion
            }
            //agregando consulta para devolver el uid
            if (retUid)
            {
                lcquery += "SELECT uid FROM " + lista[0].alias + " ORDER BY uid DESC LIMIT 1;";
            }
            lcquery += "COMMIT;";
            //enviando query
            XtrDataConnect mysql = new XtrDataConnect();
            if (!connect(ref mysql)) { return CrsCore.ResponseError(200, mysql.msg); }
            MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
            int colaf = 0;
            try
            {
                //colaf = mycomand.ExecuteNonQuery();
                MySqlDataReader myreader = mycomand.ExecuteReader();
                while (myreader.Read()) { colaf = (int)myreader.GetValue(0); }
            }
            catch (MySqlException ex)
            {
                mysql.connection.Close();
                return CrsCore.ResponseError(201, "Query:" + lcquery + " " + ex.Message);
            }
            mysql.connection.Close();
            return CrsCore.Response(colaf, "termino!!" + lcquery);
        }*/
        /*public static XResponse TableInsUpd(List<Tabla> lista)
        {
            if (lista.Count == 0) { return CrsCore.ResponseError(600, "tabla esta vacia"); }
            //ARMANDO QUERY
            string lcquery = "BEGIN;";
            string lccampos = "";
            string lcvalues = "";
            string lccampos2 = "";
            //for (int r = 0; r < lista.Length; r++)
            foreach (Tabla tabla in lista)
            {
                if (string.IsNullOrEmpty(tabla.alias)) { continue; }
                lcquery += "insert into " + tabla.alias;
                lccampos = " (";
                lccampos2 = " ";
                lcvalues = " values(";
                string valor = "";
                //agregando camps
                foreach (KeyValuePair<string, dynamic> colum in tabla.datos)
                {
                    object rigival = TraslateFields(colum.Value);
                    if (rigival == null) { continue; }
                    valor = Convert.ToString(rigival);
                    lccampos += colum.Key + ",";
                    lcvalues += "'" + valor + "',";
                    lccampos2 += colum.Key + " = '" + valor + "',"; ;
                }
                lccampos = lccampos.Substring(0, lccampos.Length - 1) + ")";//quitando ultima coma 
                lccampos2 = lccampos2.Substring(0, lccampos2.Length - 1);//quitando ultima coma 
                lcvalues = lcvalues.Substring(0, lcvalues.Length - 1) + ")";//quitando ultima coma 
                lcquery += lccampos + lcvalues + " ON DUPLICATE KEY UPDATE" + lccampos2 + ";";//cerrando instruccion
            }
            lcquery += "COMMIT;";
            //enviando query
            XtrDataConnect mysql = new XtrDataConnect();
            if (!connect(ref mysql)) { return CrsCore.ResponseError(200, mysql.msg); }
            MySqlCommand mycomand = new MySqlCommand(lcquery, mysql.connection);
            int colaf = 0;
            try
            {
                colaf = mycomand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                mysql.connection.Close();
                return CrsCore.ResponseError(201, "Query:" + lcquery + " " + ex.Message);
            }
            mysql.connection.Close();
            return CrsCore.Response(colaf, "termino!!" + lcquery);
        }*/
        public static bool connect(ref XtrDataConnect datacon)
        {
            datacon.connected = false;
            try
            {
                string lc = "Server=" + constantes.server +
                    ";Database=" + constantes.database +
                    ";Uid= " + constantes.uuser +
                    ";Pwd= " + constantes.password +
                    ";Port=3306;SslMode=none";
                MySqlConnection con = new MySqlConnection(lc);
                con.Open();
                datacon.connection = con;
                datacon.connected = true;
                return datacon.connected;
            }
            catch (MySqlException ex)
            {
                datacon.msg = ex.Message;
                return false;
            }

        }
    }
}