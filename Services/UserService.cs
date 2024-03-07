
using System.Text.Json;
using TaskProject.Interfaces;
using TaskProject.Models;
using TaskProject.Services;

namespace TaskProject.Services;

public class UserService : IUserService
{

    private List<User> users;

    private string fileName = "Users.json";

    public UserService()
    {
        this.fileName = Path.Combine("data", "Tasks.json");
        using (var jsonFile = File.OpenText(fileName))
        {
            users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

    }

    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(users));
    }


    public User get(int id)
    {
        return users.Find(user => id == user.Id);
    }

    public List<User> get()
    {
        return users;
    }

    public int Add(User user)
    {
        users.Add(user);
        if (users.Count == 0)
        {
            user.Id = 1;
        }
        else
        {
            user.Id = users.Max(t => t.Id) + 1;
        }
        users.Add(user);
        saveToFile();
        return user.Id;
    }

    public bool delete(int id)
    {
        var existingTask = get(id);
        if (existingTask == null)
            return false;

        var index = users.IndexOf(existingTask);
        if (index == -1)
            return false;

        users.RemoveAt(index);
        saveToFile();

        return true;
    }

}