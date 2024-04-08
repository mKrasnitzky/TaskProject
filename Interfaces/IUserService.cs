
using TaskProject.Models;

namespace TaskProject.Interfaces
{
    public interface IUserService {

        User Get(int userId);

        User GetById(int id);

        List<User> GetAll();

        int Add(User user);

        bool Update(int id, User newUser);

        bool Delete(int id);

        bool IsAdmin(int id);
        
        List<User> getUsers();
    }
}

