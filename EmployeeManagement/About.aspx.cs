using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var db = new DbContext();
                var tasks = db.GetAllTasks();
                string tableHtml = LoadTasks(tasks);
                tasksTablePlaceholder.Controls.Add(new LiteralControl(tableHtml));
            }
        }
        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Contact");
        }
        protected void btnCreateTask_Click(object sender, EventArgs e)
        {
            string name = Request.Form["txtName"];
            string description = Request.Form["txtDescription"];
            bool status = Request.Form["chkStatus"] == "on";
            DateTime dueBy = DateTime.Parse(Request.Form["txtDueBy"]);
            DateTime created = DateTime.Parse(Request.Form["txtCreated"]);
            var dbContext = new DbContext();
            dbContext.CreateTask(name,description,status,dueBy,created);
        }
        protected void btnOpenForm_Click(object sender, EventArgs e)
        {
            string formHtml = GenerateFormHtml();
            formPlaceholder.Controls.Add(new LiteralControl(formHtml));
            CreateDynamicButton();
        }
        public string GenerateFormHtml()
        {
            StringBuilder formHtml = new StringBuilder();
            formHtml.Append("<form id='dynamicForm' runat='server'>");
            formHtml.Append("<div>");
            formHtml.Append("<label for='txtName'>Name:</label>");
            formHtml.Append("<input type='text' id='txtName' name='txtName' runat='server' />");
            formHtml.Append("</div>");
            formHtml.Append("<div>");
            formHtml.Append("<label for='txtDescription'>Description:</label>");
            formHtml.Append("<textarea id='txtDescription' name='txtDescription' runat='server'></textarea>");
            formHtml.Append("</div>");
            formHtml.Append("<div>");
            formHtml.Append("<label for='chkStatus'>Status:</label>");
            formHtml.Append("<input type='checkbox' id='chkStatus' name='chkStatus' runat='server' />");
            formHtml.Append("</div>");
            formHtml.Append("<div>");
            formHtml.Append("<label for='txtDueBy'>Due By:</label>");
            formHtml.Append("<input type='datetime-local' id='txtDueBy' name='txtDueBy' runat='server' />");
            formHtml.Append("</div>");
            formHtml.Append("<div>");
            formHtml.Append("<label for='txtCreated'>Created:</label>");
            formHtml.Append("<input type='datetime-local' id='txtCreated' name='txtCreated' runat='server' />");
            formHtml.Append("</div>");
            formHtml.Append("</form>");

            return formHtml.ToString();
        }
        string LoadTasks(List<Task> tasks)
        {
            StringBuilder tableHtml = new StringBuilder();
            tableHtml.Append("<table class='taskTable'>");
            tableHtml.Append("<tr>");
            tableHtml.Append("<th>ID</th>");
            tableHtml.Append("<th>Name</th>");
            tableHtml.Append("<th>Description</th>");
            tableHtml.Append("<th>Status</th>");
            tableHtml.Append("<th>Due By</th>");
            tableHtml.Append("<th>Created</th>");
            tableHtml.Append("</tr>");
            foreach (Task task in tasks)
            {
                tableHtml.Append("<tr>");
                tableHtml.Append($"<td>{task.Id}</td>");
                tableHtml.Append($"<td>{task.Name}</td>");
                tableHtml.Append($"<td>{task.Description}</td>");
                tableHtml.Append($"<td>{task.Status}</td>");
                tableHtml.Append($"<td>{task.DueBy}</td>");
                tableHtml.Append($"<td>{task.Created}</td>");
                tableHtml.Append("</tr>");
            }
            tableHtml.Append("</table>");
            return tableHtml.ToString();

        }
        private void CreateDynamicButton()
        {
            Button dynamicButton = new Button();
            dynamicButton.ID = "btnProcessData";
            dynamicButton.Text = "Submit";
            dynamicButton.Click += btnCreateTask_Click;
            formPlaceholder.Controls.Add(dynamicButton);
        }
    }
}