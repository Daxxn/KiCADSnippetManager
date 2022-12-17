using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KiCadSnippetManager.Models;

using MVVMLibrary;

namespace KiCadSnippetManager.ViewModels
{
   public class NewSnippetViewModel : ViewModel
   {
      #region Local Props
      public string[] CurrentNames { get; }
      private Snippet _snippet = null!;
      private string? _tagText;

      #region Commands
      public Command NewTagCmd { get; private set; } = null!;
      public Command RemoveTagCmd { get; private set; } = null!;
      #endregion
      #endregion

      #region Constructors
      public NewSnippetViewModel(string[] currentNames)
      {
         CurrentNames = currentNames;
         Snippet = new();
         Init();
      }

      public NewSnippetViewModel(Snippet snippet, string[] currentNames)
      {
         CurrentNames = currentNames;
         Snippet = snippet;
         Init();
      }
      #endregion

      #region Methods
      private void Init()
      {
         NewTagCmd = new(NewTag);
         RemoveTagCmd = new(RemoveTag);
      }
      private void NewTag()
      {
         if (string.IsNullOrEmpty(TagText))
            return;
         if (!Snippet.Tags.Contains(TagText))
         {
            Snippet.Tags.Add(TagText);
            TagText = null;
         }
      }

      private void RemoveTag()
      {
         if (string.IsNullOrEmpty(TagText))
            return;
         Snippet.Tags.Remove(TagText);
         TagText = null;
      }
      #region Events

      #endregion
      #endregion

      #region Full Props
      public Snippet Snippet
      {
         get => _snippet;
         set
         {
            _snippet = value;
            OnPropertyChanged();
         }
      }

      public string? TagText
      {
         get => _tagText;
         set
         {
            _tagText = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
