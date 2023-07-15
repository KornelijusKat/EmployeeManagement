using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
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
                //BindWorkersToGrid();
             
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }
        private void BindGridView()
        {
            var db = new DbContext();
            GridView1.DataSource = db.GetAllTasks();
            GridView1.DataBind();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            TextBox textBoxName = row.FindControl("TextBoxName") as TextBox;
            TextBox textBoxDescription = row.FindControl("TextBoxDescription") as TextBox;
            Calendar calCreated = row.FindControl("TextBoxCreated") as Calendar;
            //TextBox textBoxCreated = row.FindControl("TextBoxCreated") as TextBox;
            Calendar calDueBy = row.FindControl("TextBoxDueBy") as Calendar;
            DropDownList dropDownList = row.FindControl("DropDownListStatus") as DropDownList;
            // Get the new values from the TextBox controls
            string name = textBoxName.Text;
            string description = textBoxDescription.Text;
            DateTime created = calCreated.SelectedDate;
            DateTime dueBy = calDueBy.SelectedDate;
            bool status =bool.Parse(dropDownList.SelectedValue);
            // Update the task in the database
            var db = new DbContext();
            db.EditTask(new Task { Id = id, Name = name, Description = description, Created = created, DueBy = dueBy, Status = status });

            // Exit edit mode
            GridView1.EditIndex = -1;
            BindGridView();
        }
        protected string GetStatusText(bool status)
        {
            return status ? "Complete" : "In Progress";
        }
        protected void btnShowCreateTaskForm_Click(object sender, EventArgs e)
        {
            createTaskPanel.Visible = true;
        }
        protected void btnCreateTask_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string description = txtDescription.Text;
            DateTime dueBy = calendarDueBy.SelectedDate;
            DateTime created = calendarCreated.SelectedDate;
            var db = new DbContext();
            db.CreateTask(name, description, false, dueBy, created);
            BindGridView();
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            calendarCreated.SelectedDate = DateTime.Today;
            calendarCreated.SelectedDate = DateTime.Today;
            createTaskPanel.Visible = false;
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            var db = new DbContext();
            db.DeleteTask(id);
            GridView1.EditIndex = -1;
            BindGridView();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }
        private void BindWorkersToGrid()
        {
            var db = new DbContext();
            GridViewWorker.DataSource = db.GetAllWorkers();
            GridViewWorker.DataBind();
        }

        protected void btnAssignTask_Click(object sender, EventArgs e)
        {
            //GridViewRow selectedRow = GridViewWorker.SelectedRow;
            //int dataKey = Convert.ToInt32(GridViewWorker.DataKeys[selectedRow.RowIndex].Value);
            Button btnAssignTask = (Button)sender;
            GridViewRow clickedRow = (GridViewRow)btnAssignTask.NamingContainer;
            int dataKey = Convert.ToInt32(GridViewWorker.DataKeys[clickedRow.RowIndex].Value);
            if (Session["SelectedDataKey"] != null)
            {
                int taskDataKey = Convert.ToInt32(Session["SelectedDataKey"]);
                var db = new DbContext();
                db.WorkerToTask(taskDataKey, dataKey);
            }

        }

        protected void btnChangeGrid_Click(object sender, EventArgs e)
        {
            GridViewRow selectedRow = GridView1.SelectedRow;
            int dataKey = Convert.ToInt32(GridView1.DataKeys[selectedRow.RowIndex].Value);
            Session["SelectedDataKey"] = dataKey;
            GridView1.Visible = false;
            GridViewWorker.Visible = true;
            BindWorkersToGrid();
        }

        protected void ddlWorkers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}