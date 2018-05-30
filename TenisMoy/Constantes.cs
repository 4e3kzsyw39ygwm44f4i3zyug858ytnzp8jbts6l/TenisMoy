using CrsClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class constantes
{
    public const int limTallasInStkTallas = 18;//limi de tallas mostradas en el cuadro de stktallas
    public const string pathcarevi = "Provider=VFPOLEDB.1;Data Source=C:\\mc_pos\\data\\carevi.DBC;";
    public const string uuser = "tenismoy";
    public const string password = "uhTGQd6bd04r";
    public const string database = "tenismoy";
    public const string server = "carand.com.mx";
    public static List<string> prodSelCampos = new List<string> { "codprod", "nombre", "uid", "ultcosto" };//campos que usa la global de producto seleccionado 
    //las tallas debren coincidir con el numero de tallas en table y ambos list deben estar del mismo ancho y posocion
    public static List<string> tallasTabla = new List<string> { "_80", "_85", "_90", "_100", "_105", "_110", "_115", "_120", "_125", "_130", "_135", "_140", "_145", "_150", "_155", "_160", "_165", "_170", "_175", "_180", "_185", "_190", "_195", "_200", "_205", "_210", "_215", "_220", "_225", "_230", "_235", "_240", "_245", "_250", "_255", "_260", "_265", "_270", "_275", "_280", "_285", "_290", "_295", "_300", "_305", "_310", "_320", "_330", "_340" };//campos que usa la global de producto seleccionado 
    public static List<string> tallasShow = new List<string> { "8.0", "8.5", "9.0", "10.0", "10.5", "11.0", "11.5", "12.0", "12.5", "13.0", "13.5", "14.0", "14.5", "15.0", "15.5", "16.0", "16.5", "17.0", "17.5", "18.0", "18.5", "19.0", "19.5", "20.0", "20.5", "21.0", "21.5", "22.0", "22.5", "23.0", "23.5", "24.0", "24.5", "25.0", "25.5", "26.0", "26.5", "27.0", "27.5", "28.0", "28.5", "29.0", "29.5", "30.0", "30.5", "31.0", "32.0", "33.0", "34.0" };//campos que usa la global de producto seleccionado 
}
public class CodexDBF
{
    public static XResponse GetStringTable(TableCode type, string alias)
    {
        List<XField> fields = new List<XField>();
        bool increment;
        switch (type)
        {
            case TableCode.Stk_Ax:
                fields = new List<XField>();
                increment = true;
                fields.Add(new XField { name = "uidprod", defVal = TDefVals.cero, type = TFldType.integer, nulo = false, unique = true });
                fields.Add(new XField { name = "uidtalla", defVal = TDefVals.cero, type = TFldType.integer, nulo = false });
                fields.Add(new XField { name = "stock", defVal = TDefVals.cero, type = TFldType.integer, nulo = true });
                fields.Add(new XField { name = "deleted", defVal = TDefVals.cero, type = TFldType.tinyint, nulo = false });
                break;
            default:
                return CrsCore.ResponseError(CrsCore.SaveLog("GetStringTable no se encontro el tipo de tabla"));
        }
        if (string.IsNullOrEmpty(alias)) { return CrsCore.ResponseError(CrsCore.SaveLog("GetStringTable no alias")); }
        List<string> campos = new List<string>();
        if (increment) { campos.Add(" uid int NOT NULL AUTO_INCREMENT UNIQUE"); }
        foreach (XField item in fields) { campos.Add(BuildField(item)); }
        return new XResponse { response = "BEGIN;" + string.Format("CREATE TABLE {0} ({1});", alias, string.Join(",", campos)) + "COMMIT;" };
    }
    public static string BuildField(XField field)
    {
        return string.Format("{0} {1} {2} {3} {5} {4}",
                field.name,
                field.type.ToString(),
                string.IsNullOrEmpty(field.length) ? "" : string.Format("({0})", field.length),
                field.nulo ? "NULL" : "NOT NULL",
                field.unique ? "UNIQUE" : "",
                string.IsNullOrEmpty(field.defVal) ? "" : string.Format("DEFAULT {0}", field.defVal));
    }
    public enum TableCode
    {
        Stk_Ax, Cat_Ax
    }
    public enum TFldType { varchar, bigint, datetime, tinyint, IDENTITY, integer, Decimal, Float }
    public class TDefVals
    {
        public static string timestamp = "CURRENT_TIMESTAMP";
        public static string cero = "'0'";
        public static string vacio = "''";
    }
    public struct XField
    {
        public string name;
        public string defVal;
        public TFldType type;
        public string length;
        public bool nulo;
        public bool unique;
    }
    public struct XNewTable
    {
        public string alias;
        public bool UIDInc;
        public List<XField> campos;
    }

}