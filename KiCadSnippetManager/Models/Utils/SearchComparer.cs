using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiCadSnippetManager.Models.Utils
{
   public class SearchComparer : IEqualityComparer<string>
   {
      public bool IgnoreCase { get; set; }
      public bool Equals(string? x, string? y)
      {
         return x.Contains(y, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
      }
      public int GetHashCode([DisallowNull] string obj)
      {
         return obj.GetHashCode();
      }
   }
}
