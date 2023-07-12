<%@ Page Title="Tasks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EmployeeManagement.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .taskTable {
    width: 100%;
    border-collapse: collapse;
}

.taskTable th, .taskTable td {
    padding: 8px;
    border: 1px solid #ccc;
}

.taskTable th {
    background-color: #f2f2f2;
    font-weight: bold;
}

.taskTable tr:nth-child(even) {
    background-color: #f9f9f9;
}

.taskTable tr:hover {
    background-color: #eaf2ff;
}
</style>
    <asp:Button ID="btnRedirect" runat="server" OnClick="btnRedirect_Click" Text="Redirect To Contact Page" />
    <h2> Title</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <asp:PlaceHolder ID="tasksTablePlaceholder" runat="server">Button</asp:PlaceHolder>
    <asp:Button ID="btnOpenForm" runat="server" Text="Create New Task" OnClick="btnOpenForm_Click" />
     <asp:PlaceHolder ID="formPlaceholder" runat="server"></asp:PlaceHolder>
</asp:Content>