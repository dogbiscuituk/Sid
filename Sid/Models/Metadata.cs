using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sid.Models
{
    public static class Metadata
    {
        public static string AmpersandEscape(this string s)
        {
            return s.Replace("&", "&&");
        }

        public static string AmpersandUnescape(this string s)
        {
            return s.Replace("&&", "&");
        }
    }
}
