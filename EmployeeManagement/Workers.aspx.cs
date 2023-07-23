using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Workers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["recordId"] != null && int.TryParse(Request.QueryString["recordId"], out int recordId))
                {
                    BindWorkersTasks(recordId);
                    BindAllTasks();
                }
                else
                {
                    Response.Redirect("Tasks.aspx");
                }             
            }
        }
        protected void BindAllTasks()
        {
            DbContext db = new DbContext();
            int workerId = int.Parse(Request.QueryString["recordId"]);
            AllTasks.DataSource = db.GetAllFreeTasks(workerId);
            AllTasks.DataBind();
        }
        protected void BindWorkersTasks(int id)
        {
            DbContext db = new DbContext();
            var list = db.GetAllTasksOfWorker(id);
            WorkersTasksGridView.DataSource = list;
            WorkersTasksGridView.DataBind();
        }
        protected string GetStatusText(bool status)
        {
            return status ? "In Progress" : "Complete";
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tasks.aspx");
        }

        protected void btnAssignTask_Click(object sender, EventArgs e)
        {
            Button btnAssign = (Button)sender;
            GridViewRow clickedRow = (GridViewRow)btnAssign.NamingContainer;
            DbContext db = new DbContext();
            int taskKey = Convert.ToInt32(AllTasks.DataKeys[clickedRow.RowIndex].Value);
            if(Session["SelectedWorkerKey"] != null)
            {
                int workerKey = Convert.ToInt32(Session["SelectedWorkerKey"]);
                var response = db.WorkerToTask(taskKey, workerKey);
                WorkersTasksGridView.DataBind();
                BindWorkersTasks(workerKey);
                Session["WorkersGridData"] = WorkersTasksGridView.DataSource;
                Response.Redirect("Workers.aspx?recordId=" + workerKey);
            }
        }

        protected void btnUnassignTask_Click(object sender, EventArgs e)
        {
            var db = new DbContext();
            Button btnUnAssignTask = (Button)sender;
            GridViewRow clickedRow = (GridViewRow)btnUnAssignTask.NamingContainer;
            int taskDataKey = Convert.ToInt32(WorkersTasksGridView.DataKeys[clickedRow.RowIndex].Value);;
            if (Session["SelectedWorkerKey"] != null)
            {
                int dataKey = Convert.ToInt32(Session["SelectedWorkerKey"]);
                db.DeleteWorkerTaskPair(taskDataKey, dataKey);
                BindWorkersTasks(dataKey);
                BindAllTasks();
            }
        }
    }
}