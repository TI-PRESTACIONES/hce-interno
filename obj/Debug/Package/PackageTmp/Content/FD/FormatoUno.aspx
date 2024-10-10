<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormatoUno.aspx.cs" Inherits="WebApplication1.FormatoUno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 <!DOCTYPE html>
<html>
<body>
    <form id="formactual" runat="server">
    <asp:TextBox ID="txtreceptor" runat="server"></asp:TextBox>
<button onclick="myFunction()">test</button>
<button onclick="obtener()">recepcion...</button>
<!-- w  w w .  j ava  2  s  . co  m-->
<script>
    function myFunctionLL() {
        var x = document.createElement("FORM");
        x.setAttribute("id", "myForm");
        document.body.appendChild(x);

        var y = document.createElement("INPUT");
        y.setAttribute("type", "text");
        y.setAttribute("id", "codigo");
        y.setAttribute("value", "abc");
        document.getElementById("myForm").appendChild(y);

        agregado();

    }
    function agregado() {
        document.getElementById("codigo").value = "Sistemas";

    }
    function getParameter(theParameter) {
        var params = window.location.search.substr(1).split('&');

        for (var i = 0; i < params.length; i++) {
            var p = params[i].split('=');
            if (p[0] == theParameter) {
                return decodeURIComponent(p[1]);
            }
        }
        return false;
    }
    function readCookie(name) {
        var i, c, ca, nameEQ = name + "=";
        ca = document.cookie.split(';');
        for (i = 0; i < ca.length; i++) {
            c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1, c.length);
            }
            if (c.indexOf(nameEQ) == 0) {
                return c.substring(nameEQ.length, c.length);

            }
        }
        return '';
    }
    function eraseCookie(name) {
        createCookie(name, "", -1);
    }

    function obtener() {
        document.getElementById("txtreceptor").value = readCookie('10');
        createCookie("10", "", -1);
    }
    obtener();
</script>

    </form>

</body>
</html>
