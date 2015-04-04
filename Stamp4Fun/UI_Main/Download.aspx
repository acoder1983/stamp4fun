<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="Stamp4Fun.UI_Main.Download" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<p>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Version/目录客户端.zip" Font-Size=Large>目录客户端.zip</asp:HyperLink>
    </p>
    <p>
        <span style="color: rgb(70, 70, 70); font-family: simsun; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 21px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
        下载软件包后，将软件置于固定目录下，双击“目录客户端.exe”，如果出现</span></p>
    <p>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resource/dotneterr.jpg" />
    </p>
    <p>
        <span style="color: rgb(70, 70, 70); font-family: simsun; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 21px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
        请安装微软.net 2.0 </span>
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="~/Version/NetFx20SP2_x86.exe" Font-Size=Large>NetFx20SP2_x86.exe</asp:HyperLink>
    </p>
    <asp:HyperLink ID="HyperLink4" runat="server" 
            NavigateUrl="http://tieba.baidu.com/p/3090253240?pid=51787201244&cid=0#51787201244" Font-Size=Large>使用说明</asp:HyperLink>
</asp:Content>
