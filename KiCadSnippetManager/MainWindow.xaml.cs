using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using KiCadSnippetManager.Models;
using KiCadSnippetManager.ViewModels;
using KiCadSnippetManager.Views;

namespace KiCadSnippetManager
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainViewModel VM { get; set; }
      public MainWindow()
      {
         VM = App.MainVM;
         DataContext = VM;
         InitializeComponent();
         Loaded += VM.OnLoaded;
      }

      private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         if (sender is ListBox lb)
         {
            if (lb.SelectedItem is string tag)
            {
               VM.SearchText = tag;
            }
         }
      }

      private void OpenNewSnippet_Click(object sender, RoutedEventArgs e)
      {
         var newSnipVM = new EditSnippetViewModel(VM.SnippetList.Select(x => x.Name).ToArray());
         var newSnippetView = new EditSnippetView(newSnipVM);
         if (newSnippetView.ShowDialog() == true)
         {
            VM.NewSnippet(newSnipVM.Snippet);
         }
      }

      private void Edit_Click(object sender, RoutedEventArgs e)
      {
         if (sender is Button btn)
         {
            if (btn.DataContext is Snippet snip)
            {
               var newSnipVM = new EditSnippetViewModel(snip, VM.SnippetList.Select(x => x.Name).ToArray());
               var editView = new EditSnippetView(newSnipVM);
               if (editView.ShowDialog() == true)
               {
                  snip = newSnipVM.Snippet;
               }
            }
         }
      }
   }
}
