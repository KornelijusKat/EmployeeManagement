using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Workers : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    BindingData();
        //}
        //public string GenerateWorkerForm()
        //{
        //    StringBuilder formHtml = new StringBuilder();
        //    formHtml.Append("<form id='dynamicForm' runat='server'>");
        //    formHtml.Append("<div>");
        //    formHtml.Append("<label for='txtName'>Name:</label>");
        //    formHtml.Append("<input type='text' id='txtname' name='txtName' runat='server' />");
        //    formHtml.Append("</div>");
        //    formHtml.Append("<div>");
        //    formHtml.Append("<label for='txtName'>Last Name:</label>");
        //    formHtml.Append("<input type='text' id='txtLastName' name='txtLastName' runat='server' />");
        //    formHtml.Append("</div>");
        //    formHtml.Append("<input type='submit' value='Submit' name='btnSubmit' onClick='btnSubmit_Click' runat='server' />");
        //    formHtml.Append("</form>");
        //    return formHtml.ToString();
        //}
        ////protected void CreateWorker(object sender, EventArgs e)
        ////{
        ////    string formHtml = GenerateWorkerForm();
        ////    formPlaceholder.Controls.Add(new LiteralControl(formHtml));
        ////    //string name = Request.Form["txtName"];
        ////    //string lastName = Request.Form["txtLastName"];
        ////    //var dbContext = new DbContext();
        ////    //dbContext.CreateWorker(name, lastName);
        ////}
        protected string GetStatusText(bool status)
        {
            return status ? "Complete" : "In Progress";
        }
        //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        //{

        //    //BindingData();
        //}

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{


        //}
        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    GridViewRow row = GridView1.Rows[e.RowIndex];

        //    string id = GridView1.DataKeys[e.RowIndex].Value.ToString(); // Retrieve the ID value from the DataKey

        //    TextBox textBoxName = row.FindControl("TextBoxName") as TextBox;
        //    string name = textBoxName.Text;



        //    var editedTask = new Task() { Name = name};
        //    // Update the data using the retrieved values (e.g., save to database)
        //    var db = new DbContext();
        //    db.EditTask(editedTask);
        //    GridView1.EditIndex = -1; // Exit edit mode
        //    GridView1.DataBind();
        //}
        //protected void BindingData()
        //{
        //    var db = new DbContext();
        //    var list = db.GetAllTasks();
        //    GridView1.DataSource = list;
        //    GridView1.DataBind();

        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
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
            //int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            TextBox textBoxName = row.FindControl("TextBoxName") as TextBox;
            TextBox textBoxDescription = row.FindControl("TextBoxDescription") as TextBox;
            TextBox textBoxCreated = row.FindControl("TextBoxCreated") as TextBox;
            TextBox textBoxDueBy = row.FindControl("TextBoxDueBy") as TextBox;

            // Get the new values from the TextBox controls
            string name = textBoxName.Text;
            string description = textBoxDescription.Text;
            DateTime created = DateTime.Parse(textBoxCreated.Text);
            DateTime dueBy = DateTime.Parse(textBoxDueBy.Text);

            // Update the task in the database
            var db = new DbContext();
            db.EditTask(new Task {/* Id = id,*/ Name = name, Description = description, Created = created, DueBy = dueBy });

            // Exit edit mode
            GridView1.EditIndex = -1;
            BindGridView();
        }
    }
}