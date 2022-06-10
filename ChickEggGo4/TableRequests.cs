using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickEggGo4
{
    class TableRequests: IEnumerable<string>
    {
        private Dictionary<string, List<IMenuItem>> orders = new Dictionary<string, List<IMenuItem>>();

        public void Add<T>(string nameCustomer) where T : IMenuItem 
        {
            if (!orders.ContainsKey(nameCustomer))
            {
                orders.Add(nameCustomer, new List<IMenuItem>());
            }
            orders[nameCustomer].Add(Activator.CreateInstance<T>());
        }

         /* public List<IMenuItem> Get(IMenuItem menuitem)
        {
            List<IMenuItem> li = new List<IMenuItem>():
            foreach (KeyValuePair<string, List<IMenuItem>> element in orders)
            {
                foreach (var elem in element.Value)
                {
                    if (elem.GetType() == menuitem.GetType())
                    {
                        li.Add(elem);
                    }
                }
            }
            return li;
        } */

        public List<T> Get<T>()
        {
            List<T> list = new List<T>();
            foreach (var order in orders.Values)
            {
                foreach (var item in order)
                {
                    if (item is T menuitem)
                    {
                        list.Add(menuitem);
                    }
                }
                
            }
            return list;
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in orders.Keys)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<IMenuItem> this[string customerName]
        {
            get
            {
                return orders[customerName];
            }
        }

       
    }
}
