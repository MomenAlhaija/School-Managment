using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(PersonConsts.MaxFullNameLength)] 
        public string FullName { get; set; }  
        [MaxLength(PersonConsts.MaxUserNameLength)]
        [RegularExpression(PersonConsts.EmailFormat)]
        public string UserName { get; set; }
 
        public string Password { get; set; }
        public int UserType { get; set; }   
    }
}
