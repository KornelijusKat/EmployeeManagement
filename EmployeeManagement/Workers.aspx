<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Workers.aspx.cs" Inherits="EmployeeManagement.Workers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" AutoGenerateColumns="False"  OnRowUpdating="GridView1_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None" Height="718px" Width="1085px">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:TemplateField HeaderText="Edit">
            <ItemTemplate>
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" OnClientClick="GridView1_RowEditing"/>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <%# Eval("Name") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <%# Eval("Description") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Created">
            <ItemTemplate>
                <%# Eval("Created") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxCreated" runat="server" Text='<%# Bind("Created") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DueBy">
            <ItemTemplate>
                <%# Eval("DueBy") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxDueBy" runat="server" Text='<%# Bind("DueBy") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowEditButton="True" />
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <%# GetStatusText(Convert.ToBoolean(Eval("Status"))) %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListStatus" runat="server">
                    <asp:ListItem Text="Active" Value="true"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="false"></asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>
    </p>
</asp:Content>
