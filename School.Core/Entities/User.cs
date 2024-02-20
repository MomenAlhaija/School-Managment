using Microsoft.AspNetCore.Identity;
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
        [MaxLength(50)] 
        public string FullName { get; set; }  
        [MaxLength(20)]
        public string UserName { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }
        public int UserType { get; set; }   
    }
}
