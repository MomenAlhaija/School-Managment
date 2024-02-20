using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ClassDTO
    {
        public int? Id {get;set;}
        [Required]
        [StringLength(ClassConsts.MaxClassNameLength, MinimumLength = ClassConsts.MinClassNameLength)]
        [RegularExpression(ClassConsts.FormatNameAr, ErrorMessage = ClassConsts.ErrorMessageForFormatNameAr)]

        public string ClassNameAr { get; set; } = string.Empty;
        [StringLength(ClassConsts.MaxClassNameLength, MinimumLength = ClassConsts.MinClassNameLength)]
        [RegularExpression(ClassConsts.FormatNameEn, ErrorMessage = ClassConsts.ErrorMessageForFormatNameEn)]

        public string? ClassNameEn { get; set; }
        public IEnumerable<Material>? Materials { get; set; }
        public List<int>? MaterialsIds { get; set; }
        public List<SelectListItem>? MateriaLItems { get; set; }
        public Material? ClassMaterialForTeacher { get; set; }
    }
}
