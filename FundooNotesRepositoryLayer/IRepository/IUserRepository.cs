using FundooNotesModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesRepositoryLayer.IRepository
{
    public interface IUserRepository
    {
        object AddUser(UserModel userModel);
        bool DeleteUser(string email);
        bool ForgotPasswordUser(ForgetPasswordModel model, string url);
        string LoginUser(LoginModel loginModel);

        bool ResetPassword(ResetPasswordModel model);
    }
}
