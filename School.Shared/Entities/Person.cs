using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class Person
    {

        [MaxLength(PersonConsts.MaxFullNameLength)]
        public string FullNameAr { get; set; }
        [MaxLength(PersonConsts.MaxFullNameLength)]
        public string? FullNameEn { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public int Gender { get; set; }
        
    }
}
