using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
/*
var st = "Good Friday Today";
var stOd = "OrderMePizza"

// not present is greater
var lookup = new Dictionary<char, int>();
var i = 0;
foreach(var ch in stOd){
    lookup[ch] = i++; 
}
st.ToCharArray().Sort((char a, char b)=>{
    if (!lookup.ContainsKey(a))
    {
        if (!lookup.ContainsKey(b){
            return 0;
        }
        else
            return 1;
    }
    else
    {
        if (!lookup.ContainsKey(b)
            return -1;
        else if (lookup[a] == lookup[b])
            return 0;
        else if (l[a] < l[b])
            return -1;
        else
            return 1;
    }
}).ToString();
*/


namespace SortOneStringByAnother
{
    class Program
    {
        static void Main(string[] args)
        {
            string st = "This is GoodFridaySaturdayMonday zbc xy w".ToLower();
            const string od = "defghiklmnopqrstuv";
            string stOrderedChat = SortStringByOrderLinqChatGPT(st, od);
            Console.WriteLine($"Ordered with LinqChatGPT{Environment.NewLine}{stOrderedChat}");
            string stOrderedLinq = SortStringByOrderLinq(st, od);
            Console.WriteLine("Ordered using LINQ");
            Console.WriteLine(stOrderedLinq);
            string stOrdered = SortStringByOrder(st, od);
            Console.WriteLine($"Ordered without LINQ{Environment.NewLine}{stOrdered}");

            Console.ReadLine();
        }

        static string SortStringByOrderLinqChatGPT(string input, string order)
        {
            Dictionary<char, int> priorityMap = order
                .Select((ch, index) => new { ch, index })
                .ToDictionary(x => x.ch, x => x.index);
            // ThenBy only sorts what does has an equal sorting key, i.e. int.MaxValue, not all characters in the sequence      
            // Output: ddddfghiiimnooorrsssttu     aaaabcwxyyyyz
            var sortedChars = input
                .OrderBy(ch => priorityMap.ContainsKey(ch) ? priorityMap[ch] : int.MaxValue)
                .ThenBy(ch => ch);

            return new string(sortedChars.ToArray());
        }
        static string SortStringByOrderLinq(string st, string od)
        {
            var l = new Dictionary<char, int>();
            int i = 0;
            od.ToList().ForEach(x => {
                if (l.ContainsKey(x))
                {
                    throw new ArgumentException("Duplicate character in order string");
                }
                else
                {
                    l[x] = i++;
                }
            });
            // ThenBy only sorts what does has an equal sorting key, i.e. int.MaxValue, not all characters in the sequence      
            // Output: ddddfghiiimnooorrsssttu     aaaabcwxyyyyz
            var stOrdered =
                new string(st.OrderBy(x =>
                {
                    if (!l.ContainsKey(x))
                        return int.MaxValue; // not present sorts highest
                    else
                        return l[x];
                }).ThenBy(x => x).ToArray());
            return stOrdered;
        }

        static string SortStringByOrder(string st, string od)
        {
            var ar = st.ToCharArray();
            Array.Sort(ar, new Orderer(od));
            return new string(ar);
        }

        class Orderer: IComparer<char>
        {
            private readonly Dictionary<char, int> l = new Dictionary<char, int>();
            public Orderer(string od)
            {
                int i = 0;
                foreach (char x in od)
                {
                    if (l.ContainsKey(x))
                    {
                        throw new ArgumentException("Duplicate character in order string");
                    }
                    else
                    {
                        l[x] = i++;
                    }
                }
            }
            public int Compare(char x, char y)
            {
                if (!l.ContainsKey(x))
                {
                    if (!l.ContainsKey(y))
                    {
                        return x.CompareTo(y);
                    }
                    else
                        return 1;
                }
                else
                {
                    if (!l.ContainsKey(y))
                        return -1;
                    else
                        return l[x].CompareTo(l[y]);
                }
            }
        }
    }
}
