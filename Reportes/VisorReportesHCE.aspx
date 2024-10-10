<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorReportesHCE.aspx.cs" Inherits="AppSaludMVC.Reportes.VisorReportesHCE" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #dialogzone {
            overflow: hidden;
            position: relative;
            width: 100%;    


        }
    </style>
     <script type="text/javascript">
       function Print() {
       var dvReport = document.getElementById("dvReport");
       var frame1 = dvReport.getElementsByTagName("iframe")[0];
       if (navigator.appName.indexOf("Internet Explorer") != -1) {
        frame1.name = frame1.id;
        window.frames[frame1.id].focus();
        window.frames[frame1.id].print();
       }
       else
       {
        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
        frameDoc.print();
       }
       }


    </script>
    
 <%--   <a href="VisorReportesHCE.aspx">VisorReportesHCE.aspx</a>--%>


</head>
<body>
<form id="form1" runat="server" style="width:100%;height:100%" >
    <div id="dvReport">
         <%--<input type="button" id="btnPrint" value="Print" onclick="Print()" />--%>
   <asp:Button ID="Button1" runat="server" Text="Exportar PDF" OnClick="Button1_Click" Visible="false" />
    <CR:CrystalReportViewer ID="ReportViewer" runat="server" AutoDataBind="True" EnableDatabaseLogonPrompt="False"
        
	    EnableParameterPrompt="False" HasCrystalLogo="False"   
	    HasDrillUpButton="False" HasToggleGroupTreeButton="False"  
	    HasSearchButton="False" EnableDrillDown="False" 
	    Height="550px"  Width="550px" ToolPanelView="None"  PrintMode="ActiveX" ViewStateMode="Enabled"
        
        />	
 
    </div>
</form>
   
</body>
</html>
 