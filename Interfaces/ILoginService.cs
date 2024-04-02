
using TaskProject.Models;

namespace TaskProject.Interfaces
{
    public interface ILoginService
    {
        bool CheckAdmin(User myUser);
        bool CheckLogin(User myUser);
        int GetId(User myUser);    
    }
}