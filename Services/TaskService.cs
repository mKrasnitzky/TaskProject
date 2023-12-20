using System.Collections.Generic;
using System.Linq;
// using System.Threading;

namespace MyList.Services{
using MyList.Models;

    public static class TaskService
    {
        private static List<Task> tasks;
    
        static TaskService()
        {
            tasks = new List<Task>
            {
                new Task { Id = 1, Profession = "node.js", Description = "to finish"},
                new Task { Id = 2, Profession = "java", Description = ""},
                new Task { Id = 3, Profession = "c#", Description = "study for the test"}
            };
        }
    
        public static List<Task> GetAll() => tasks;
    
        public static Task GetById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }
    
        public static int Add(Task newTask)
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
    
        public static bool Updata(int id, Task newTask)
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
    }
}
