<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="Stamp4Fun.UI_Search.SearchResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%--<script language=javascript>
    function OnClickSearchBtn() {
        var s = '<%=GetSearchHttp()%>';
        window.open(s);
    }
</script> 
<asp:ScriptManager ID="ScriptManager1" runat="server"EnablePageMethods="true" /> 
<script type="text/javascript">
    function OnClickSearchBtn() { 
alert(PageMethods.Say());//<SPAN style="COLOR: #ff6666">//注意js中调用后台方法的方式</SPAN> 
}
</script> --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

</asp:Content>
