using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TfsRestApi.Services.Extensions
{
    public static class StringEx
    {
        public static String Alt(this string s, string alt)
        {
            string result = String.IsNullOrEmpty(s) ? alt : s;
            return result;
        }
    }
}