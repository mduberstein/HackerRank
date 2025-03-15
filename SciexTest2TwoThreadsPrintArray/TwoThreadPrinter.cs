using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciexTest2TwoThreadsPrintArray
{
    class TwoThreadPrinterWithMonitor
    {
        public TwoThreadPrinterWithMonitor() { }
        public static int Index
        {
            get { return index; }
            set { index = value; }
        }
        static int index = 0;
        static readonly object lockObj = new object();
     
        public static void PrintEvenIndexedNumbers(int[] numbers)
        {
            while (true)
            {
                lock (lockObj)
                {
                    if (index >= numbers.Length)
                        return; // Exit when all elements are printed

                    if (index % 2 == 0)
                    {
                        Console.WriteLine($"Thread 1: {numbers[index]}");
                        index++;
                        Monitor.Pulse(lockObj); // Notify the other thread
                    }
                    else
                    {
                        Monitor.Wait(lockObj); // Wait for the turn
                    }
                }
            }
        }

        public static void PrintOddIndexedNumbers(int[] numbers)
        {
            while (true)
            {
                lock (lockObj)
                {
                    if (index >= numbers.Length)
                        return; // Exit when all elements are printed

                    if (index % 2 == 1)
                    {
                        Console.WriteLine($"Thread 2: {numbers[index]}");
                        index++;
                        Monitor.Pulse(lockObj); // Notify the other thread
                    }
                    else
                    {
                        Monitor.Wait(lockObj); // Wait for the turn
                    }
                }
            }
        }
    }
}
