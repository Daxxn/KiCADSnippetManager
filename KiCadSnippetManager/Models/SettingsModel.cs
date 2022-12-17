using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SettingsLibrary.Models;

namespace KiCadSnippetManager.Models
{
   public class SettingsModel : ISettingsModel
   {
      public string? SavePath { get; set; }
      public string? LastSavePath { get; set; }
      public bool AutoOpen { get; set; } = true;
   }
}
