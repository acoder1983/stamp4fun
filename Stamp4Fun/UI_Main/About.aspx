<%@ Page Title="关于我们" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="Stamp4Fun.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        
    </h2>
    <p>
        这是一个永久非盈利网站。基本内容来自Scott邮票目录，我们“盗版”但不侵权。
    </p>
    <p>
    本站旨在为外邮爱好者提供一个信息汇集，交流的平台，欢迎大家成为目录编辑。
    </p>
    <p>
        &nbsp;</p>
    <p>
        编写者</p>
    <p>
        <asp:Table ID="tableEditors" runat="server">
        </asp:Table>
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
