using System;

namespace ReverseStringRecursive
{
    class Program
    {
        static void Main(string[] args)
        {
            string original = "Geeks for Geeks";
            string reversed = ReverseStringRecursive(original);
            Console.WriteLine(reversed);
            Console.ReadLine();
        }

        static string ReverseStringRecursive(string org)
        {
            if (org.Length <= 1)
                return org;
            return org.Substring(org.Length - 1) + ReverseStringRecursive(org.Substring(0, org.Length - 1));
        }
    }
}
