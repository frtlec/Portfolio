using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileNameConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "Bakery website/bread&cakes";

            fileName= String.Join("", fileName.Normalize(NormalizationForm.FormD)
                     .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            fileName = Regex.Replace(fileName, @"[/\-.]", "-");
            Console.WriteLine(Regex.Replace(fileName,@"[\s-]+","-"));
        }
    }
}
