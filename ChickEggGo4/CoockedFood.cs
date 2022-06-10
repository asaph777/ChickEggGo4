using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickEggGo4
{
    abstract class CoockedFood : IMenuItem
    {  
        public void Cook() { }
        public void Obtain(){}
        public virtual void Prepare() { }
        public void Serve(){}
    }
}
