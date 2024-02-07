using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{

    static void Main(String[] args)
    {
        // https://www.hackerrank.com/challenges/simple-array-sum
        //array size redundant, except for submission into HackerRank
        //int n = Convert.ToInt32(Console.ReadLine());
        string[] arr_temp = Console.ReadLine().Split(' ');
        //int[] arr = Array.ConvertAll(arr_temp, Int32.Parse);
        //var sum = arr.Sum();
        var sum = arr_temp.Select(s => Int32.Parse(s)).Sum();
        Console.WriteLine(sum);
        Console.ReadKey();
    }
}
