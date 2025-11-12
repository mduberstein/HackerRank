using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


////https://www.hackerrank.com/challenges/countingsort1
//Comparison Sorting
//Quicksort usually has a running time of NLog(N), but is there an algorithm that can sort even faster?
//In general, this is not possible. Most sorting algorithms are comparison sorts, i.e. they sort a list just by comparing the elements to one another.
//A comparison sort algorithm cannot beat  (worst-case) running time,
//since  represents the minimum number of comparisons needed to know where to place each element. For more details, you can see these notes (PDF).

//Alternative Sorting
//Another sorting method, the counting sort, does not require comparison. Instead, you create an integer array whose index range covers the entire range of values in your array to sort.
//Each time a value occurs in the original array, you increment the counter at that index.
//At the end, run through your counting array, printing the value of each non-zero valued index that number of times.

//Example

//All of the values are in the range, so create an array of zeros, . The results of each iteration are in "result" column.

//i     arr[i]	result
//0	    1	    [0, 1, 0, 0]
//1     1       [0, 2, 0, 0]
//2     3       [0, 2, 0, 1]
//3     2       [0, 2, 1, 1]
//4     1       [0, 3, 1, 1]
//The frequency array is result. These values can be used to create the sorted array as well: .

//Note
//For this exercise, always return a frequency array with 100 elements. The example above shows only the first 4 elements, the remainder being zeros.

//Challenge
//Given a list of integers, count and return the number of times each value appears as an array of integers.

//Function Description

//Complete the countingSort function in the editor below.

//countingSort has the following parameter(s):

//arr[n]: an array of integers
//Returns

//int[100]: a frequency array
//Input Format

//The first line contains an integer , the number of items in .
//Each of the next  lines contains an integer  where .

//Constraints

class Solution
{
    static void Main()
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        string line;
        if (Console.IsInputRedirected) {
            line = Console.ReadLine(); //skip line with crazy characters - encoding ?
        }
        else {
            // Console.WriteLine("Print numbers with spaces, then hit enter");
        }
        line = Console.ReadLine();
        line = Console.ReadLine();
        // only positive integers are assumed
        int[] _ar = line.Split().Select(s => int.TryParse(s, out int val) ? val : -1).ToArray();
        //int[] countAr = new int[_ar.Length];
        //for (int i = 0; i < _ar.Length; i++) {
        //    (countAr[_ar[i]])++;
        //}
        //string outStr = string.Join(" ", countAr.Take(100));
        // Do only for the first 100 for testing
        var inAr100 = _ar.Take(100).ToArray();
        int[] count100 = new int[inAr100.Max() + 1];
        foreach(var n in inAr100)
        {
            (count100[n])++;
        }
        List<int> outList = new List<int>(100);
        for(int i = 0, count = 0; i < count100.Length; i++)
        {
            count = count100[i];
            for(int j = 0; j < count; j++)
            {
                outList.Add(i);
            }
        }
        string inStr = string.Join(" ", inAr100);
        string outStr = string.Join(" ", outList);
        // Old code
        //var sb = new StringBuilder(100);
        
        //foreach(var c in count100)
        //{
        //    sb.Append($"{countAr[n]} ");
        //}

        //string outStr = sb.ToString();
        Console.WriteLine(inStr);
        Console.WriteLine();
        Console.WriteLine(outStr);
        Console.ReadKey();
    }
}