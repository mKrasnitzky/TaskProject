
using TaskProject.Models;

namespace TaskProject.Interfaces;

public interface IUserService {

    User get(int id);

    List<User> get();

    int Add(User user);

    bool delete(int id);

}