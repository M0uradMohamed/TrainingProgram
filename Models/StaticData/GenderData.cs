using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.StaticData
{
    public static class GenderData
    {
        public static readonly Dictionary<Gender, string> gender = new Dictionary<Gender, string>()
        {
            {Gender.Male,"ذكر" },
            {Gender.Female,"انثى" },
        };

    }
}
