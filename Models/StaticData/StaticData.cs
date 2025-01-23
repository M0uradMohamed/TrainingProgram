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

    }
}
