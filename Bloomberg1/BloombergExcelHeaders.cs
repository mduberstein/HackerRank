using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergExcelHeaders
{
    class Solution
    {
        static void Main(string[] args)
        {
            string line;
            if (!Console.IsInputRedirected) {
                while (true) {
                    Console.WriteLine("Print positive integer starting from 1 or -1 to exit, then hit enter");
                    //For Octal - didn't get to work yet
                    //Console.WriteLine("Print non-negative integer starting from 1 or -1 to exit, then hit enter");
                    line = Console.ReadLine();
                    int num = Convert.ToInt32(line);
                    if (num < 0)
                        break;
                    //tested, works
                    //string header = StackToString(NumberToHeader(num));
                    //tested, works, simplest
                    string header = GetExcelColumnName(num);
                    //string header = StackToString(NumberToOctal(num));
                    Console.WriteLine(header);
                }
            }
            else {
                line = Console.ReadLine(); //skip line with crazy characters
            }
            line = Console.ReadLine();
        }

        //tested, works
        static string StackToString(Stack<char> st)
        {
            StringBuilder sb = new StringBuilder();
            while (st.Count > 0) {
                sb.Append(st.Pop());
            }
            return sb.ToString();
        }

        static Stack<char> NumberToOctal(int n)
        {
            const int b = 8;          
            Stack<char> st = new Stack<char>();

            do {
                int d = n % b;
                char c = (char)('0' + d);
                st.Push(c);
                n /= b;
            } while (n > 0);
            return st;
        }
        /// <summary>
        /// one based
        /// </summary>
        /// <param name="n">number of columns</param>
        /// <returns></returns>
        static Stack<char> NumberToHeader(int n)
        {
            if(n <= 0) {
                throw new ArgumentException($"Invalid argument {n}! Must be non-negative integer");
            }
            //n += 1; //make it number of columns from the last column zero based index
            const int b = 26;
            Stack<char> st = new Stack<char>();
            //1:26 A:Z, 27:52 AA:AZ,53:78 BA:BZ,.. 651:676 YA:YZ, 677:702 ZA:ZZ - one based
            //0:25 A:Z, 26:51 AA:AZ,52:77 BA:BZ,.. 650:675 YA:YZ
            char c;
            int d;
            do {
                d = (n - 1) % b;
                c = (char)('A' + d);
                st.Push(c);
                n = (n - d) / b;
            } while (n > 0);

            return st;
        }

        //tested works, simplest
        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0) {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
    }
}
