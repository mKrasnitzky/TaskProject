using System.Collections.Generic;
using System.Linq;
using TaskProject.Interfaces;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text.Json;
using TaskProject.Models;
 
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
            this.fileName = Path.Combine("data", "Tasks.json");
            using (var jsonFile = File.OpenText(fileName))
            {
                tasks = JsonSerializer.Deserialize<List<Task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }


        private void saveToFile()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(tasks));
        }

        public List<Task> GetAll(int userId) {
            List<Task> myTask = new List<Task>{ };
            foreach(Task task in tasks) {
                if(task.UserId == userId)
                    myTask.Add(task);
            }
            return myTask;
        } 
    
        public Task GetById(int id,int userId)
        {
            return tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);
        }
    
        public int Add(Task newTask, int userId)
        {
            if (tasks.Count == 0)
            {
                newTask.Id = 1;
            }
            else
            {
                newTask.Id = tasks.Max(t => t.Id) + 1;
            }
            newTask.UserId = userId;
            tasks.Add(newTask);
            saveToFile();
            return newTask.Id;
        }
    
        public bool Update(int id, Task newTask, int userId)
        {
            if (id != newTask.Id)
                return false;
            var existingTask = GetById(id, userId);
            if (existingTask == null)
                return false;
            if (existingTask.UserId != userId)
                return false;
            var index = tasks.IndexOf(existingTask);
            if (index == -1)
                return false;
            tasks[index]=newTask;
            saveToFile();
            return true;
        }

        public bool Delete(int id, int userId)
        {
            var existingTask = GetById(id, userId);
            if (existingTask == null )
                return false;
            if (existingTask.UserId != userId)
                return false;
            var index = tasks.IndexOf(existingTask);
            if (index == -1 )
                return false;

            tasks.RemoveAt(index);
                        saveToFile();

            return true;
        }

        public void DeleteTasks(int userId) {
            foreach (Task task in tasks) {
                if (task.UserId == userId)
                    tasks.RemoveAt(tasks.IndexOf(task));
            }
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
