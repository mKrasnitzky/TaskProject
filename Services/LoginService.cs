
using TaskProject.Interfaces;
using TaskProject.Models;
using TaskProject.Services;

namespace TaskProject.Services
{
    
    public class LoginService : ILoginService
    {

        IUserService userService;
        
        public LoginService(){
            this.userService = new UserService();
        }

        public bool CheckLogin(User myUser) {

            List<User> myList = userService.GetUsers();

            foreach(User user in userService.GetUsers()) 
            {
                if(user.Name == myUser.Name && user.Password == myUser.Password)
                    return true;
            }
            return false;
        }
        
        public bool CheckAdmin(User myUser) {
            foreach(User user in userService.GetUsers()) {
                if(myUser.Name == user.Name && myUser.Password == user.Password){
                    if(user.IsAdmin)
                        return true;
                    return false;
                }
            }
            return false;
        }

        public int GetId(User myUser) {
            foreach(User user in userService.GetUsers()) {
               if(myUser.Name == user.Name && myUser.Password == user.Password)
                   return user.Id;
            }
            return 1;
        }
    }
}