using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyNursingFuture.Util
{
    public static class StringExtensions
    {
        public static string Truncate(this string text, int length = 45, string suffix = "..." )
        {
            var shortText = Regex.Replace(text ?? String.Empty, @"<(.|\n)*?>", string.Empty).Substring(0, Math.Min((text ?? string.Empty).Length, length));
            return shortText.Length > 0 && (text.Length > shortText.Length) ? String.Concat(shortText, suffix) : shortText;
        }
    }
}
