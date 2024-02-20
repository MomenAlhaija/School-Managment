using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPro.shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.DTO
{
    public class AddOrUpdateMaterialDTO
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(MaterialConsts.MaxMateriaNamelLength,MinimumLength =MaterialConsts.MinMateriaNamelLength)]
        [RegularExpression(ClassConsts.FormatNameAr,ErrorMessage =ClassConsts.ErrorMessageForFormatNameAr)]
        public string MaterialNameAr { get; set; }
        [StringLength(MaterialConsts.MaxMateriaNamelLength, MinimumLength = MaterialConsts.MinMateriaNamelLength)]
        [RegularExpression(ClassConsts.FormatNameEn, ErrorMessage = ClassConsts.ErrorMessageForFormatNameEn)]
        public string? MaterialNameEn { get; set; }
    }
}
