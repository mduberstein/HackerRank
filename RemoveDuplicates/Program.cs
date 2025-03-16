using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveDuplicates
{
    class Program
    {
        static void Main(string[] args)
        {
            #region String Reduce
            string str = "abcd";
            Console.WriteLine($"{str} original");
            string reduced = RemoveDuplicates(str);
            Console.WriteLine($"{reduced} reduced");
            str = "aaaa";
            Console.WriteLine($"{str} original");
            reduced = RemoveDuplicates(str);
            Console.WriteLine($"{reduced} reduced");
            str = null;
            Console.WriteLine($"{str ?? "null"} original");
            reduced = RemoveDuplicates(str);
            Console.WriteLine($"{reduced ?? "null"} reduced");
            str = null;
            Console.WriteLine($"{str} original");
            reduced = RemoveDuplicates(str);
            Console.WriteLine($"{reduced} reduced");
            str = "abaccdb";
            Console.WriteLine($"{str ?? "null"} original");
            reduced = RemoveDuplicates(str);
            Console.WriteLine($"{reduced ?? "null"} reduced");
            #endregion

            #region Int Reduce
            List<int[]> inputs = new List<int[]> {new int[0], new int[]{ 5 },
                new int[]{5, 5, 5}, new int[]{ 5, 2, 2, 5 }, new int []{3,4,5,6},
                new int[]{4, 9, 9, 9, 4}, new int[]{9, 9, 8, 7, 6, 9, 9 } };
            foreach(var input in inputs)
            {
                Console.WriteLine($"Input: {string.Join(" ", input)}");
                int[] output = RemoveDuplicatesAr(input);
                int[] outputH = RemoveDuplicatesHash(input);
                Console.WriteLine($"Output: {string.Join(" ", output)}");
                Console.WriteLine($"OutputH: {string.Join(" ", outputH)}");
                Console.WriteLine();
            }
            #endregion
            Console.ReadKey();
        }

        static string RemoveDuplicates(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str;
            int tail = 1;
            char[] arr = str.ToCharArray();
            for(int i = 1; i < arr.Length; ++i) {   
                // invariant - all before tail are not duplicates
                // >= than tail, but < i are duplicates, i is under test
                int j = 0;
                for( ; j < tail; j++) {
                    if (arr[j] == arr[i])
                        break;
                }
                if(j == tail) {
                    arr[tail] = arr[i];
                    ++tail;
                }
            }
            string reduced = new String(arr, 0, tail);
            return reduced;
        }

        static int[] RemoveDuplicatesAr(int[] ar)
        {
            if (ar.Count() <= 1) //could be < with the same effect
            {
                return ar;
            }
            int t = 1;
            for (int i = 1; i < ar.Length; i++) {
                // invariant - all before tail are not duplicates
                // >= than tail, but < i are duplicates, i is under test
                int j = 0;
                for (; j < t; ++j)
                {
                    if (ar[i] == ar[j])
                        break;
                }
                if (j == t) //here was the bug i > t
                {
                    ar[t] = ar[i];
                    t++;
                }
            }
            var result = new int[t];
            Array.Copy(ar, result, t);
            return result;
        }

        static int[] RemoveDuplicatesHash(int[] ar)
        {
            LinkedList<int> rl = new LinkedList<int>();
            HashSet<int> h = new HashSet<int>();
            for(int i = 0; i < ar.Length; i++)
            {
                if (!h.Contains(ar[i]))
                {
                    h.Add(ar[i]);
                    rl.AddLast(ar[i]);
                }
            }
            return rl.ToArray();
        }
    }
}
