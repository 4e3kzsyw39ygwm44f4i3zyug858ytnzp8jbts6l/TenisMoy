<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TenisMoy.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/leonora.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/Leonora.js"></script>
    <title>leonora</title>

</head>
<body class="bg-secondary">
    <asp:Panel runat="server" Visible="false" ID="divResponse" CssClass="container alert alert-danger col-lg-10 col-sm-12 col-md-12" Style="text-align: center" role="alert">
            <asp:Label runat="server" ID="lbresponse" Text=""></asp:Label>
            <a id="btncloseresp">X</a>
        </asp:Panel>
    <div class="container">
        <div class="row">
            <form class="form-signin" runat="server" id="form1">
                <h2 class="form-signin-heading ltxt4">Inicio de sesion</h2>
                <label for="usuario" class="sr-only">Usuario</label>
                <input runat="server" type="text" name="usuario" id="usuario" class="form-control" placeholder="Usuario" required autofocus/>
                <label for="contrasenia" class="sr-only">Password</label>
                <input runat="server" type="password" name="contrasenia" id="contrasenia" class="form-control" placeholder="Password" required/>
                <asp:Button runat="server" ID="btnlogin" OnClick="btnlogin_Click" Text="Entrar" class="btn btn-lg btn-primary btn-block" type="submit"></asp:Button>
            </form>
        </div>
    </div>
</body>
</html>
