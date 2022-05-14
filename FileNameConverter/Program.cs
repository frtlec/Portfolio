using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileNameConverter
{
  internal class Program
  {
    static List<Testx> ListCurrent = new List<Testx>
    {
      new Testx
      {
        Id=1,
        Type=500,
        Value="Zafer"
      },
       new Testx
      {
        Id=2,
        Type=600,
        Value="Kamuran"
      },
        new Testx
      {
        Id=3,
        Type=700,
        Value="Nagihan"
      },
    };
    static List<Testx> ListNew = new List<Testx>
    {
     
       new Testx
      {
        Id=2,
        Type=600,
        Value="Kamuranx"
      },
        new Testx
      {
        Id=3,
        Type=700,
        Value="Nagihanq"
      },
      new Testx
      {
        Id=4,
        Type=800,
        Value="Tulay"
      },
    };
    static void Main(string[] args)
    {

     var x= Guid.Parse("2548bfc0-361c-4376-b553-cac3f8ec22a2");

      //var diff= ListNew.Except(ListCurrent);

      //var deleteItems = ListCurrent.Where(p => !ListNew.Any(l => p.Type == l.Type)).ToList();

      //foreach (var item in deleteItems)
      //{
      //  ListCurrent.Remove(item);//deleted
      //}

      //foreach (var item in ListNew)
      //{
      //  var x = ListCurrent.FirstOrDefault(f => f.Type == item.Type);
      //  if (x!=null)
      //  {
      //    x.Value = item.Value;
      //  }
      //  else
      //  {
      //    ListCurrent.Add(item);
      //  }
      //}







      //fileName= String.Join("", fileName.Normalize(NormalizationForm.FormD)
      //         .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

      //fileName = Regex.Replace(fileName, @"[/\-.]", "-");
      //Console.WriteLine(Regex.Replace(fileName,@"[\s-]+","-"));
    }
  }
  public class Testx
  {
    public int Id { get; set; }
    public string Value { get; set; }
    public int Type { get; set; }
  }
}
