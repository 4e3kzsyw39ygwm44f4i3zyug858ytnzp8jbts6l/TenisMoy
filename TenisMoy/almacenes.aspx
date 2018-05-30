<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="almacenes.aspx.cs" Inherits="TenisMoy.almacenes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/leonora.css" />
    <script src="js/Leonora.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { menumaker({ title: "Menu", format: "multitoggle" }); });
    </script>
    <title>Catalogo de almacenes</title>
</head>
<body class="bg-secondary">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="cssmenu"></asp:Panel>
        <asp:Panel runat="server" Visible="false" ID="divResponse" CssClass="" Style="text-align: center" role="alert">
            <asp:Label runat="server" ID="lbresponse" Text=""></asp:Label>
            <a id="btncloseresp">X</a>
        </asp:Panel>
        <div class="container">
            <asp:Panel runat="server" CssClass="card form-group m-2">
                <div class="card-header p-0 pl-4">
                    <h3>Catalogo de almacenes</h3>
                </div>
                <label class="card-text pl-2 font-weight-bold">Si elimina un almacen su tabla de existencias no se borrará</label>
                <div class="card-body p-0 pl-2 pb-2 pr-2 ">
                    <div class="justify-content-start shadow-sm">
                        <label class="card-text ">Si crea un almacen son el mismo ID se vincularan con la tabla de existencias que coincida con el id del almacen</label>
                    </div>
                </div>
            </asp:Panel>
            <div class="card text-white bg-info m-2">
                <div class="card-header p-0 pl-2">
                    <h4>Almacenes</h4>
                </div>
                <div class="card-body table-responsive">
                    <table runat="server" id="tableSeek" class="table table-sm table-hover ">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">ID</th>
                                <th scope="col">Nombre</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
