
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
        public bool CheckLogin(User myUser){
            List<User> myList = userService.getUsers();
            // System.Console.WriteLine(userService.getUsers().Add());
            foreach(User user in userService.getUsers()) {
                if(user.name == myUser.name && user.password == myUser.password)
                    return true;
            }
            return false;
        }
        public bool CheckAdmin(User myUser) {
            foreach(User user in userService.getUsers()) {
                if(myUser.name == user.name && myUser.password == user.password){
                    if(user.isAdmin)
                        return true;
                    return false;
                }
            }
            return false;
        }

        public int GetId(User myUser) {
            foreach(User user in userService.getUsers()) {
               if(myUser.name == user.name && myUser.password == user.password)
                   return user.id;
            }
            return 1;
        }
    }
}