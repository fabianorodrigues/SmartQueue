using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmartQueue.Utils
{
    public class Agrupar<TKey, TItem> : ObservableCollection<TItem>
    {
        public TKey Key { get; private set; }
        public Agrupar(TKey key, IEnumerable<TItem> items)
        {
            this.Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
