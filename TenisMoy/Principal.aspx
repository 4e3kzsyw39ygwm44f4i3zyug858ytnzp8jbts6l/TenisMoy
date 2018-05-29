<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="TenisMoy.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/leonora.css" />
    <script src="js/leonora.js"></script>
  <script type="text/javascript">
      $(document).ready(function () { menumaker({ title: "Menu", format: "multitoggle" }); });
  </script>
    <title>Menu</title>
</head>
<body class="bg-secondary">
    <form id="form1" runat="server">
        <asp:Panel runat="server" id="cssmenu"></asp:Panel>
        <div>
        </div>
    </form>
</body>
</html>
