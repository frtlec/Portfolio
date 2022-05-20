using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Portfolio.Shared.Extensions
{
  public static class StringExtensions
  {
    public static string RemoveHtmlTags(this string value)
    {
      if (value == null)
      {
        return string.Empty;
      }

      value= Regex.Replace(value, "<.*?>", String.Empty);

      return value; 
    }
    public static string RemoveLines(this string value)
    {
      if (value == null)
      {
        return string.Empty;
      }

      value = Regex.Replace(value, "\n", String.Empty);

      return value;
    }
    public static string RemoveSpaces(this string value)
    {
      if (value == null)
      {
        return string.Empty;
      }

      value = Regex.Replace(value, @"\s", "");

      return value;
    }
  }
}
