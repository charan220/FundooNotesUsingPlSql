using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesModelLayer.Models
{
   public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
