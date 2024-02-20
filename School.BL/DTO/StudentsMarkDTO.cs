using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.DTO
{
    public class StudentsMarkDTO
    {
        public int? StudentId { get; set; }
        public int? UserId { get; set; }
        [RegularExpression(ClassConsts.FormatNameAr, ErrorMessage = ClassConsts.ErrorMessageForFormatNameAr)]
        public string FullNameAr { get; set; }
        [RegularExpression(ClassConsts.FormatNameEn, ErrorMessage = ClassConsts.ErrorMessageForFormatNameEn)]
        public string? FullNameEn { get; set; }
        public int MaterialId { get; set; }
        [Range(PersonConsts.MinGrade, PersonConsts.MaxGrade)]
        public decimal StudentMark { get; set; }
    }
}
