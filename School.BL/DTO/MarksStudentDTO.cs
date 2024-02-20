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
    public class MarksStudentDTO
    {
        public int? StudentId { get; set; }
        public int? UserId { get; set; }
        public string Email { get; set; }
        [RegularExpression(ClassConsts.FormatNameAr, ErrorMessage = ClassConsts.ErrorMessageForFormatNameAr)]

        public string FullNameAr { get; set; }
        [RegularExpression(ClassConsts.FormatNameEn, ErrorMessage = ClassConsts.ErrorMessageForFormatNameEn)]

        public string? FullNameEn { get; set; }
        public List<GradeDTO> Grades { get; set; }
        public int ClassId { get; set; }    
        public Gender Gender { get; set; }  
        public int Age { get; set; }
        public string ClassName { get; set; }
    }
}
