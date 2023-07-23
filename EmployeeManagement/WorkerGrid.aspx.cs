using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class WorkerGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAllWorkers();
            }
        }
        private void BindAllWorkers()
        {
            var db = new DbContext();
            WorkersGrid.DataSource = db.GetAllWorkers();
            WorkersGrid.DataBind();
        }

        protected void btnShowTasks_Click(object sender, EventArgs e)
        {
            Button showTask = (Button)sender;
            var clickedRow = (GridViewRow)showTask.NamingContainer;
            var dataKey = WorkersGrid.DataKeys[clickedRow.RowIndex].Value;
            Session["SelectedWorkerKey"] = dataKey;
            Response.Redirect("Workers.aspx?recordId=" + dataKey.ToString());
        }

        protected void WorkersGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            WorkersGrid.EditIndex = e.NewEditIndex;
            BindAllWorkers();
        }

        protected void WorkersGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var row = WorkersGrid.Rows[e.RowIndex];
            var dataKey = Convert.ToInt32(WorkersGrid.DataKeys[row.RowIndex].Value);
            TextBox textBoxName = row.FindControl("txtBoxName") as TextBox;
            TextBox TextBoxLastName = row.FindControl("txtBoxLastName") as TextBox;
            var editedWorker = new Worker() { Id = dataKey, Name = textBoxName.Text, LastName = TextBoxLastName.Text };
            var db = new DbContext();
            db.EditWorker(editedWorker);
            WorkersGrid.EditIndex = -1;
            BindAllWorkers();
        }

        protected void WorkersGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = WorkersGrid.Rows[e.RowIndex];
            var dataKey = Convert.ToInt32(WorkersGrid.DataKeys[row.RowIndex].Value);
            var db = new DbContext();
            db.DeleteWorker(dataKey);
            WorkersGrid.EditIndex = -1;
            BindAllWorkers();
        }

        protected void WorkersGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            WorkersGrid.EditIndex = -1;
            BindAllWorkers();
        }

        protected void btnCreateWorker_Click(object sender, EventArgs e)
        {
            var db = new DbContext();
            db.CreateWorker(txtWorkerName.Text, txtWorkerLastName.Text);
            createWorkerPanel.Visible = false;
            btnShowForm.Visible = true;
            BindAllWorkers();
        }

        protected void btnCancelWorker_Click(object sender, EventArgs e)
        {
            btnShowForm.Visible = true;
            createWorkerPanel.Visible = false;
        }

        protected void btnShowForm_Click(object sender, EventArgs e)
        {
            createWorkerPanel.Visible = true;
            btnShowForm.Visible = false;
        }
    }
}