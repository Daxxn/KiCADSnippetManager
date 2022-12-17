using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using JsonReaderLibrary;

using KiCadSnippetManager.Models;

using Microsoft.Win32;

using MVVMLibrary;

using SettingsLibrary;

namespace KiCadSnippetManager.ViewModels;

public class MainViewModel : ViewModel
{
   #region Local Props
   private SnippetCollection _snippetList = new();
   private Snippet? _selectedSnippet = null;

   private string? _searchText = null;
   #region Commands
   public Command UseSnippetCmd { get; init; }
   public Command ClearSnippetsCmd { get; init; }
   public Command ClearClipboardCmd { get; init; }
   public Command DeleteSnippetCmd { get; init; }

   public Command NewCmd { get; init; }
   public Command SaveCmd { get; init; }
   public Command OpenCmd { get; init; }

   public Command SearchCmd { get; init; }
   public Command ClearSearchCmd { get; init; }
   #endregion
   #endregion

   #region Constructors
   public MainViewModel()
   {
      UseSnippetCmd = new(UseSnippet);
      ClearSnippetsCmd = new(ClearSnippets);
      ClearClipboardCmd = new(ClearClipboard);
      DeleteSnippetCmd = new(DeleteSnippet);

      NewCmd = new(NewFile);
      SaveCmd = new(Save);
      OpenCmd = new(Open);

      SearchCmd = new(Search);
      ClearSearchCmd = new(ClearSearch);
   }
   #endregion

   #region Methods
   private async void Save()
   {
      try
      {
         if (!File.Exists(App.Settings.SavePath))
         {
            SaveFileDialog dialog = new()
            {
               AddExtension = true,
               DefaultExt = ".json",
               Title = "Save Snippets",
               Filter = "Json|*.json|All Files|*.*",
               CustomPlaces = App.Settings.SaveLocations != null ? new List<FileDialogCustomPlace>(App.Settings.SaveLocations.Select(x => new FileDialogCustomPlace(x))) : null,
            };
            if (dialog.ShowDialog() == true)
            {
               await JsonReader.SaveJsonFileAsync(dialog.FileName, SnippetList, true);
               App.Settings.SavePath = dialog.FileName;
               App.SaveSettings();
            }
         }
         else
         {
            await JsonReader.SaveJsonFileAsync(App.Settings.SavePath!, SnippetList);
         }
      }
      catch (Exception e)
      {
         MessageBox.Show(e.Message, "Save Error");
      }
   }

   private async void Open()
   {
      try
      {
         if (!File.Exists(App.Settings.SavePath))
         {
            OpenFileDialog dialog = new()
            {
               AddExtension = true,
               DefaultExt = ".json",
               Multiselect = false,
               Title = "Open Snippets"
            };

            if (dialog.ShowDialog() == true)
            {
               var snippets = await JsonReader.OpenJsonFileAsync<SnippetCollection>(dialog.FileName);
               if (snippets is null)
                  return;
               App.Settings.SavePath = dialog.FileName;
               App.SaveSettings();
               SnippetList = snippets;
            }
         }
         else
         {
            var snippets = await JsonReader.OpenJsonFileAsync<SnippetCollection>(App.Settings.SavePath);
            if (snippets != null)
            {
               SnippetList = snippets;
            }
         }
      }
      catch (Exception e)
      {
         MessageBox.Show(e.Message, "Open Error");
      }
   }

   private void NewFile()
   {
      SnippetList = new();
      SelectedSnippet = null;
      SearchText = null;
      App.Settings.SavePath = null;
   }

   public void NewSnippet(Snippet newSnippet)
   {
      SnippetList.Add(newSnippet);
      SelectedSnippet = newSnippet;
   }

   private void UseSnippet()
   {
      if (SelectedSnippet is null)
         return;
      Clipboard.SetText(SelectedSnippet.Value);
   }

   private void ClearSnippets()
   {
      if (MessageBox.Show("Are you sure??", "WOAHH", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
      {
         SelectedSnippet = null;
         SearchText = null;
         SnippetList.Clear();
         Clipboard.Clear();
      }
   }

   private void ClearClipboard()
   {
      SelectedSnippet = null;
      Clipboard.Clear();
   }

   private void DeleteSnippet()
   {
      if (SelectedSnippet is null)
         return;
      SnippetList.Remove(SelectedSnippet);
   }

   private void Search()
   {
      if (SearchText is null)
      {
         SnippetList.Clear();
         return;
      }
      SnippetList.Search(SearchText);
   }

   private void ClearSearch()
   {
      SearchText = null;
      SnippetList.ClearSearch();
   }
   #region Events
   public void OnLoaded(object? sender, EventArgs e)
   {
      if (App.Settings.AutoOpen && App.Settings.SavePath != null)
      {
         Open();
      }
   }
   #endregion
   #endregion

   #region Full Props
   public SnippetCollection SnippetList
   {
      get => _snippetList;
      set
      {
         _snippetList = value;
         OnPropertyChanged();
      }
   }

   public Snippet? SelectedSnippet
   {
      get => _selectedSnippet;
      set
      {
         _selectedSnippet = value;
         OnPropertyChanged();
      }
   }

   public string? SearchText
   {
      get => _searchText;
      set
      {
         _searchText = value;
         OnPropertyChanged();
      }
   }
   #endregion
}
