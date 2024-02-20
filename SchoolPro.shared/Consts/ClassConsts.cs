using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPro.shared.Consts
{
    public static class ClassConsts
    {
        public const int MaxClassNameLength = 50;
        public const int MinClassNameLength = 1;
        public const string FormatNameAr = @"^[\u0600-\u06FF0-9\s\p{P}]+$";
        public const string ErrorMessageForFormatNameAr = "Input must contain only Arabic characters  and numbers";
        public const string FormatNameEn = @"^[A-Za-z\d\s]+$";
        public const string ErrorMessageForFormatNameEn = "Input must contain only English characters and numbers";
    }
}
