using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Infrastructure.Extentions
{
    public static class IEnumerableExtentions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            var obsCollection = new ObservableCollection<T>();
            foreach(var item in collection)
            {
                obsCollection.Add(item);
            }
            return obsCollection;
        }
    }
}
