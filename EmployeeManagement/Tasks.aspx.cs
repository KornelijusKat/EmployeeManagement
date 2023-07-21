using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Tasks : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }
        protected bool preSelectedBool
        {
            get { return ViewState["PreSelectedBool"] != null ? (bool)ViewState["PreSelectedBool"] : true; }
            set { ViewState["PreSelectedBool"] = value; }
        }
        protected DateTime preEditCreateDate
        {
            get { return (DateTime)ViewState["PreEditDate"]; }
            set { ViewState["PreEditDate"] = value; }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            AllTasksGridView.EditIndex = e.NewEditIndex;
            BindGridView();
        }
        private void BindGridView()
        {
            var db = new DbContext();
            AllTasksGridView.DataSource = db.GetAllTasks();
            AllTasksGridView.DataBind();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = AllTasksGridView.Rows[e.RowIndex];
            int id = Convert.ToInt32(AllTasksGridView.DataKeys[e.RowIndex].Value);
            TextBox textBoxName = row.FindControl("TextBoxName") as TextBox;
            TextBox textBoxDescription = row.FindControl("TextBoxDescription") as TextBox;
            Calendar calDueBy = row.FindControl("CalendarDueBy") as Calendar;
            DropDownList dropDownList = row.FindControl("DropDownListStatus") as DropDownList;
            string name = textBoxName.Text;
            string description = textBoxDescription.Text;
            DateTime dueBy = calDueBy.SelectedDate;
            if(dueBy < preEditCreateDate)
            {
                dueBy = DateTime.MinValue;
            }
            bool preEditStatus = preSelectedBool;
            bool status;
            if (dropDownList.SelectedValue == "")
            {
                status = preEditStatus;
            }
            else
            {
                status = bool.Parse(dropDownList.SelectedValue);
            }
            var db = new DbContext();
            db.EditTask(new Task { Id = id, Name = name, Description = description, DueBy = dueBy, Status = status });
            AllTasksGridView.EditIndex = -1;
            BindGridView();
        }
        protected string GetStatusText(bool status)
        {
            return status ? "In Progress" : "Complete" ;
        }
        protected void btnShowCreateTaskForm_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            createTaskPanel.Visible = true;
            btnShowCreateTaskForm.Visible = false;
        }
        protected void btnCreateTask_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string description = txtDescription.Text;
            DateTime dueBy = calendarDueBy.SelectedDate;
            DateTime created = DateTime.Today;
            if(dueBy < created)
            {
                lblError.Text = "Please select date, due date cannot be earlier than today";
                lblError.Visible = true;
                return;
            }
            var db = new DbContext();
            db.CreateTask(name, description, true, dueBy, created);
            BindGridView();
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            createTaskPanel.Visible = false;
            btnShowCreateTaskForm.Visible = true;
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(AllTasksGridView.DataKeys[e.RowIndex].Value);
            var db = new DbContext();
            db.DeleteTask(id);
            AllTasksGridView.EditIndex = -1;
            BindGridView();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            AllTasksGridView.EditIndex = -1;
            BindGridView();
        }
        private void BindWorkersToGrid()
        {
            var db = new DbContext();
            GridViewWorker.DataSource = db.GetAllWorkers();
            GridViewWorker.DataBind();
        }
        private void BindAssignedWorkersToGrid(int taskId)
        {
            var db = new DbContext();
            WorkersToTaskGridView.DataSource = db.GetAllWorkersByTaskID(taskId);
            WorkersToTaskGridView.DataBind();
        }
        protected void btnAssignTask_Click(object sender, EventArgs e)
        {
            Button btnAssignTask = (Button)sender;
            GridViewRow clickedRow = (GridViewRow)btnAssignTask.NamingContainer;
            int dataKey = Convert.ToInt32(GridViewWorker.DataKeys[clickedRow.RowIndex].Value);
            if (Session["SelectedDataKey"] != null)
            {
                int taskDataKey = Convert.ToInt32(Session["SelectedDataKey"]);
                var db = new DbContext();
                var response = db.WorkerToTask(taskDataKey, dataKey);
                if (!response)
                {
                    AssignError.Text = "Task has already been assigned to worker";
                    AssignError.Visible = true;
                    return;
                }
                AssignError.Visible = false;
                BindAssignedWorkersToGrid(taskDataKey);
            }
        }
        protected void btnChangeGrid_Click(object sender, EventArgs e)
        {
            AllTasksPanel.Visible = false;
            btnShowCreateTaskForm.Visible = false;
            btnReturnToTasks.Visible = true;
            Button btnAssignTask = (Button)sender;
            GridViewRow clickedRow = (GridViewRow)btnAssignTask.NamingContainer;
            int dataKey = Convert.ToInt32(AllTasksGridView.DataKeys[clickedRow.RowIndex].Value);
            Session["SelectedDataKey"] = dataKey;
            AssignmentContainer.Visible = true;
            BindWorkersToGrid();
            BindAssignedWorkersToGrid(dataKey);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && AllTasksGridView.EditIndex == e.Row.RowIndex)
            {
                bool preEditStatus = (bool)AllTasksGridView.DataKeys[e.Row.RowIndex]["Status"];
                preEditCreateDate = (DateTime)AllTasksGridView.DataKeys[e.Row.RowIndex]["Created"];
                preSelectedBool = preEditStatus;
            }         
        }
        protected void btnUnAssignTask_Click(object sender, EventArgs e)
        {
            var db = new DbContext();
            Button btnUnAssignTask = (Button)sender;
            GridViewRow clickedRow = (GridViewRow)btnUnAssignTask.NamingContainer;
            int dataKey = Convert.ToInt32(WorkersToTaskGridView.DataKeys[clickedRow.RowIndex].Value);
            if (Session["SelectedDataKey"] != null)
            {
                int taskDataKey = Convert.ToInt32(Session["SelectedDataKey"]);
                db.DeleteWorkerTaskPair(taskDataKey, dataKey);
                BindAssignedWorkersToGrid(taskDataKey);
            }
        }
        protected void btnReturnToTasks_Click(object sender, EventArgs e)
        {
            AssignmentContainer.Visible = false;
            btnReturnToTasks.Visible = false;
            AllTasksPanel.Visible = true;
            btnShowCreateTaskForm.Visible = true;
        }
        protected void btnViewWorkersTask_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            createTaskPanel.Visible = false;
            btnShowCreateTaskForm.Visible = true;
        }
    }
}