
using System.Collections.Generic;
using System.Linq;
using TaskProject.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using TaskProject.Models;

namespace TaskProject.Services
{
    public class UserService :IUserService
    {

        private List<User> users { get; set; }

        private TaskService taskService;
    
        private string fileName = "Users.json";

        public UserService()
        {
            taskService=new TaskService();
            this.fileName = Path.Combine("data", "Users.json");
            using (var jsonFile = File.OpenText(fileName))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

        }


        public void saveToFile()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(users));
        }

        public User Get(int userId)
        {
            foreach(User user in users){
                if (user.id == userId)
                    return user;
            }
            return null;
        }

        public User GetById(int id)
        {
            return users.Find(user => id == user.id);
        }

        public List<User> GetAll()
        {
            return users;
        }

        public int Add(User user)
        {
            if (users.Count == 0)
            {
                user.id = 1;
            }
            else
            {
                user.id = users.Max(t => t.id) + 1;
            }
            users.Add(user);
            saveToFile();
            return user.id;
        }

        public bool Update(int id, User newUser)
        {
            if (id != newUser.id)
                return false;
            var existingUser = GetById(id);
            if (existingUser == null)
                return false;
            var index = users.IndexOf(existingUser);
            if (index == -1)
                return false;
            users[index]=newUser;
            saveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            var existingTask = GetById(id);
            if (existingTask == null)
                return false;

            var index = users.IndexOf(existingTask);
            if (index == -1)
                return false;

            taskService.DeleteTasks(existingTask.id);
            users.RemoveAt(index);
            saveToFile();

            return true;
        }

        public bool IsAdmin(int id) {

            foreach (User user in users)
            {
                if (user.id == id)
                {
                    return user.isAdmin;
                }
            }
            return false;

        }

        public List<User>  getUsers()=>users;

    }


    public static class UserUtils
    {
        public static void AddUser(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
        }
    }

}
