using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickEggGo4
{
   
    class Server
    {
        public delegate void readyDelegate(TableRequests tableRequests);

        readonly MenuItemComparer menuItemComparer = new MenuItemComparer();
        public event readyDelegate Ready;

        private TableRequests tablerequest = new TableRequests();
        public static int counReqs = 0;
        
        public void Recieve(int countChick, int countEgg, string drink, string nameCustomer)
        {
            switch (drink)
            {
                case "Tea":
                    tablerequest.Add<Tea>(nameCustomer);
                    break;
                case "Coca-Cola":
                    tablerequest.Add<CocaCola>(nameCustomer);
                    break;
                case "Pepsi":
                    tablerequest.Add<Pepsi>(nameCustomer);
                    break;
                default:
                    tablerequest.Add<NoDrink>(nameCustomer);
                    break;
            }

            for (int i = 0; i < countChick; i++)
            {
                tablerequest.Add<Chicken>(nameCustomer);
            }
            for (int i = 0; i < countEgg; i++)
            {
                tablerequest.Add<Egg>(nameCustomer);
            }
            
           counReqs++;
        }
        public void Send()
        {
            Ready?.Invoke(tablerequest);
        }
        
        public List<string> Serve()
        {
            List<string> results = new List<string>();
            foreach (var customerName in tablerequest)
            {
                var orders = tablerequest[customerName];
                orders.Sort(menuItemComparer);
                int countchicken = 0;
                int counteggs = 0;
                string drinkName = "No Drink";
              
                foreach (var order in orders)
                {
                    if (order is Drink drink)
                    {
                        drinkName = order.GetType().Name;
                        order.Obtain();
                    }
                    if (order is Chicken)
                    {
                        countchicken++;
                    }
                    if (order is Egg)
                    {
                        counteggs++;
                    }
                    order.Serve();
                }

                results.Add($"Customer Name: {customerName} ; Drink: {drinkName} ; Orders: " +
                    $"{countchicken} chickens, {counteggs} eggs");
            }
            tablerequest = new TableRequests();
            return results;
        }
    }

    class MenuItemComparer : IComparer<IMenuItem>
    {
        public int Compare(IMenuItem x, IMenuItem Y)
        {
            if (x is Drink && !(Y is  Drink))
            {
                return -1;
            }
            else if (!(x is Drink) && Y is Drink)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
