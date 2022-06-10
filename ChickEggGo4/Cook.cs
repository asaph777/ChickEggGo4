using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickEggGo4
{
    
    class Cook
    {
        public delegate void proceedDelegate();

        public event proceedDelegate Processed;

        public void Process(TableRequests tableRequests)
        {
            foreach (Chicken item in tableRequests.Get<Chicken>())
            {
                item.Obtain();
                item.CutUp();
                item.Cook();
            }
            foreach (Egg item in tableRequests.Get<Egg>())
            {
                item.Obtain();
                item.Crack();
                item.Discard();
                item.Cook();
                item.Dispose();
            }
            Processed();
        }
    }
}
