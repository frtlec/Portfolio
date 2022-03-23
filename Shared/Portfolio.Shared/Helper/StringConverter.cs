using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Portfolio.Shared.Helper
{
    public static class StringConverter
    {
        public static string ConvertSpaceToDashAndTrToEnd(string text)
        {
            text = String.Join("", text.Normalize(NormalizationForm.FormD)
                     .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
            text = Regex.Replace(text, @"[/\-.]", "-");
            return Regex.Replace(text, @"[\s-]+", "-");
        }
    }
}
