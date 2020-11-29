using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IMAP.Model
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Name { get; private set; }
        public Grouping(K name, IEnumerable<T> items)
        {
            Name = name;
            foreach (T item in items)
                Items.Add(item);
        }
    }
}
