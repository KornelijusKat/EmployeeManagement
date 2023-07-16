<%@ Page Title="Tasks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="EmployeeManagement.Tasks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Tasks page</h3>
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1138px" Height="215px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" />
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
                <asp:Calendar ID="TextBoxCreated" runat="server" SelectedDate='<%# Bind("Created") %>'></asp:Calendar>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DueBy">
            <ItemTemplate>
                <%# Eval("DueBy") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Calendar ID="TextBoxDueBy" runat="server" SelectedDate='<%# Bind("DueBy") %>'></asp:Calendar>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <%# GetStatusText(Convert.ToBoolean(Eval("Status"))) %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListStatus" runat="server">
                    <asp:ListItem Text="In Progress" Value="true"></asp:ListItem>
                    <asp:ListItem Text="Complete" Value="false"></asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowEditButton="True" />
        <asp:CommandField ShowDeleteButton="True" />
        <asp:TemplateField>
            <ItemTemplate>
                   <asp:Button ID="btnChangeGrid" runat="server" Text="Assign worker" OnClick="btnChangeGrid_Click" />
            </ItemTemplate>   
        </asp:TemplateField>       
        <asp:ButtonField ButtonType="Button" CommandName="Select" Text="View workers" />
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
    <asp:GridView ID="GridViewWorker" runat="server" DataKeyNames="Id" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="377px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
              <asp:BoundField DataField="Id" HeaderText="ID" />
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <%# Eval("Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Last Name">
            <ItemTemplate>
                <%# Eval("LastName") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnAssignTask" runat="server" Text="Assign Worker" OnClick="btnAssignTask_Click"/>
   <%--             <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />
                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" />--%>
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
      <asp:GridView ID="GridView2" runat="server" DataKeyNames="Id" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
              <asp:BoundField DataField="Id" HeaderText="ID" />
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <%# Eval("Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Last Name">
            <ItemTemplate>
                <%# Eval("LastName") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnUnAssignTask" runat="server" Text="UnAssign Worker" OnClick="btnUnAssignTask_Click"/>
   <%--             <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />
                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" />--%>
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
        <asp:Button ID="btnShowCreateTaskForm" runat="server" Text="Create New Task" OnClick="btnShowCreateTaskForm_Click" />


        <asp:Panel ID="createTaskPanel" runat="server" Visible="false" Height="526px">
            <asp:Label ID="lblName" runat="server" Text="Name" AssociatedControlID="txtName"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" />
            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" Text="Description"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" />
            <asp:Label ID="lblDueBy" runat="server" AssociatedControlID="calendarDueBy" Text="DueBy"></asp:Label>
            <asp:Calendar ID="calendarDueBy" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
            <asp:Label ID="lblCreated" runat="server" AssociatedControlID="calendarCreated" Text="Created"></asp:Label>
            <asp:Calendar ID="calendarCreated" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
            <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreateTask_Click" />
        </asp:Panel>    
</asp:Content>

