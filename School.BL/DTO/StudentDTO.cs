using School.BL.Enum;
using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.DTO
{
    public class StudentDTO
    {
        public int? StudentId { get; set; }  
        public int? UserId { get; set; }
        public string FullNameAr { get; set; }
        public string? FullNameEn { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        [Range(PersonConsts.MinGrade, PersonConsts.MaxGrade)]
        public decimal? Grade { get;set; }

    }
}
