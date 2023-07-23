<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkerGrid.aspx.cs" Inherits="EmployeeManagement.WorkerGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="AllWorkerPanel" runat="server" Width="800px">
        <asp:Label ID="lblWorkerGrid" runat="server" CssClass="lblWorkers">Employed Workers</asp:Label>
        <asp:GridView ID="WorkersGrid" runat="server"  AutoGenerateColumns="False" OnRowEditing="WorkersGrid_RowEditing" OnRowUpdating="WorkersGrid_RowUpdating" OnRowDeleting="WorkersGrid_RowDeleting" OnRowCancelingEdit="WorkersGrid_RowCancelingEdit" CellPadding="4" ForeColor="#333333" GridLines="None" Width="705px" DataKeyNames="Id">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Id"  HeaderText="ID" ReadOnly="true" />
                <asp:TemplateField HeaderText="Name" runat="server">
                    <ItemTemplate>
                        <%# Eval("Name") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBoxName" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                           <asp:TextBox ID="txtBoxName" runat="server" Text='<%# Bind("Name")%>'></asp:TextBox>                      
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LastName" runat="server">
                    <ItemTemplate>
                        <%# Eval("LastName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBoxLastName" ErrorMessage="Last Name is required"></asp:RequiredFieldValidator>   
                            <asp:TextBox ID="txtBoxLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                   <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />            
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Assign Tasks" OnClick="btnShowTasks_Click" />
                    </ItemTemplate>
                </asp:TemplateField>               
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </asp:Panel>
    <asp:Button ID="btnShowForm" Text="Create Worker" runat="server" OnClick="btnShowForm_Click"/>
    <asp:Panel ID="createWorkerPanel" runat="server" CssClass="workerPanel" Visible="false">
    <div class="formDiv">
        <asp:RequiredFieldValidator ID="ValidatorForNewWorkerName" runat="server"
            ControlToValidate="txtWorkerName" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
        <asp:Label ID="lblWorkerName" runat="server" Text="Name" AssociatedControlID="txtWorkerName"></asp:Label>
        <asp:TextBox ID="txtWorkerName" runat="server"></asp:TextBox>
    </div>
    <div class="formDiv">
        <asp:RequiredFieldValidator ID="ValidatorForNewWorkerLastName" runat="server"
            ControlToValidate="txtWorkerLastName" ErrorMessage="Last Name is required"></asp:RequiredFieldValidator>
        <asp:Label ID="lblWorkerLastName" runat="server" AssociatedControlID="txtWorkerLastName" Text="Last Name"></asp:Label>
        <asp:TextBox ID="txtWorkerLastName" runat="server"></asp:TextBox>
    </div>
    <div class="btnWrap">
        <asp:Button ID="btnCreateWorker" runat="server" Text="Create Worker" OnClick="btnCreateWorker_Click" />
        <asp:Button ID="btnCancelWorker" CausesValidation="False" runat="server" Text="Cancel" OnClick="btnCancelWorker_Click" />
    </div>
</asp:Panel>
    <style>
        .formDiv {
        margin-bottom: 5px;
        display: flex;
        align-items: flex-start
    }

    .formDiv label {
        width: 50px;
        margin-right: 10px;
    }

    .formDiv input[type="text"] {
        flex: 1;
        padding: 5px;
    }

    .btnWrap {
        display: flex;
        justify-content:space-evenly;
    }

    .workerPanel {
        margin-top: 20px;
        background-color: #f2f2f2;
        border: 1px solid #ccc;
        border-radius: 4px;
        text-align: left;
        width: 300px;
    }
        .lblWorkers{
            font-size: 36px;
        }
    </style>
</asp:Content>
