using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.StaticData
{
    public static class BelongData
    {
        public static readonly Dictionary<Belong, string> belong = new Dictionary<Belong, string>()
        {
            {Belong.BelongTo,"تابع" },
            {Belong.NotBelongTo,"غير تابع" },
        };
    }
}
