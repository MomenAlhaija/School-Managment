using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.DTO
{
    public class MarkDTO
    {
        public int StudentId { get; set; }
        [Range(PersonConsts.MinGrade, PersonConsts.MaxGrade)]
        public decimal Mark { get; set; }   
        public int MaterialId { get; set; } 
    }
}
