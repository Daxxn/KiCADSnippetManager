using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVVMLibrary;

using Newtonsoft.Json;

namespace KiCadSnippetManager.Models
{
   public class SnippetCollection : Model, IList<Snippet>
   {
      #region Local Props
      private ObservableCollection<Snippet> _snippets = new();
      private ObservableCollection<Snippet>? _searchResults = null;
      public Snippet this[int index] { get => Snippets[index]; set => Snippets[index] = value; }
      public Snippet? this[Snippet? snippet]
      {
         get => snippet is null ? null : Snippets.FirstOrDefault(sn => sn.Name == snippet.Name) ?? null;
         set
         {
            if (snippet is null)
               return;
            Replace(snippet);
         }
      }

      [JsonIgnore]
      public int Count => Snippets.Count;
      [JsonIgnore]
      public bool IsReadOnly => false;
      #endregion

      #region Constructors
      public SnippetCollection() { }
      public SnippetCollection(IEnumerable<Snippet> collection) => Snippets = new(collection);
      #endregion

      #region Methods
      public bool Contains(Snippet item) => Snippets.Contains(item);
      public int IndexOf(Snippet item) => Snippets.IndexOf(item);
      public Snippet Create()
      {
         var newItem = new Snippet();
         Snippets.Add(newItem);
         return newItem;
      }
      public void Add(Snippet item)
      {
         if (Contains(item))
         {
            throw new ArgumentException("Item already exists.", nameof(item));
         }
         Snippets.Add(item);
      }
      public void Replace(Snippet item)
      {
         if (Contains(item))
         {
            throw new ArgumentException("Item already exists.", nameof(item));
         }
         Snippets.Add(item);
      }
      public void Insert(int index, Snippet item) => Snippets.Insert(index, item);
      public bool Remove(Snippet item) => Snippets.Remove(item);
      public void RemoveAt(int index) => Snippets.RemoveAt(index);
      public void Clear()
      {
         Snippets.Clear();
         SearchResults = null;
      }
      public void CopyTo(Snippet[] array, int arrayIndex) => Snippets.CopyTo(array, arrayIndex);
      public IEnumerator<Snippet> GetEnumerator() => Snippets.GetEnumerator();
      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      #region Search Methods
      public void Search(string text, bool exludeTags = false) =>
         SearchResults = new(Snippets.Where(snip => snip.Name.Contains(text) || (!exludeTags && snip.Tags.Contains(text))));

      public void ClearSearch() => SearchResults = null;
      #endregion
      #endregion

      #region Full Props
      public ObservableCollection<Snippet> Snippets
      {
         get => _snippets;
         set
         {
            _snippets = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SnippetDisplay));
         }
      }

      [JsonIgnore]
      public ObservableCollection<Snippet> SnippetDisplay => SearchResults ?? Snippets;

      [JsonIgnore]
      public ObservableCollection<Snippet>? SearchResults
      {
         get => _searchResults;
         set
         {
            _searchResults = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSearching));
         }
      }

      [JsonIgnore]
      public bool IsSearching => _searchResults != null;
      #endregion
   }
}
