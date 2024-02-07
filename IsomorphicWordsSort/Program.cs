using System;
using System.Collections.Generic;
using System.Text;

namespace IsomorphicWordsSort
{
    // Amazon face to face test
    class Program
    {
        /*Isomorphic words are words that were one word can be obtained from another by mapping, ex. abba from cddc, a to c, d to b*/
        /* Output
Length limit 13 letters for key of type long
{abbc, kllm, tvvw}, {zaaa, xyyy}, {zzzc}, {ccmm}, {abcdefghijkli, ABCDEFGHIJKLI}, {abcdefghijkl}

Length limit is the length of the keyMapArray
{abbc, kllm, tvvw}, {zaaa, xyyy}, {zzzc}, {ccmm}, {abcdefghijklmnopqrstuvwxyt, ABCDEFGHIJKLMNOPQRSTUVWXYT}, {abcdefghijklmnopqrstuvwxyz}
*/
        static void Main(string[] args)
        {
            // the word length limit for the key being long (64 bit) is approximately 13 characters,
            // with this algorithm
            var inputListLengthLimited = new List<string> { "abcdefghijkli", "ABCDEFGHIJKLI", "abcdefghijkl",
                "abbc", "zaaa", "xyyy", "zzzc", "kllm", "ccmm", "tvvw"};
            var outListLengthLimited = GroupIsomorphicWordsLengthLimited(inputListLengthLimited);
            Console.WriteLine("Length limit 13 letters for key of type long");
            PrintWordsList(outListLengthLimited);


            // word length limit is the length of the keyMapArray below
            var inputList = new List<string> { "abcdefghijklmnopqrstuvwxyt", "ABCDEFGHIJKLMNOPQRSTUVWXYT", "abcdefghijklmnopqrstuvwxyz",
                "abbc", "zaaa", "xyyy", "zzzc", "kllm", "ccmm", "tvvw"
            };
            var outList = GroupIsomorphicWords(inputList);
            Console.WriteLine($"{Environment.NewLine}Length limit is the length of the keyMapArray");
            PrintWordsList(outList);
        }

        static LinkedList<List<string>> GroupIsomorphicWordsLengthLimited(List<string> words)
        {
            // var ret = new LinkedList<List<string>>();
            var wordListLookup = new Dictionary<long, List<string>>(words.Count);
            foreach (var w in words)
            {
                var key = GetIsomorphicKeyLengthLimited(w);
                if (!wordListLookup.TryGetValue(key, out List<string> list))
                {
                    list = new List<string>();
                }
                list.Add(w);
                wordListLookup[key] = list;
            }
            var ret = new LinkedList<List<string>>(wordListLookup.Values);
            //foreach (var value in wordListLookup.Values)
            //{
            //    ret.AddLast(value);
            //}
            return ret;
        }

        static LinkedList<List<string>> GroupIsomorphicWords(List<string> words)
        {
            var ret = new LinkedList<List<string>>();
            var wordListLookup = new Dictionary<string, List<string>>(words.Count);
            foreach (var w in words)
            {
                var key = GetIsomorphicKey(w);
                if (!wordListLookup.TryGetValue(key, out List<string> list))
                {
                    list = new List<string>();
                }
                list.Add(w);
                wordListLookup[key] = list;
            }

            foreach (var value in wordListLookup.Values)
            {
                ret.AddLast(value);
            }
            return ret;
        }

        // MOST IMPORTANT METHOD 1
        // 
        static long GetIsomorphicKeyLengthLimited(string w)
        {
            if (w.Length > 13)
            {
                throw new ArgumentException($"{nameof(w)} too long for the key of type long");
            }
            var sb = new StringBuilder(w.Length);
            var dict = new Dictionary<char, int>();
            for (int i = 1; i <= w.Length; i++)
            {
                int keyValue;
                if (dict.TryGetValue(w[i - 1], out keyValue))
                {
                    sb.Append(keyValue);
                }
                else
                {
                    dict[w[i - 1]] = i;
                    sb.Append(i);           // IMPORTANT
                }

            }
            return long.Parse(sb.ToString());
        }

        static char[] keyMapArray = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        // MOST IMPORTANT METHOD 2
        static string GetIsomorphicKey(string w)
        {
            // irrelevant, different characters matter, not the 
            //if (w.Length > keyMapArray.Length)
            //{
            //    throw new ArgumentException($"{nameof(w)} too long for the {nameof(keyMapArray)} length");
            //}

            var sb = new StringBuilder(w.Length);
            var dict = new Dictionary<char, char>();
            for (int i = 0; i < w.Length; i++)
            {
                char keyValue;
                if (dict.TryGetValue(w[i], out keyValue))
                {
                    sb.Append(keyValue);
                }
                else
                {
                    dict[w[i]] = keyMapArray[i];
                    sb.Append(keyMapArray[i]);           // IMPORTANT
                }

            }
            return sb.ToString();
        }

        static void PrintWordsList(LinkedList<List<string>> list)
        {
            var sb = new StringBuilder();
            int i = 0;
            foreach (var subList in list)
            {
                i++;
                sb.Append($"{{{string.Join(", ", subList)}}}{(i < list.Count ? ", " : "")}");
            }
            Console.WriteLine(sb.ToString());
        }
    }
}







