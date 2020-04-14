using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLearning
{
    class CustomizationCollection<X>
    {
        //Property
        public X Item;

        //Constructor
        public CustomizationCollection(X x)
        {
            Item = x;
        }

        //Get method
        public void Get()
        {
            Console.WriteLine(Item);
        }
    }
}
