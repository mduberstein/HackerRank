using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInputTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            if (!Console.IsInputRedirected) {
                // Console.WriteLine("Print numbers with spaces, then hit enter");
            }else {
                line = Console.ReadLine(); //skip line with crazy characters
            }           
            line = Console.ReadLine();
            var strings = line.Split(' ');

            // Version 1 with error processing by outputing -1
            //var ar = new int[strings.Length];
            //for (var i = 0; i < ar.Length; i++)
            //{
            //    ar[i] = stringtoint(strings[i]);
            //}
            //var sb = new StringBuilder(ar.Length << 2);
            //for (int i = 0; i < ar.Length; i++)
            //{
            //    sb.Append($"{ar[i]}{(i != ar.Length - 1 ? " " : "")}");
            //}
            //Console.WriteLine(sb);

            ////local function
            //int stringtoint(string s)
            //{
            //    if (int.TryParse(s, out int result))
            //    {
            //        return result;
            //    }
            //    Console.WriteLine("Invalid input");
            //    return -1;
            //}
            // Version 2 with LINQ 
            int[] ints = strings.Select(int.Parse).ToArray();
            Console.WriteLine(string.Join(" ", ints));
        }
    }
}
