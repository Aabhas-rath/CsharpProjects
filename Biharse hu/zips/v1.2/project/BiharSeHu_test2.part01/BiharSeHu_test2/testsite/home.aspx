<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="testsite.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        name :<asp:TextBox ID="name" runat="server"></asp:TextBox>
        <br />
        display name :<asp:TextBox ID="displayname" runat="server"></asp:TextBox>
        <br />
        username <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        password :<asp:TextBox ID="password" runat="server"></asp:TextBox>
        <br />
        email :<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        dob :<asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <br />
        is Author :<asp:CheckBox ID="CheckBox1" runat="server" />

        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />

    </div>
    </form>
</body>
</html>
