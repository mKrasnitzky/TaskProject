using System.Collections.Generic;
using System.Linq;
using TaskProject.Interfaces;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text.Json;
 
// using System.Threading;

namespace TaskProject.Services
{
    using TaskProject.Models;

    public class TaskService : ITaskService
    {
        private List<Task> tasks;
        private string fileName = "Tasks.json";
    
        public TaskService()
        {
            this.fileName = Path.Combine("Data", "Tasks.json");
            using (var jsonFile = File.OpenText(fileName))
            {
                tasks = JsonSerializer.Deserialize<List<Task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    
        public List<Task> GetAll() => tasks;
    
        public Task GetById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }
    
        public int Add(Task newTask)
        {
            if (tasks.Count == 0)
            {
                newTask.Id = 1;
            }
            else
            {
                newTask.Id = tasks.Max(t => t.Id) + 1;
            }
            tasks.Add(newTask);
    
            return newTask.Id;
        }
    
        public bool Updata(int id, Task newTask)
        {
            if (id != newTask.Id)
                return false;
            var existingTask = GetById(id);
            if (existingTask == null)
                return false;
            
            var index = tasks.IndexOf(existingTask);
            if (index == -1)
                return false;
            tasks[index]=newTask;
            return true;
        }

        public bool Delete(int id)
        {
            var existingTask = GetById(id);
            if (existingTask == null )
                return false;

            var index = tasks.IndexOf(existingTask);
            if (index == -1 )
                return false;

            tasks.RemoveAt(index);
            return true;
        }  
    }

    public static class TaskUtils
    {
        public static void AddTask(this IServiceCollection services)
        {
            services.AddSingleton<ITaskService, TaskService>();
        }
    }

}
