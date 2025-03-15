using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

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
            index = FindPatternInStringKMP(search, pattern);
            Console.WriteLine($"KMP Searched {search}. Found {pattern} at index {index}{Environment.NewLine}");

            search = "ababababcababbca";
            pattern = "ababcabab";
            ret = Substring(search, pattern);
            // Searched aaabca. Found abc at index 2
            Console.WriteLine($"Searched {search}. Found {ret.sub} at index {ret.index}");
            index = SubstringBrute(search, pattern);
            Console.WriteLine($"Brute Searched {search}. Found {pattern} at index {index}{Environment.NewLine}");
            index = FindPatternInStringKMP(search, pattern);
            Console.WriteLine($"KMP Searched {search}. Found {pattern} at index {index}{Environment.NewLine}");

            search = "aaabca";
            pattern = "bca";
            ret = Substring(search, pattern);
            // Searched aaabca. Found bca at index 3
            Console.WriteLine($"Searched {search}. Found {ret.sub} at index {ret.index}");
            index = SubstringBrute(search, pattern);
            Console.WriteLine($"Brute Searched {search}. Found {pattern} at index {index}{Environment.NewLine}");
            index = FindPatternInStringKMP(search, pattern);
            Console.WriteLine($"KMP Searched {search}. Found {pattern} at index {index}{Environment.NewLine}");

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
            //              return i
            //          increment j;
            //      else
            //          set j to 0 and break;
            // return -1;
            for(int i = 0; i <= search.Length - pattern.Length; i++)
            {
                for(int j = 0; j < pattern.Length; j++)
                {
                    if (search[i + j] != pattern[j])
                    {
                        break;
                    }
                    if (j == pattern.Length - 1)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        //Optimized O(n) Solution Using Knuth-Morris-Pratt(KMP) Algorith
        //KMP preprocessing builds a partial match(LPS - Longest Prefix Suffix) table, allowing efficient skipping of unnecessary comparisons.
        // Time Complexity: O(n + m) where n is the length of searched and m is the length of pattern
        // pattern = "ababcabab" lpi = {0,0,1,2,0,1,2,3,4} // j
        // search = "ababababcababbca"; // i 
        // i = 4, j = 4, search[i] = 'a',  pattern[j] = 'c', j = lps[3] = 2
        // i = 4, j = 2, search[i] = 'a',  pattern[j] = 'a'
        // i = 5, j = 3, search[i] = 'b',  pattern[j] = 'b'
        // i = 6, j = 4, search[i] = 'a',  pattern[j] = 'c, j = lps[3] = 2
        // i = 6, j = 2, search[i] = 'a',  pattern[j] = 'a'
        // i = 7, j = 3, search[i] = 'b',  pattern[j] = 'b'
        // i = 8, j = 4, search[i] = 'c',  pattern[j] = 'c'
        // i = 9, j = 5, search[i] = 'a',  pattern[j] = 'a'
        // i = 10, j = 6, search[i] = 'b',  pattern[j] = 'b'
        // i = 11, j = 7, search[i] = 'a',  pattern[j] = 'a'
        // i = 12, j = 8, search[i] = 'b',  pattern[j] = 'b', j == 9 & pattern.Length == 9, return i - j = 4;
        public static int FindPatternInStringKMP(string searched, string pattern)
        {
            if (string.IsNullOrEmpty(searched) || string.IsNullOrEmpty(pattern))
                return -1;

            int[] lps = ComputeLPSArray(pattern);
            int i = 0, j = 0; // i for searched, j for pattern

            while (i < searched.Length)
            {
                if (searched[i] == pattern[j])
                {
                    //i++; j++;
                    //if (j == pattern.Length)
                    //    return i - j; // Found pattern, return start index
                    // Alternative
                    if(j == pattern.Length - 1)
                    {
                        return i - j - 1;
                    }
                    i++; j++;
                }
                else
                {
                    if (j != 0)
                        // Set j to the previous LPS value, so that we can skip unnecessary comparisons
                        // as we know that the previous characters in the prefix in the pattern already matched the suffix in the search string
                        j = lps[j - 1]; // Use LPS array to skip comparisons
                    else
                        i++;
                }
            }

            return -1; // Pattern not found
        }
        // example: pattern = "ababcabab":
        // len | i | lpi
        // 0   | 1 | {0,0}
        // 0   | 2 | {0,0,1}
        // 1   | 3 | {0,0,1,2}
        // 2   | 4 | {0,0,1,2} len = 1
        // 1   | 4 | {0,0,1,2} len = 0
        // 0   | 4 | {0,0,1,2,0}
        // 0   | 5 | {0,0,1,2,0,1}
        // 1   | 6 | {0,0,1,2,0,1,2}
        // 2   | 7 | {0,0,1,2,0,1,2,3}
        // 3   | 8 | {0,0,1,2,0,1,2,3,4}
        // Longest prefix suffix
        private static int[] ComputeLPSArray(string pattern)
        {
            int[] lps = new int[pattern.Length];
            int len = 0; // length of previous LPS
            int i = 1;

            while (i < pattern.Length)
            {
                // invariant: compare with current with character immediately past the previous LPS
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                        len = lps[len - 1]; // Reduce LPS length
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
            return lps;
        }

        //public int index FindPinS(string s, string p)
        //{
        //    if(string.IsNullOrEmpty(s) || string.IsNullOrEmpty(p))
        //        return -1;
        //    int[] lps = ComputeLPSArray(p);
        //    int i = 0, j = 0;
        //    for(int )

        //}
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
