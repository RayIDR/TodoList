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
        static int nextId = 3;
        public TaskService(ITodoListDatabaseSettings settings)
        {
            // Tasks = new List<Task>
            // {
            //     new Task { id = 1, title = "Wash the dishes", completed = false },
            //     new Task { id = 2, title = "Wipe the floor", completed = true }
            // };
            var client = new MongoClient("mongodb://127.0.0.1:27017/?readPreference=primary&serverSelectionTimeoutMS=2000&appname=MongoDB%20Compass&directConnection=true&ssl=false");
            var database = client.GetDatabase("TodoListDB");

            _tasks = database.GetCollection<Task>("Tasks");
        }

        // public static List<Task> GetAll() => Tasks;

        // public static Task Get(int id) => Tasks.FirstOrDefault(p => p.id == id);

        public List<Task> Get() => 
            _tasks.Find(task => true).ToList();
        
        public Task Get(string id) => 
            _tasks.Find<Task>(task => task.id == id).FirstOrDefault();
        public Task Add(Task task)
        {
            // task.id = nextId++;
            // Tasks.Add(task);
            _tasks.InsertOne(task);
            return task;
        }

        public void Delete(string id)
        {
            // var task = Get(id);
            // if(task is null)
            //     return;

            // Tasks.Remove(task);
            _tasks.DeleteOne(task => task.id == id);
        }

        public void Delete(Task taskIn)
        {
            _tasks.DeleteOne(task => task.id == taskIn.id);
        }

        public void Update(string id, Task taskIn)
        {
            // var index = Tasks.FindIndex(p => p.id == task.id);
            // if(index == -1)
            //     return;

            // Tasks[index] = task;
            _tasks.ReplaceOne(task => task.id == id, taskIn);
        }
    }
}