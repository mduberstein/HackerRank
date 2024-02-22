//Strings
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

class Solution
{
    static void Main(String[] args)
    {
        // Reduce a string of lowercase characters in range ascii[‘a’..’z’]by doing a series of operations. 
        // In each operation, select a pair of adjacent letters that match, and delete them.
        // Delete as many characters as possible using this method and return the resulting string.
        // If the final string is empty, return Empty String

        // https://www.hackerrank.com/challenges/reduced-string
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        //var inputString = "aabcc";
        //var inputString = "aaabccddd";
        //var inputString = "baab";
        //var inputString = "aa";

        //var inputString = Console.ReadLine();

        var inputs = new string[] { "aaabccddd", "baab", "aa" };
        foreach (var input in inputs)
        {
            var outputString = super_reduced_string(input);
            Console.WriteLine($"Output string from {nameof(super_reduced_string)}: {outputString}");

            outputString = super_reduced_string1(input);
            Console.WriteLine($"Output string from {nameof(super_reduced_string1)}: {outputString}");
        }

        Console.ReadKey();
    }

    // foreach character in the input string
    // if the next character is the same as the currrent character
        // remove the current character and the next character
        // set the current character the next of the next character
    private static string super_reduced_string(string s)
    {
        var charAr = s.ToCharArray();
        var list = new LinkedList<char>(charAr);
        bool reduced = true;
        do {
            reduced = false;
            for (LinkedListNode<char> prev = list.First, cur = prev?.Next; cur != null;) {
                if (cur.Value == prev.Value) {
                    var delElem = prev;
                    prev = cur.Next;
                    cur = cur.Next?.Next;
                    list.Remove(delElem.Next);
                    list.Remove(delElem);
                    reduced = true;
                }
                else {
                    prev = cur;
                    cur = cur.Next;
                }
            }
        } while (reduced);
        var sb = new StringBuilder(list.Count);
        //works
        foreach (var elem in list) {
            sb.Append(elem);
        }
       
        //doesn't work for some reason - doesn't append
        //list.Select(c => sb.Append(c));
        var outputString = sb.ToString();
        if(outputString == string.Empty) {
            return "Empty String";
        }
        return outputString;
    }

    /// <summary>
    /// second version, change 4
    /// third version, change 5
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static string super_reduced_string1(string input)
    {
        if (string.IsNullOrEmpty(input)) 
            return input;

        Stack<char> stack = new Stack<char>();

        foreach (var c in input)
        {
            if (stack.Count != 0 && stack.Peek() == c)
            {
                stack.Pop();
            }
            else
            {
                stack.Push(c);
            }
        }

        if( stack.Count == 0)
        {
            return "Empty String";
        }

        char[] result = new char[stack.Count];
        for (int i = result.Length - 1; i >= 0; i--)
        {
            result[i] = stack.Pop();
        }
       
        return new string(result);
    }
}