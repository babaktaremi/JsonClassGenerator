using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonClassGenerator
{
   public static class Utility
    {
        public static string Capitalize(this string input)=> input.Trim().First().ToString().ToUpper() + input.Trim().Substring(1);
    }
}
