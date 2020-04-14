using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLearning
{
    class Pi
    {
        public static void GetByMonteCarlo()
        {
            long hitSum = 0;
            long totalSum = 2000000000; 
            Random r = new Random();
            int totalThread = 8;
            Task[] taskArray = new Task[totalThread];
            Object obj = new Object();
            Object objRandom = new Object();

            DateTime startTime = DateTime.Now;
            for (int t = 0; t < totalThread; t++)
            {                
                taskArray[t] = Task.Run(() =>
                {
                    Double x;
                    Double y;
                    long hitSumTask = 0;
                    for (int i = 0; i < totalSum; i++)
                    {
                        lock (objRandom)
                        {
                            x = r.NextDouble();
                            y = r.NextDouble();
                        }
                        //Console.WriteLine(x);
                        //Console.WriteLine(y);
                        if (x * x + y * y <= 1.0)
                        {
                            hitSumTask++;
                            
                        }
                    }
                    lock (obj)
                    {
                        hitSum = hitSum + hitSumTask;
                    }
                });

            }

            Task.WaitAll(taskArray);
            DateTime endTime = DateTime.Now;

            Console.WriteLine("Pi constant:" + Math.PI);
            Console.WriteLine("Pi         :" + 4.0 * hitSum / (totalSum * totalThread));
            Console.WriteLine(endTime - startTime);

        }

        //Pi/4 = 1 - 1/3 + 1/5 - 1/7 + 1/9 - 1/11 + 1/13 - 1/15 + 1/17 - 1/19 + 1/21......
        public static void GetByP1()
        {
            long loopNumber = 10000000;
            decimal totalSum = 0.0m;
            int totalThread = 8;
            Task[] taskArray = new Task[totalThread];
            Object obj = new Object();

            DateTime startTime = DateTime.Now;
            for (int t = 0; t < totalThread; t++)
            {
                taskArray[t] = Task.Run(() =>
                {
                    decimal subTotal = 0.0m;
                    decimal signal = 1.0m;
                    int subLoopNumber = (int)Task.CurrentId - 1;

                    for (long i = subLoopNumber * loopNumber + 1; i <= (subLoopNumber + 1) * loopNumber; i++)
                    {
                        subTotal += signal * 1 / (2 * i - 1);
                        signal = -signal;
                        //Console.WriteLine("#" + Task.CurrentId + ":" + (subLoopNumber * loopNumber + 1) + "->" + ((subLoopNumber + 1) * loopNumber));
                
                    }
                    Console.WriteLine("#" + Task.CurrentId + ":" + subTotal); 
                    lock (obj)
                    {
                        totalSum = totalSum + subTotal;
                    }

                });

            }

            Task.WaitAll(taskArray);
            DateTime endTime = DateTime.Now;

            Console.WriteLine("Pi constant:" + Math.PI);
            Console.WriteLine("Pi         :" + 4 * totalSum);
            Console.WriteLine(endTime - startTime);

        }

    }
}
