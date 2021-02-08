using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer.Models;
using FundooNotesRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesManagerLayer.Manager
{
    public class UserManager : IUserManager
    {
        IUserRepository userRepository;
        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
       public object AddUser(UserModel userModel)
        {
            return this.userRepository.AddUser(userModel);
        }

        public bool DeleteUser(string email)
        {
            return this.userRepository.DeleteUser(email);
        }
        public string LoginUser(LoginModel loginModel)
        {
            var result = this.userRepository.LoginUser(loginModel);
            return result;
        }

        public bool ResetPassword(ResetPasswordModel model)
        {
            return this.userRepository.ResetPassword(model);
        }
    }
}
