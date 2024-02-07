using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class MegeSort
    {
        static void Main(string[] args)
        {
            string line;
            if (Console.IsInputRedirected) {
                line = Console.ReadLine(); //read crazy characters
            }
            line = Console.ReadLine();
            var ar = line.Split().Select(s => int.Parse(s)).ToArray();
            MergeSort(ar);
            Console.WriteLine(string.Join(" ", ar));
        }

        //assending
        static void MergeSort<T>(T[] items) where T:IComparable
        {
            if(items.Length <= 1) {
                return;
            }
            int lSize = items.Length / 2;
            int rSize = items.Length - lSize;
            T[] left = items.Take(lSize).ToArray();
            T[] right = items.Skip(lSize).ToArray();
            //T[] right = new T[rSize];
            //Array.Copy(items, lSize, right, 0, rSize);

            MergeSort(left); //works on the copy
            MergeSort(right); //works on the copy
            Merge(items, left, right); //works on items in place
        }

        static void Merge<T>(T[] items, T[] left, T[] right) where T:IComparable
        {
            int lCount = 0, rCount = 0, iCount = 0;
            while(iCount < items.Length) {
                if(lCount >= left.Length) {
                    items[iCount] = right[rCount++];
                }else if(rCount >= right.Length) {
                    items[iCount] = left[lCount++];
                }else if(left[lCount].CompareTo(right[rCount]) <= 0) {
                    items[iCount] = left[lCount++];
                }else {
                    items[iCount] = right[rCount++];
                }
                iCount++;
            }
        }
    }
}
