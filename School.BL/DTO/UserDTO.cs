using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.DTO
{
    public class UserDTO
    {
        public int? UserId { get; set; }
        [MaxLength(PersonConsts.MaxFullNameLength)]
        public string FullName { get; set; }
        [StringLength(PersonConsts.MaxUserNameLength,MinimumLength =PersonConsts.MinUserNameLength)]
        [RegularExpression(PersonConsts.EmailFormat,ErrorMessage =PersonConsts.ErrorEmailMessage)]
        public string UserName { get; set; }
        [StringLength(PersonConsts.MaxPasswordLength,MinimumLength =PersonConsts.MinPasswordLength)]
        [RegularExpression(PersonConsts.PasswordFormat,ErrorMessage =PersonConsts.PasswordErrorMessage)]
        public string Password { get; set; }
        public int UserType { get; set; }
    }
}
