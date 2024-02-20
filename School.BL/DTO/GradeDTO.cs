using School.Core.Entities;
using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.DTO
{
    public class GradeDTO
    {
        public int MaterialId { get; set; }
        public string MaterialNameAr { get; set; }
        public string? MaterialNameEn { get; set; }
        [Range(PersonConsts.MinGrade, PersonConsts.MaxGrade)]
        public decimal? Grade { get; set; }  
    }
}
