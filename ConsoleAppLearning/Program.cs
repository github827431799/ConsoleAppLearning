using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            //Process words in parallel
            //MultipleThread.ProcessWordParallel();
            //string word = "abc‘";
            //Console.WriteLine(word.Replace("‘",""));

            //Process characters in parallel
            //MultipleThread.ProcessCharacterParallel();

            //Customize collection class like List<T>
            //CustomizationCollection<int> gClassI = new CustomizationCollection<int>(10000);
            //CustomizationCollection<string> gClassStr = new CustomizationCollection<string>("Book");
            //gClassI.Get();
            //gClassStr.Get();

            //Lambda
            //LambdaUsage.LambdaTest();

            //Calculate PI by Monte Carlo method
            //Pi.GetByMonteCarlo();

            //Calculate PI by P1
            Pi.GetByP1();

            //Test query and method for LINQ
            //LinqOperation.TestLinq();


            Console.ReadKey();
        }
    }
}
