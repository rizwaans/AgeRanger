<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="AR.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="container">
            <h1 class="text-capitalize text-center">AgeRanger</h1>
            <div class="table-responsive">
                <table class="table table-bordered table-striped">
                    <tr>
                        <th>First Name:</th>
                        <td>
                            <asp:TextBox ID="txt_fName" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>Last Name:</th>
                        <td>
                            <asp:TextBox ID="txt_lname" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>Age:</th>
                        <td>
                            <asp:TextBox ID="txt_age" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>

                   
                    
                    <tr>
                        
                        <td class="text-right" colspan="2"><asp:Label ID="lbl_txt" runat="server" CssClass="label-warning" /><asp:Button ID="btn_clear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btn_clear_Click" />
                        <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" CssClass="btn btn-primary" />
                            <asp:Button ID="btn_search" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btn_search_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:GridView ID="Grid" runat="server" CssClass="table table-bordered table-striped" OnRowDeleting="Grid_RowDeleting" OnRowEditing="Grid_RowEditing" OnRowCancelingEdit="Grid_RowCancelingEdit" OnRowUpdating="Grid_RowUpdating" OnSelectedIndexChanged="Grid_SelectedIndexChanged">
                <Columns>
                     
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
