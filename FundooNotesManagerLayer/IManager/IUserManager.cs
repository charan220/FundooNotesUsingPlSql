using FundooNotesModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesManagerLayer.IManager
{
  public  interface IUserManager
    {
        object AddUser(UserModel userModel);
        bool DeleteUser(string email);
        string LoginUser(LoginModel loginModel);
        bool ResetPassword(ResetPasswordModel model);


    }
}
