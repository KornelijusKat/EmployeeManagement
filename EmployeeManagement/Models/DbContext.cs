using EmployeeManagement.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Models
{
    public class DbContext
    {
        public string ConnectionString { get; set; }


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(@"server = localhost; port = 3306;user=root; password=test;database=management");
        }
        public void CreateWorker(string name, string lastName)
        {
            using (var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "INSERT INTO workers(name, last_name) Values(@Name, @LastName)";
                MySqlCommand cmd = new MySqlCommand(query,sqlCon);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("last_name", lastName);
                cmd.ExecuteNonQuery();
            }
        }
        public void CreateTask(string name, string description, bool status, DateTime dueBy, DateTime created)
        {
            using (var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "INSERT INTO tasks(name, description,status, due_by, created) Values(@name, @description,@status, @due_by, @created)";
                MySqlCommand cmd = new MySqlCommand(query,sqlCon);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("description", description);
                cmd.Parameters.AddWithValue("status", status);
                cmd.Parameters.AddWithValue("due_by", dueBy);
                cmd.Parameters.AddWithValue("created", created);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Task> GetAllTasks()
        {
            var tasks = new List<Task>();
            using (var sqlCon = GetConnection())
            {            
                sqlCon.Open();
                string query = "SELECT * FROM tasks";
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                  while(reader.Read())
                    {
                        tasks.Add(new Task()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Description = reader.GetString("description"),
                            Created = reader.GetDateTime("created"),
                            DueBy = reader.GetDateTime("due_by"),
                            Status = reader.GetBoolean("status")
                        }) ;
                    }
                }
            }
            return tasks;
        }
    }
}