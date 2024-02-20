using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.DTO
{
    public class ResetPasswordDTO
    {
        [StringLength(PersonConsts.MaxPasswordLength, MinimumLength = PersonConsts.MinPasswordLength)]
        [RegularExpression(PersonConsts.PasswordFormat,ErrorMessage =PersonConsts.PasswordErrorMessage)]

        public string OldPassword { get; set; }
        [StringLength(PersonConsts.MaxPasswordLength, MinimumLength = PersonConsts.MinPasswordLength)]
        [RegularExpression(PersonConsts.PasswordFormat,ErrorMessage =PersonConsts.PasswordErrorMessage)]
        public string NewPassword { get; set; }
        public int UserId { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
