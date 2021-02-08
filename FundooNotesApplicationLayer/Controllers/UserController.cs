using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserManager userManager;
        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            try
            {
                var item = this.userManager.AddUser(userModel);
                if (item != null)
                {
                    return this.Ok(new { Status = true, Message = "user added successfully", Data = item });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "user added unsuccessfully", Data = item });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }


        }

        [HttpDelete]
        [Route("{empEmail}")]
        public IActionResult DeleteUser(string empEmail)
        {
            try
            {
                var result = this.userManager.DeleteUser(empEmail);
                if (result != true)
                {
                    return this.Ok(new { Status = true, Message = "user deleted successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "user deleted unsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result =  this.userManager.LoginUser(loginModel);
                if (result == null)
                {
                    return this.BadRequest(new { Status = false, Message = "user login unsuccessfully", Data = result });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = "user login successfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }
        [HttpPut]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var result = this.userManager.ResetPassword(resetPasswordModel);
                if (result != true)
                {
                    return this.BadRequest(new { Status = false, Message = "reset pasword unsuccessfully", Data = result });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = "reset paswordsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }
    }
}