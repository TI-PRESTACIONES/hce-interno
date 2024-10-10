<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebReportesView.aspx.cs" Inherits="AppSaludMVC.Reportes.WebReportesView" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Exportar PDF" OnClick="Button1_Click"  Visible="false" />
        <CR:CrystalReportViewer ID="visorReportes" runat="server"
            AutoDataBind="true" GroupTreeStyle-ShowLines="False"
            HasSearchButton="False" HasCrystalLogo="False"
            ReuseParameterValuesOnRefresh="true"
            OnNavigate="CrystalReportViewer1_Navigate" HasGotoPageButton="True"
            HasZoomFactorList="False"
            HasDrillUpButton="False" HasToggleGroupTreeButton="False"
            ToolPanelView="None" HasToggleParameterPanelButton="False" />
    </div>
    </form>
</body>
</html>

