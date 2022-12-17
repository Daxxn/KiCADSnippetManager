using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVVMLibrary;

namespace KiCadSnippetManager.Models;

[DebuggerDisplay(" Name - {Name} - Tags {Tags.Count} - Size {Value.Length}")]
public class Snippet : Model
{
   private string _name = "New Snippet";
   private ObservableCollection<string> _tags = new();
   private string? _value = null;

   #region Methods
   //public override string ToString() => $"";
   #endregion

   #region Full Props
   public string Name
   {
      get => _name;
      set
      {
         _name = value;
         OnPropertyChanged();
      }
   }

   public ObservableCollection<string> Tags
   {
      get => _tags;
      set
      {
         _tags = value;
         OnPropertyChanged();
      }
   }

   public string? Value
   {
      get => _value;
      set
      {
         _value = value;
         OnPropertyChanged();
      }
   }
   #endregion
}
