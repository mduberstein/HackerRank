using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Solution
{
    // Building block 1
    static void swap(int[] ar, int i, int j)
    {
        int t = ar[i];
        ar[i] = ar[j];
        ar[j] = t;
    }

    // Optional Visualization building block 2
    static void print(int[] ar, int low, int high)
    {
        var sb = new StringBuilder();
        for(int i = low; i <= high; i++) {
            sb.Append($"{ar[i]}{(i != high ? " ": "")}");
        }
        Console.WriteLine(sb);
    }

    // Building block 3
    // prerequisite for this call is low < high !!!
    static int partitionInPlace(int[] ar, int low, int high, int pivot)
    {
        if (pivot != low) {
            swap(ar, low, pivot);
            pivot = low;
        }
        low++; // maintenance invariant 1 (defined below) is certainly true after this line!!!
               // maintenance invariant 1, i.e. statement true after each run of the loop: ar[i] <= ar[pivot] for i < low, 
               // maintenance invariant 2: i.e. statement true after each run of the loop: ar[i] > ar[pivot] for any valid i > high, 
               // valid word is important since at the begining i > high is beyound range
        
        // because of prerequisite the loop is guaranted to run at least one time
        while (low <= high) { //not strict is important here, < would be incorrect
            if (ar[low] <= ar[pivot]) { // important that this is the first if statement
                low++;
            }
            else if (ar[pivot] < ar[high]) {
                high--;
            }
            else {
                swap(ar, low, high);
            }
        }
        // since pivot is out of way in the left part of sorted range, must swap with high, not low
        // validity of swapping with high at end (i.e. when high < low) is based on maintenance invariant 1 being correct before the first run of the loop and after any run of the loop
        // validity of swapping with low at end (i.e. when low > high) has no basis, since it would be potentially putting value greater than ar[pivot] to the left of pivot !!!!  
       
        if (pivot != high) {
            swap(ar, pivot, high);
        }
        return high;
    }

    // option for Building Block 3
    //additional invariant to partitionInPlace is: existing order is maintained for i < pivot and i > pivot
    static int partitionWithOrderPreserved(int[] ar, int low, int high, int pivot)
    {
        if (pivot != low) {
            swap(ar, low, pivot);
            pivot = low;
        }
        var left = new LinkedList<int>(); //value <= than ar[pivot]
        var right = new LinkedList<int>();//value > than ar[pivot]
        int i = low + 1;
        for(; i <= high; i++) {
            if(ar[i] <= ar[pivot]) {
                left.AddLast(ar[i]); 
            }else {
                right.AddLast(ar[i]);
            }
        }

        var pivotValue = ar[pivot];
        i = low;
        var cur = left.First;
        while (cur != null) {
            ar[i++] = cur.Value;
            cur = cur.Next;
        }
        pivot = i;
        ar[i++] = pivotValue;
        cur = right.First;
        while (cur != null) {
            ar[i++] = cur.Value;
            cur = cur.Next;
        }

        return pivot;
    }

    // Actual algorithm implementation !!!
    // Algorithm statement !!!
    // for an array of size greater than 1
    // 1. Choose pivot index anywhere in the array, low, high, (low+high)/2 doesn't matter
    // 2. Execute partitioning step: i.e. ensure correctness of the termination invariant
    //          ar[i] <= ar[pivot] for i<pivot
    //          and ar[i] > ar[pivot] for i > pivot
    // after partitioning
    // 3. reqursively execute the same for left and right of the pivot

    static void qSortImpl(int[] ar, int low, int high)
    {
        if (low < high) {
            int pivot = low; 
            pivot = partitionInPlace(ar, low, high, pivot);
            qSortImpl(ar, low, pivot - 1);
            qSortImpl(ar, pivot + 1, high);
            print(ar, low, high);
        }
    }

    static void qSortImplWithPartitionOrderPreserved(int[] ar, int low, int high)
    {
        if (low < high) {
            int pivot = low;
            pivot = partitionWithOrderPreserved(ar, low, high, pivot);
            qSortImplWithPartitionOrderPreserved(ar, low, pivot - 1);
            qSortImplWithPartitionOrderPreserved(ar, pivot + 1, high);
            print(ar, low, high);
        }
    }

    // The start !!!
    // this is a recursive algorighm
    static void quickSort(int[] ar)
    {
        //qSortImpl(ar, 0, ar.Length - 1); //usual use of swapping when partitioning
        qSortImplWithPartitionOrderPreserved(ar, 0, ar.Length - 1);
    }
    /* Tail starts here */
    static void Main(String[] args)
    {
        //using (var reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding)) {
        //    int _ar_size;
        //    string line = Console.ReadLine();
        //    _ar_size = Convert.ToInt32(line);
        //    int[] _ar = new int[_ar_size];
        //    String elements = Console.ReadLine();
        //    String[] split_elements = elements.Split(' ');
        //    for (int _ar_i = 0; _ar_i < _ar_size; _ar_i++) {
        //        _ar[_ar_i] = Convert.ToInt32(split_elements[_ar_i]);
        //    }

        //    quickSort(_ar);
        //}

        int _ar_size;
        string line = Console.ReadLine();
        _ar_size = Convert.ToInt32(line);
        int[] _ar = new int[_ar_size];
        String elements = Console.ReadLine();
        String[] split_elements = elements.Split(' ');
        for (int _ar_i = 0; _ar_i < _ar_size; _ar_i++) {
            _ar[_ar_i] = Convert.ToInt32(split_elements[_ar_i]);
        }
        //int[] _ar = {5, 8, 1, 3, 7, 9, 2 };
        quickSort(_ar);
    }
}
