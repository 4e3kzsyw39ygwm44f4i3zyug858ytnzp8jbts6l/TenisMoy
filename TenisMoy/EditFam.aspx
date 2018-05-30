<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditFam.aspx.cs" Inherits="TenisMoy.EditFam" %>

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
    <title>Editar Familia</title>
</head>
<body class="bg-secondary">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="cssmenu"></asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="divResponse" CssClass="" Style="text-align: center" role="alert">
            <asp:Label runat="server" ID="lbresponse" Text=""></asp:Label>
            <a id="btncloseresp">X</a>
        </asp:Panel>
        <div class="container">
            <div class="jumbotron">
                <h1 class="display-5">Editar Familia</h1>
                <p class="lead">Si cambia el nombre de una familia no se perdera el vinculo con los productos a los que este asignada.</p>
                <p>Asegurese de no crear familias con nombres duplicados... </p>
                <p>Revise la lista de familias para asegurarse que no existe la familia que quiere dar de alta.</p>
                <hr class="my-4" />
                <p>Escriba el nuevo nombre para la familia.</p>
                <div class="form-group ">
                    <label for="txtnombre">Nombre:</label>
                    <asp:TextBox runat="server" ID="txtnombre" class="form-control col-sm-12 col-lg-3" type="text" />
                </div>
                <p class="lead">
                    <asp:Button runat="server" ID="btnsave" OnClick="btnsave_Click" CssClass="btn btn-primary btn-lg" Text="Crear" />
                </p>
            </div>
        </div>
    </form>
</body>
</html>
