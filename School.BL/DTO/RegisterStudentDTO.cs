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
    public class RegisterStudentDTO
    {
        public int? Id { get;set; }
        [StringLength(PersonConsts.MaxUserNameLength, MinimumLength = PersonConsts.MinUserNameLength)]
        [RegularExpression(PersonConsts.EmailFormat,ErrorMessage =PersonConsts.ErrorEmailMessage)]
 
        public string Email { get; set; }
        [StringLength(PersonConsts.MaxPasswordLength, MinimumLength = PersonConsts.MinPasswordLength)]
        [RegularExpression(PersonConsts.PasswordFormat,ErrorMessage =PersonConsts.PasswordErrorMessage)]
        public string Password { get; set; }
        [Required]
        [StringLength(PersonConsts.MaxFullNameLength, MinimumLength = PersonConsts.MinFullNameLength)]
        [RegularExpression(ClassConsts.FormatNameAr, ErrorMessage = ClassConsts.ErrorMessageForFormatNameAr)]

        public string FullNameAr { get; set; }
        [StringLength(PersonConsts.MaxFullNameLength, MinimumLength = PersonConsts.MinFullNameLength)]
        [RegularExpression(ClassConsts.FormatNameEn, ErrorMessage = ClassConsts.ErrorMessageForFormatNameEn)]

        public string? FullNameEn { get; set; }
        public int ClassId { get; set; }
        [Range(PersonConsts.MinAge, PersonConsts.MaxAge)]
        public int Age { get; set; }
        public int? UserId { get; set; }
        [StringLength(ClassConsts.MaxClassNameLength, MinimumLength = ClassConsts.MinClassNameLength)]

        public string? ClassName{get;set;}
        public int Gender { get;set; }
        public List<SelectListItem>? ClassItems { get; set; }
    }
}
