using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPro.shared.Consts;
using System.ComponentModel.DataAnnotations;

namespace School.BL.DTO
{
    public class RegisterTeacherDTO
    {
        public int? Id { get; set; }
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
        public int MaterialId { get; set; }
        public int? UserId { get; set; }
        [StringLength(MaterialConsts.MaxMateriaNamelLength, MinimumLength = MaterialConsts.MinMateriaNamelLength)]
        public string? MaterialName { get; set; }
        public int Gender { get; set; }

        public List<SelectListItem>? MaterialsItems { get; set; }
    }
}
