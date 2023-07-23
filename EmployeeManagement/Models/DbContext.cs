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
        public void EditTask(Task task)
        {
            using(var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "UPDATE tasks SET";
                var parameters = new List<MySqlParameter>();
                query += " status = @status,";
                int tinyInt = task.Status ? 1 : 0;
                parameters.Add(new MySqlParameter("@status", tinyInt));
                if (!string.IsNullOrEmpty(task.Name))
                {
                    query += " name = @name,";
                    parameters.Add(new MySqlParameter("@name", task.Name));
                }          
                if (task.Description != null)
                {
                    query += " description = @description,";
                    parameters.Add(new MySqlParameter("@description", task.Description));
                }
                if (task.DueBy != DateTime.MinValue)
                {
                    query += " due_by = @dueBy,";
                    parameters.Add(new MySqlParameter("@dueBy", task.DueBy));
                }
                if(task.Created != DateTime.MinValue)
                {
                    query += " created = @created,";
                    parameters.Add(new MySqlParameter("@created", task.Created));
                }
                query = query.TrimEnd(',');
                query += " WHERE Id = @Id";
                parameters.Add(new MySqlParameter("@Id", task.Id));
                using (var cmd = new MySqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    cmd.ExecuteNonQuery();
                }
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
        public List<Worker> GetAllWorkersByTaskID(int taskId)
        {
            var workers = new List<Worker>();
            using(var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "SELECT DISTINCT workers.id,workers.name, workers.last_name From workers INNER JOIN workertask ON workertask.worker_id = id WHERE workertask.task_id = @taskId";
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("taskId", taskId);
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        workers.Add(new Worker()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            LastName = reader.GetString("last_name")
                        });
                    }
                }
            }
            return workers;
        }
        public List<Worker> GetAllWorkers()
        {
            var workers = new List<Worker>();
            using(var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "SELECT * FROM workers";
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        workers.Add(new Worker()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            LastName = reader.GetString("last_name")
                        });
                    }
                }
            }
            return workers;
        }
        public void DeleteTask(int Id)
        {
            using(var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "DELETE tasks, workertask FROM tasks LEFT JOIN workertask ON workertask.task_id = tasks.id WHERE tasks.id = @ID";
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Worker> GetAllFreeWorkers(int taskId)
        {
            var workers = new List<Worker>();
            using (var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "SELECT DISTINCT id, name, last_name FROM workers WHERE NOT EXISTS (SELECT 1 FROM workertask WHERE worker_id = workers.id AND task_id = @taskId)"; ;
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("taskId", taskId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workers.Add(new Worker()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            LastName = reader.GetString("last_name")
                        });
                    }
                }
            }
            return workers;
        }
        public List<Task> GetAllFreeTasks(int workerId)
        {
            var tasks = new List<Task>();
            using (var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "SELECT DISTINCT tasks.id, tasks.name, tasks.description, tasks.created, tasks.due_by, tasks.status FROM tasks" +
                                " WHERE NOT EXISTS(SELECT 1 FROM workertask WHERE task_id = tasks.id AND worker_id = @workerId)";
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@workerId", workerId);
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Description = reader.GetString("description"),
                            Created = reader.GetDateTime("created"),
                            DueBy = reader.GetDateTime("due_by"),
                            Status = reader.GetBoolean("status")
                        });
                    }
                }
            }
            return tasks;
        }
        public bool WorkerToTask(int taskId, int workerId)
        {
            using (var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query ="INSERT INTO workertask (worker_id, task_id) SELECT @workerId, @taskId WHERE NOT EXISTS (SELECT 1 FROM workertask WHERE worker_id = @workerId AND task_id = @taskId)";
                using (var cmd = new MySqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@workerId", workerId);
                    int commitedAction = cmd.ExecuteNonQuery();
                    return commitedAction > 0;
                }
            }
        }
        public void DeleteWorkerTaskPair(int taskId, int workerId)
        {
            using (var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "DELETE FROM management.workertask WHERE worker_id = @workerId AND task_id = @taskId";
                using (var cmd = new MySqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@workerId", workerId);
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Task> GetAllTasksOfWorker(int Id)
        {
            var taskList = new List<Task>();
            using (var sqlCon = GetConnection())
            {
                sqlCon.Open();
                string query = "SELECT DISTINCT tasks.id, tasks.name, tasks.description, tasks.created, tasks.due_by, tasks.status FROM tasks INNER JOIN workertask on workertask.task_id = tasks.id WHERE workertask.worker_id = @workerId";
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("workerId", Id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taskList.Add(new Task()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Description = reader.GetString("description"),
                            Created = reader.GetDateTime("created"),
                            DueBy = reader.GetDateTime("due_by"),
                            Status = reader.GetBoolean("status")
                        });
                    }
                }
            }
            return taskList;
        }
    }
}