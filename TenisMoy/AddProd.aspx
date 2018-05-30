<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProd.aspx.cs" Inherits="TenisMoy.AddProd" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/leonora.css" />
    <script src="js/Leonora.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { menumaker({ title: "Menu", format: "multitoggle" }); });
    </script>
    <title>Agregar Almacen</title>
</head>
<body class="bg-secondary">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="cssmenu"></asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="divResponse" CssClass="" Style="text-align: center" role="alert">
            <asp:Label runat="server" ID="lbresponse" Text=""></asp:Label>
            <a id="btncloseresp">X</a>
        </asp:Panel>
        <div>
        </div>
    </form>
</body>
</html>
