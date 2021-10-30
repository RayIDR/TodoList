using ToDo.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ToDo.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<Task> _tasks;
        static List<Task> Tasks { get; }
        public TaskService(ITodoListDatabaseSettings settings)
        {
            var client = new MongoClient("mongodb+srv://admin:123@cluster0.e0jyn.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var database = client.GetDatabase("TodoListDB");

            _tasks = database.GetCollection<Task>("Tasks");
        }

        public List<Task> Get() => 
            _tasks.Find(task => true).ToList();
        
        public Task Get(string id) => 
            _tasks.Find<Task>(task => task.id == id).FirstOrDefault();
        public Task Add(Task task)
        {
            _tasks.InsertOne(task);
            return task;
        }

        public void Delete(string id)
        {
            _tasks.DeleteOne(task => task.id == id);
        }

        public void Delete(Task taskIn)
        {
            _tasks.DeleteOne(task => task.id == taskIn.id);
        }

        public void Update(string id, Task taskIn)
        {
            _tasks.ReplaceOne(task => task.id == id, taskIn);
        }
    }
}