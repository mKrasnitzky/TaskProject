
using TaskProject.Models;

namespace TaskProject.Interfaces
{
    public interface ILoginService
    {
        bool CheckLogin(User myUser);

        bool CheckAdmin(User myUser);
        
        int GetId(User myUser);    
    }
}