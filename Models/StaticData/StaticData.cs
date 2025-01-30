using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.StaticData
{
    public static class StaticData
    {
        public static readonly Dictionary<Gender, string> gender = new Dictionary<Gender, string>()
        {
            {Gender.Male,"ذكر" },
            {Gender.Female,"انثى" },
        };
        public static readonly Dictionary<Belong, string> belong = new Dictionary<Belong, string>()
        {
            {Belong.BelongTo,"تابع" },
            {Belong.NotBelongTo,"غير تابع" },
        };
        public static readonly Dictionary<Check, string> check = new Dictionary<Check, string>()
        {
            {Check.Ongoing,"قائم" },
            {Check.Pending, "منتظر"},
            {Check.Completed,"تمت" },
            {Check.Canceled, "لاغي"},
            {Check.Postponed, "مؤجل"}
        };
        public static readonly Dictionary<CourseType, string> courseType = new Dictionary<CourseType, string>()
        {
            {CourseType.Path,"مسار"},
            {CourseType.NotPath,"غير مسار"}
        };
        public static readonly Dictionary<Estimate, string> estimate = new Dictionary<Estimate, string>()
        {
            {Estimate.Excellent,"ممتاز" },
            {Estimate.VeryGood,"جيد جدا" },
            {Estimate.Good,"جيد" },
            {Estimate.Acceptable,"مقبول" },
            {Estimate.Failed,"راسب" }
        };
        public static readonly Dictionary<Material, string> material = new Dictionary<Material, string>()
        {
            {Material.Available,"متاحة" },
            {Material.Unavailable,"غير متاحة" }
        };
        public static readonly Dictionary<ImplementationMonth, string> implementationMonth = new Dictionary<ImplementationMonth, string>()
        {
            {ImplementationMonth.January ,"يناير" },
            {ImplementationMonth.February ,"فبراير" },
            {ImplementationMonth.March ,"مارس" },
            {ImplementationMonth.April ,"ابريل" },
            {ImplementationMonth.May ,"مايو" },
            {ImplementationMonth.June ,"يونيو" },
            {ImplementationMonth.July ,"يوليو" },
            {ImplementationMonth.August ,"اغسطس" },
            {ImplementationMonth.September ,"سبتمبر" },
            {ImplementationMonth.October ,"اكتوبر" },
            {ImplementationMonth.November ,"نوفمبر" },
            {ImplementationMonth.December ,"ديسمبر" }
        };

    }
}
