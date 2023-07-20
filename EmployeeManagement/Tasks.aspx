<%@ Page Title="Tasks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="EmployeeManagement.Tasks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <asp:Panel ID="AllTasksPanel" runat="server">
     <asp:Label ID="Label3" runat="server" CssClass="lblPadding" Text="Current tasks"></asp:Label>
     <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id, Status" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1138px" Height="215px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <%# Eval("Name") %>
            </ItemTemplate>
            <EditItemTemplate>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server"
            ControlToValidate="TextBoxName" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <%# Eval("Description") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:RequiredFieldValidator ID="ValidatorForTaskNameEdit" runat="server"
            ControlToValidate="TextBoxDescription" ErrorMessage="Description is required"></asp:RequiredFieldValidator>
                <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Created">
            <ItemTemplate>
                <%# Eval("Created") %>
            </ItemTemplate>
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
                    <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
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
<asp:Panel  ID="AssignmentContainer" class="AssignmentContainer" style="display: flex; flex-direction: row;" runat="server" Visible="false">
    <asp:Panel ID="allWorkerPanel" CssClass="panels" runat="server" style="display: flex; flex-direction: column;">  
         <asp:Label ID="Label2" CssClass="lblPadding" runat="server" Text="All workers"></asp:Label>
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
        <asp:Label ID="AssignError" visible="false" runat="server" style="color: red;"></asp:Label>
        </asp:Panel>   
    <asp:panel ID="assignedWorkerPanel" CssClass="panels" runat="server" style="display: flex; flex-direction: column;" >
        <asp:Label ID="lblGridViewHeader" runat="server" CssClass="lblPadding" Text="Assigned Workers to Task"></asp:Label>
      <asp:GridView ID="GridView2" runat="server" DataKeyNames="Id" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="377px">
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
          </asp:panel>
  </asp:Panel>
          <div style="margin-top: 20px" >
        <asp:Button ID="btnShowCreateTaskForm" runat="server" Text="Create New Task" OnClick="btnShowCreateTaskForm_Click" />
            </div>
         <asp:Button ID="btnReturnToTasks" runat="server" Text="Return to tasks" visible="false" OnClick="btnReturnToTasks_Click" />   
        <asp:Panel ID="createTaskPanel" runat="server" CssClass="taskPanel" Visible="false" >            
              <div class="formDiv">
                  <asp:RequiredFieldValidator ID="ValidatorForNewTaskName" runat="server"
            ControlToValidate="TxtName" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
            <asp:Label ID="lblName" runat="server" Text="Name" AssociatedControlID="txtName"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" />
                  </div>
              <div class="formDiv">
                  <asp:RequiredFieldValidator ID="ValidatorForNewTaskDescription" runat="server"
            ControlToValidate="txtDescription" ErrorMessage="Description is required"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" Text="Description"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" />
                  </div>
             <div class="formDiv">
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
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
                 </div>
             <div class="btnWrap">
            <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreateTask_Click" />
                 <asp:Button ID="btnCancel" CausesValidation="False" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                 </div>
        </asp:Panel>    
    <style>
.taskPanel {
  margin-top: 20px;
  padding: 20px;
  background-color: #f2f2f2;
  border: 1px solid #ccc;
  border-radius: 4px;
  text-align: left;
  width:fit-content; 
}
.formDiv {
  margin-bottom: 15px;
}

label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
}
.btnWrap {
  justify-content: space-between;
   display: flex;
  align-items: center;
 }
.lblPadding{
    font-size:36px;
}
    </style>
</asp:Content>

