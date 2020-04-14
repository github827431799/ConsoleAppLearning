using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLearning
{
    class LinqOperation
    {
        public static void TestLinq()
        {
            int[] scores = { 1, 3, 2 };
            IEnumerable<int> results = scores.OrderBy(g => g);

            foreach (int result in results)
            {
                Console.WriteLine(result);
            }


        }
    }
}
