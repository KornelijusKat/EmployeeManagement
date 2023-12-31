﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Workers.aspx.cs" Inherits="EmployeeManagement.Workers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="WorkersPanel" runat="server" style="display: flex; flex-direction: row;" Width="1138px" >
        <asp:Panel ID="AllWorkersPanel" runat="server" style="display: flex; flex-direction: column;" >
            <asp:Label ID="WorkersTasksLabel" runat="server" CssClass="lblGrid">Tasks assigned to workers</asp:Label>
            <asp:GridView ID="WorkersTasksGridView" DataKeyNames="Id" runat="server" CssClass="Grids" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ViewStateMode="Enabled"  Width="570px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                      <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />
                 <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# Eval("Name") %>
                    </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                <%# Eval("Description") %>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="DueBy">
                    <ItemTemplate>
                <%# Eval("DueBy", "{0:yyyy-MM-dd}") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                <%# GetStatusText(Convert.ToBoolean(Eval("Status"))) %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnUnassignTask" runat="server" Text="Unassign Task" OnClick="btnUnassignTask_Click" />
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
        <asp:Panel ID="AllTasksPanel" runat="server" style="display: flex; flex-direction: column;" >
            <asp:Label ID="AllTasksLabel" runat="server" CssClass="lblGrid" >All current tasks</asp:Label>
            <asp:GridView ID="AllTasks" DataKeyNames="Id" CssClass="Grids" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="570px"  >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                 <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />
                 <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# Eval("Name") %>
                    </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                <%# Eval("Description") %>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="DueBy">
                    <ItemTemplate>
                <%# Eval("DueBy", "{0:yyyy-MM-dd}") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                <%# GetStatusText(Convert.ToBoolean(Eval("Status"))) %>
                    </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnAssignTask" runat="server" Text="Assign Task" OnClick="btnAssignTask_Click" />
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
    </asp:Panel>
    <p> </p>
    <asp:Button ID="btnGoToTasks" runat="server" Text="Go to Tasks" OnClick="btnGoToTasks_Click"/>
    <asp:Button ID="btnGoToWorkers" runat="server" Text="Go To Workers" OnClick="btnGoToWorkers_Click"/>
    <style>
        .lblGrid{
            font-size: 36px;
        }
        .Grids{
            padding:5px;
        }
     /*   label{
         
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }*/
    </style>
</asp:Content>
