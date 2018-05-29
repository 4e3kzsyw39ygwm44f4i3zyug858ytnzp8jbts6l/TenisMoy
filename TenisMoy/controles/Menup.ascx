<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menup.ascx.cs" Inherits="TenisMoy.controles.Menup" %>
<ul>
    <li><a href="../Principal.aspx">Home</a></li>
    <li class='active'><a href='#'>Utilerias</a>
        <ul>
            <li><a href='../almacenes.aspx'>Almacenes</a>
                <ul>
                    <li><a href="../AddAlm.aspx">Crear Almacen</a></li>
                </ul>
            </li>
            <li><a href='#'>Tiendas</a>
                <ul>
                    <li><a href='#'>Crear Tienda</a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li class=''><a href='#'>Productos</a>
        <ul>
            <li><a href="../AddProd.aspx">Alta Producto</a></li>
            <li><a href="../Familias.aspx">Familias</a>
                <ul>
                    <li><a href="../AddFam.aspx">Alta Familia</a></li>
                </ul>
            </li>
        </ul>

    </li>
</ul>
