using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SubstringAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var search = "aacb";
            var pattern = "ac";
            var ret = Substring(search, pattern);
            // Searched aacb. Found ac at index 
            Console.WriteLine($"Searched {search}. Found {ret.sub} at index {ret.index}");
            var index = SubstringBrute(search, pattern);
            Console.WriteLine($"Brute Searched {search}. Found {pattern} at index {index}{Environment.NewLine}");

            search = "aaabca";
            pattern = "abc";
            ret = Substring(search, pattern);
            // Searched aaabca. Found abc at index 2
            Console.WriteLine($"Searched {search}. Found {ret.sub} at index {ret.index}");
            index = SubstringBrute(search, pattern);
            Console.WriteLine($"Brute Searched {search}. Found {pattern} at index {index}{Environment.NewLine}");

            search = "aaabca";
            pattern = "bca";
            ret = Substring(search, pattern);
            // Searched aaabca. Found bca at index 3
            Console.WriteLine($"Searched {search}. Found {ret.sub} at index {ret.index}");
            index = SubstringBrute(search, pattern);
            Console.WriteLine($"Brute Searched {search}. Found {pattern} at index {index}{Environment.NewLine}");

            Console.ReadLine();
        }

        static (string sub, int index) Substring(string search, string pattern)
        {

            int index = -1;
            var states = new SearchStates { Pattern = pattern, Positions = new HashSet<int>() };
            var ret = (MinSuffixLength: pattern.Length, Matched: false);

            int i = 0;
            for (; !ret.Matched && i <= search.Length - ret.MinSuffixLength; i++)
            {
                ret = states.GetSearchStates(search[i]);
            }
            if (ret.Matched)
            {
                index = i - pattern.Length;
            }
            return (pattern, index);
        }
        
        static int SubstringBrute(string search, string pattern)
        {
            // for each position i in search, starting with 0
            //  while the remaining number of characters in search is greater or equal to pattern.Length
            // for each position j in pattern starting with 0
            //      if search[i + j] == pattern[j]
            //          if j == pattern.Length - 1,
            //              return i - pattern.Length + 1
            //          increment j;
            //      else
            //          set j to 0 and break;
            // return -1;
            for(int i = 0; i <= search.Length - pattern.Length; i++)
            {
                for(int j = 0; j < pattern.Length; j++)
                {
                    if (search[i + j] == pattern[j])
                    {
                        if (j == pattern.Length - 1)
                            return i;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return -1;
        }
    }

    class SearchStates
    {
        public string Pattern;
        public HashSet<int> Positions;

        // returns true if match found
        public (int MinSuffixLength, bool Matched) GetSearchStates(char x)
        {
            int minSuffixLength = Pattern.Length;
            var positions = Positions.ToList(); // cannot change HashSet while iterating over it and need a fresh one anyway
            Positions = new HashSet<int>();
            if (x == Pattern[0])
            {
                if (1 == Pattern.Length)
                    return (0, true);

                Positions.Add(1);
                minSuffixLength -= 2;
            }
            foreach (var pos in positions)
            {
                if (x == Pattern[pos])
                {
                    if (pos + 1 == Pattern.Length) // matched the entire length
                        return (0, true);

                    Positions.Add(pos + 1);
                    minSuffixLength -= (pos + 1);

                }
            }
            return (minSuffixLength, false);
        }
    }
}
