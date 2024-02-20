using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.DTO
{
    public class StudentGradeDTO
    {
        public int StudentId { get; set; }
        [Range(PersonConsts.MinGrade, PersonConsts.MaxGrade)]
        public decimal Grade { get; set;}
        public int MaterialId { get; set; }
        public int ClassId { get; set; }
    }
}
