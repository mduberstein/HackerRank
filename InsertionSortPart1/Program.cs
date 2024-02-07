using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Solution
{
    static void insertionSort(int[] ar)
    {
        //Outer loop Invariant Parts - assending
        //https://www.hackerrank.com/challenges/correctness-invariant
        //Initialization Invariant: elements to the left of the top are sorted
        //Maintenance(after each outer for cycle): elements to the left including the top are sorted
        //Termination: top is past last index - all sorted
        for (int top = 1; top < ar.Length; top++) {
            int tv = ar[top];
            int insertPosition = top;
            //inner loop invariant parts:
            //initilization: first value to analyze is one before top. Then move analysis to the left.   
            int i = top - 1;
            for (; i >= 0 && ar[i] > tv; i--) {
                    // insertPosition = i; only needed for clarity
                    // move value, greater then the top, towards the top
                    ar[i + 1] = ar[i];
            }
            // insertion action
            // ar[insertPosition] = tv;
            ar[i + 1] = tv;
            print(ar);
        }
    }

    static void print (int[] ar)
    {
        var sb = new StringBuilder(ar.Length << 2);
        for(var i = 0; i < ar.Length; i++) 
            sb.Append(i == 0 ? $"{ar[i]}" : $" {ar[i]}");
        Console.WriteLine(sb);
    }

    static void Main(String[] args)
    {
        //int _ar_size;
        //_ar_size = Convert.ToInt32(Console.ReadLine());
        //int[] _ar = new int[_ar_size];
        //String elements = Console.ReadLine();
        //String[] split_elements = elements.Split(' ');
        //for (int _ar_i = 0; _ar_i < _ar_size; _ar_i++) {
        //    _ar[_ar_i] = Convert.ToInt32(split_elements[_ar_i]);
        //}
        //int [] _ar = { 1, 4, 3, 5, 6, 2 };
        int[] _ar = {4, 1, 3, 5, 6, 2};
        //int[] _ar = { 9, 8, 6, 7, 3, 5, 4, 1, 2 };
        insertionSort(_ar);
        Console.ReadKey();
    }
}
