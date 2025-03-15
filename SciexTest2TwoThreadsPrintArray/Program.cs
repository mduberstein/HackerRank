// See https://aka.ms/new-console-template for more information
using SciexTest2TwoThreadsPrintArray;

Console.WriteLine("With Monitor");

int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
TwoThreadPrinterWithMonitor.Index = 0;
Thread t1 = new Thread(() => TwoThreadPrinterWithMonitor.PrintEvenIndexedNumbers(numbers));
Thread t2 = new Thread(() => TwoThreadPrinterWithMonitor.PrintOddIndexedNumbers(numbers));

t1.Start();
t2.Start();

t1.Join();
t2.Join();

Console.WriteLine("With AutoReset Events");
Thread t3 = new Thread(() => TwoThreadPrinterWithEvent.PrintEvenIndexedNumbers(numbers));
Thread t4 = new Thread(() => TwoThreadPrinterWithEvent.PrintOddIndexedNumbers(numbers));

t3.Start();
t4.Start();

t3.Join();
t4.Join();

