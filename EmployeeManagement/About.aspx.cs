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
                tasksLiteral.Text = LoadTasks();
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
            tasksLiteral.Text = LoadTasks();
        }
        protected void btnOpenForm_Click(object sender, EventArgs e)
        {
            string formHtml = GenerateFormHtml();
            formPlaceholder.Controls.Add(new LiteralControl(formHtml));
            btnCreateTask.Visible = true;
            //CreateDynamicButton();
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
        string LoadTasks()
        {
            var db = new DbContext();
            var tasks = db.GetAllTasks();
            StringBuilder tableHtml = new StringBuilder();
            foreach (Task task in tasks)
            {
                tableHtml.Append("<tr>");
                tableHtml.Append($"<td>{task.Id}</td>");
                tableHtml.Append($"<td>{task.Name}</td>");
                tableHtml.Append($"<td>{task.Description}</td>");
                tableHtml.Append($"<td>{task.Status}</td>");
                tableHtml.Append($"<td>{task.DueBy}</td>");
                tableHtml.Append($"<td>{task.Created}</td>");
                tableHtml.Append("<td> <button class='editButton' onclick='btnEditTask'>Edit</button> </td>");
                tableHtml.Append("</tr>");
                
            }
            return tableHtml.ToString();
        }
        protected void btnEditRow_Click(object sender, EventArgs e)
        {
            string formHtml = GenerateFormHtml();
            formPlaceholder.Controls.Add(new LiteralControl(formHtml));
            btnEditTask.Visible = true;
        }
    }
}