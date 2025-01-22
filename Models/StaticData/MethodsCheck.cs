using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.StaticData
{
    public static class MethodsCheck
    {
        public static string chechName(string? input)
        {
            if (input != null)
            {
            input = input.Replace('أ', 'ا')
                    .Replace('إ', 'ا')
                    .Replace('آ', 'ا');

            input = input.Replace("عبد ", "عبد");
            return input;
            }
            return null;
        }
    }
}
