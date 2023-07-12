using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Workers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GenerateWorkerForm()
        {
            StringBuilder formHtml = new StringBuilder();
            formHtml.Append("<form id='dynamicForm' runat='server'>");
            formHtml.Append("<div>");
            formHtml.Append("<label for='txtName'>Name:</label>");
            formHtml.Append("<input type='text' id='txtname' name='txtName' runat='server' />");
            formHtml.Append("</div>");
            formHtml.Append("<div>");
            formHtml.Append("<label for='txtName'>Last Name:</label>");
            formHtml.Append("<input type='text' id='txtLastName' name='txtLastName' runat='server' />");
            formHtml.Append("</div>");
            formHtml.Append("<input type='submit' value='Submit' name='btnSubmit' onClick='btnSubmit_Click' runat='server' />");
            formHtml.Append("</form>");
            return formHtml.ToString();
        }
        //protected void CreateWorker(object sender, EventArgs e)
        //{
        //    string formHtml = GenerateWorkerForm();
        //    formPlaceholder.Controls.Add(new LiteralControl(formHtml));
        //    //string name = Request.Form["txtName"];
        //    //string lastName = Request.Form["txtLastName"];
        //    //var dbContext = new DbContext();
        //    //dbContext.CreateWorker(name, lastName);
        //}
    }
}