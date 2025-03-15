using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciexTest2TwoThreadsPrintArray
{
    class TwoThreadPrinterWithEvent
    {
        public static int Index { get; set; } = 0;
        static AutoResetEvent oddEvent = new AutoResetEvent(false);
        static AutoResetEvent evenEvent = new AutoResetEvent(true);

        public static void PrintEvenIndexedNumbers(int[] numbers)
        {
            while (true)
            {
                evenEvent.WaitOne();
                if (Index >= numbers.Length)
                    return; // Exit when all elements are printed
                if (Index % 2 == 0)
                {
                    Console.WriteLine($"Thread 1: {numbers[Index]}");
                    Index++;
                    oddEvent.Set(); // Notify the other thread
                }
            }
        }

        public static void PrintOddIndexedNumbers(int[] numbers)
        {
            while (true)
            {
                oddEvent.WaitOne();
                if (Index >= numbers.Length)
                    return; // Exit when all elements are printed
                if (Index % 2 == 1)
                {
                    Console.WriteLine($"Thread 2: {numbers[Index]}");
                    Index++;
                    evenEvent.Set(); // Notify the other thread
                }
            }
        }
    }
}
