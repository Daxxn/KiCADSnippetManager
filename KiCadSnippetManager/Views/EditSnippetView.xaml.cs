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
using System.Windows.Shapes;

using KiCadSnippetManager.ViewModels;

namespace KiCadSnippetManager.Views
{
   /// <summary>
   /// Interaction logic for NewSnippetView.xaml
   /// </summary>
   public partial class EditSnippetView : Window
   {
      public NewSnippetViewModel VM { get; set; }
      public EditSnippetView(NewSnippetViewModel vm)
      {
         VM = vm;
         DataContext = VM;
         InitializeComponent();
      }

      private void RemoveTag_Click(object sender, RoutedEventArgs e)
      {
         if (sender is ListBox lv)
         {
            if (lv.SelectedItem is string tag)
            {
               VM.Snippet.Tags.Remove(tag);
            }
         }
      }

      private void Create_Click(object sender, RoutedEventArgs e)
      {
         DialogResult = true;
         Close();
      }
   }
}
