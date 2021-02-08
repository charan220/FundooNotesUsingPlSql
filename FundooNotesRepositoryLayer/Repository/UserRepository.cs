using FundooNotesModelLayer.Models;
using FundooNotesRepositoryLayer.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesRepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public object AddUser(UserModel userModel)
        {
            try
            {
                var password = Encryptdata(userModel.Password);
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand("sp_AddUser", _db))
                {
                    cmd.Connection = _db;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("userid", userModel.UserId);
                    cmd.Parameters.Add("employeename", userModel.FirstName);
                    cmd.Parameters.Add("phonenumber", userModel.LastName);
                    cmd.Parameters.Add("address", userModel.Email);
                    cmd.Parameters.Add("department", password);
                    _db.Open();
                    int value = cmd.ExecuteNonQuery();
                    _db.Close();
                    if (value != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool DeleteUser(string email)
        {
            try
            {

                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = _db;
                    cmd.CommandText = "sp_DeleteUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("email", email);
                    _db.Open();
                    int value = cmd.ExecuteNonQuery();
                    _db.Close();
                    if (value >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public  string LoginUser(LoginModel loginModel)
        {
            var decryptPassword = Encryptdata(loginModel.Password);
            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = _db;
                cmd.CommandText = "sp_LoginUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("email", loginModel.Email);
                cmd.Parameters.Add("password", decryptPassword);
                _db.Open();
                int value = cmd.ExecuteNonQuery();
                _db.Close();
                if (value >= 1)
                {
                    var token = GenrateJWTToken(loginModel.Email);
                    return token;
                }
                return null;

            }
        }
        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        private string GenrateJWTToken(string email)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),

                        };
            var tokenOptionOne = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }
        public bool ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var decryptPassword = Encryptdata(resetPasswordModel.Password);
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = _db;
                    cmd.CommandText = "sp_Reset_User";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("email", resetPasswordModel.Email);
                    cmd.Parameters.Add("password", decryptPassword);

                    _db.Open();
                    int value = cmd.ExecuteNonQuery();
                    _db.Close();
                    if (value >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public bool ForgotPasswordUser(ForgetPasswordModel model, string url)
        {
            throw new NotImplementedException();
        }
    }
    
}
