using System;
using System.Collections.Generic;
using System.Linq;
/*
var st = "Good Friday Today";
var stOd = "OrderMePizza"

// not present is greater
var lookup = new Dictionary<char, int>();
var i = 0;
foreach(var ch in stOd){
    lookup.Add[ch] = i++; 
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
            var st = "This is GoodFridaySaturdayMonday zbc xy w".ToLower();
            var od = "defghigklmnopqrstuv";

            var l = new Dictionary<char, int>();
            int i = 0;
            od.ToList().ForEach(x => l[x] = i++);

            var stOrdered = new string(st.OrderBy(x =>
            {
                if (!l.ContainsKey(x))
                    return 100; // not present sorts highest
                else
                    return l[x];
            }).ToArray());

            Console.WriteLine(stOrdered);
            Console.ReadLine();
        }
    }
}
