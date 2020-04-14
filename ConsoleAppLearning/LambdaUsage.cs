using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLearning
{
    class LambdaUsage
    {
        public static void LambdaTest()
        {
            //Func
            Func<int, int> result = x => x * x;
            Console.WriteLine(result(10));

            Func<int> value = () =>
            {
                return 36;
            };
            Console.WriteLine(value());

            //Action
            Action act1 = () => Console.WriteLine(result(10));
            act1();


            Action<int> act2 = (x) => Console.WriteLine(result(x));
            act2(10);
        }
    }
}
