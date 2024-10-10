

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>



<!DOCTYPE html>

<html>
<head runat="server">
    <title>Advanced Password Mask - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />
   
</head>
    <script>
        function InsertRow(valor) {
            
            if (valor.getValue() != '') {
              
                    var obj = JSON.parse(valor.getValue());
                    var DescripcionLocalVar = obj.DescripcionLocal;
                    var ValorEntero4Var = obj.ValorEntero4;
                
                    var nombre = document.getElementById('SubFamilia')

                    nombre.setValue(DescripcionLocalVar);
                 
                                       
                  
                    valor.setValue('');
                }
            }
        


        function test(txt) {
            alert(txt.value);
        };

    </script>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <h1>Advanced Password Mask</h1>

        <p>PasswordMask allows :</p>
        <ul>
            <li>Generate a strong password by pattern</li>
            <li>Calculate strength of the entered password and mark the field as invalid if the strength is not enough</li>
            <li>Check that the password is not in the black list (for passwords like "qwerty")</li>
            <li>Show/Hide password</li>
            <li>Check that password doesn't equal the login</li>
        </ul>
        

        
           
       <asp:TextBox ID="RecepcionaRecurso" runat="server" onchange="InsertRow(this);" visible="false"></asp:TextBox> 


        <input id="SubFamilia" runat="server"/>     

   </form>
</body>
</html>
