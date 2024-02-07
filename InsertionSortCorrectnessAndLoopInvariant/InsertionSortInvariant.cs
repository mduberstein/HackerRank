using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//https://www.hackerrank.com/challenges/correctness-invariant
class Solution
{
    public static void insertionSort(int[] A)
    {
        var j = 0;
        for (var i = 1; i < A.Length; i++) {
            var value = A[i];
            j = i - 1;
            // termination invariant: after entire loop, all values from index 
            while (j >= 0 && value < A[j]) {
                A[j + 1] = A[j];
                j = j - 1;
            }
            A[j + 1] = value;
        }
        Console.WriteLine(string.Join(" ", A));
    }

    public static void insertionSort2(int[] A)
    {
        var j = 0;
        for (var i = 1; i < A.Length; i++)
        {
            var value = A[i];
            // termination invariant: after entire loop, all values from index 
            for (j = i; j > 0 &&  A[j - 1] > value; j--)
            {
                A[j] = A[j - 1];
            }
            A[j] = value;
        }
        Console.WriteLine(string.Join(" ", A));
    }

    static void Main(string[] args)
    {
        // Console.ReadLine();
        // int[] _ar = (from s in Console.ReadLine().Split() select Convert.ToInt32(s)).ToArray();
        // var line = Console.ReadLine();
        // int[] _ar = line.Split().Select(s => int.Parse(s)).ToArray();
        int[] _ar = { 1, 4, 3, 5, 6, 2 };
        //int[] _ar = { 4, 1, 3, 5, 6, 2 };
        insertionSort2(_ar);
        Console.ReadKey();
    }
}

