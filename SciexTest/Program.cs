using System;
using System.Collections.Generic;
using System.Linq;

//flanked
//https://stackoverflow.com/questions/19437855/algorithm-for-finding-a-group-of-numbers-in-a-list-that-equal-a-target
//https://en.wikipedia.org/wiki/Subset_sum_problem
//https://stackoverflow.com/questions/16604985/find-elements-in-a-list-that-together-add-up-to-a-target-number
//https://stackoverflow.com/questions/999050/how-to-get-all-subsets-of-an-array/999182#999182
//https://stackoverflow.com/questions/32699432/find-subset-of-numbers-that-add-up-to-a-given-number

class Solution
{
    static void Main()
    {
        // Works
        //Calculator.GetCombinations(new List<int> {3, 5, 7, 9 }).ForEach(
        //    combination =>
        //    {
        //        combination.ForEach(m => Console.Write($"{m} "));
        //        Console.WriteLine($"{Environment.NewLine}");
        //    }
        //);

        // Tested to work
        bool found;
        var targetSum = 16;
        found = PrintSequenceForTargetSum(targetSum);
        targetSum = 15;
        found = PrintSequenceForTargetSum(targetSum);
        targetSum = 17;
        found = PrintSequenceForTargetSum(targetSum);
        targetSum = 8;
        found = PrintSequenceForTargetSum(targetSum);

        Console.ReadKey();


        // Combinations doesn't exist
        //List<int> myList = new List<int>() { 5, 7, 12, 8, 7 };
        //var allMatchingCombos = new List<IList<int>>();
        //for (int lowerIndex = 1; lowerIndex < myList.Count; lowerIndex++)
        //{
        //    IEnumerable<IList<int>> matchingCombos = new Combinations<int>(myList, lowerIndex, GenerateOption.WithoutRepetition)
        //        .Where(c => c.Sum() == 20);
        //    allMatchingCombos.AddRange(matchingCombos);
        //}

        //foreach (var matchingCombo in allMatchingCombos)
        //Console.WriteLine(string.Join(",", matchingCombo));
    }

    private static bool PrintSequenceForTargetSum(int targetSum)
    {
        var combinations = Calculator.GetFirstCombinationEqualTargetSum(new List<int> { 8, 3, 5, 7 }, targetSum, out bool found);
        if (found)
        {
            combinations[combinations.Count - 1].ForEach(m => Console.Write($"{m} "));
            Console.Write($"sum is {targetSum}");
            Console.WriteLine($"{Environment.NewLine}");
        }
        else
        {
            Console.WriteLine($"Not Found for targetSum of {targetSum}");
        }

        return found;
    }
}

static class Calculator
{
    public static List<List<int>> GetCombinations(List<int> source)
    {
        var combinations = new List<List<int>>();
        if (source.Count == 0)
        {
            return combinations;
        }
        var last = source[source.Count - 1];
        combinations.Add(new List<int> { last });
        if (source.Count == 1)
        {
            return combinations;           
        }
        
        source.RemoveAt(source.Count - 1); //remaining
        var subcombinations = GetCombinations(source);
        subcombinations.ForEach(sub1 =>
        {
            combinations.Add(sub1);
            var subWithLast = sub1.ToList();
            subWithLast.Add(last);
            combinations.Add(subWithLast);
        });
        return combinations;       
    }



    public static List<List<int>> GetFirstCombinationEqualTargetSum(List<int> source, int targetSum, out bool found)
    {
        found = false;
        var combinations = new List<List<int>>();
        if (source.Count == 0)
        {
            return combinations;
        }
        var last = source[source.Count - 1];
        // for sorted only
        //if (targetSum < last)
        //{
        //    return combinations; //empty
        //}
        if(last <= targetSum)
        {
            combinations.Add(new List<int> { last });
        }
        if (last == targetSum)
        {
            found = true;
            return combinations;
        }

        if (source.Count == 1)
        {
            return combinations;
        }

        source.RemoveAt(source.Count - 1); //remaining
        var subcombinations = GetFirstCombinationEqualTargetSum(source, targetSum, out found);
        if (found)
        {
            return new List<List<int>> { subcombinations[subcombinations.Count - 1] };
        }

        foreach(var sub1 in subcombinations)
        {
            var sum = sub1.Sum();
            if(sum <= targetSum)
            {
                combinations.Add(sub1);
            }
            if(sum == targetSum)
            {
                found = true;
                return combinations;
            }
            var subWithLast = sub1.ToList();
            subWithLast.Add(last);
            sum = subWithLast.Sum();
            if(sum  <= targetSum)
            {
                combinations.Add(subWithLast);
            }
            if(sum == targetSum)
            {
                found = true;
                return combinations;
            }
        }
        return combinations;
    }
    #region From http://collabedit.com/kx2rp
    public static List<List<int>> GetCombinations2(List<int> source)
    {
        var combinations = new List<List<int>>();
        if (source.Count == 0)
            return combinations;

        var last = source.Last();
        combinations.Add(new List<int> { last });
        if (source.Count == 1)
            return combinations;

        source.RemoveAt(source.Count - 1);
        var subcombinations = GetCombinations(source);
        subcombinations.ForEach(sub =>
        {
            combinations.Add(sub);
            var subWithLast = sub.ToList();
            subWithLast.Add(last);
            combinations.Add(subWithLast);
        });
        return combinations;
    }

    // if found, the last in the returned list is the correct combination
    public static List<List<int>> GetFirstCombinationEqualTargetSum2(List<int> source, int targetSum, out bool found)
    {
        found = false;
        var combinations = new List<List<int>>();

        if (source.Count == 0)
            return combinations; //empty

        var last = source.Last();
        if (targetSum >= last)
        {// chance with this last
            combinations.Add(new List<int> { last });
        }

        if (targetSum == last)
        {
            found = true;
            return combinations;
        }
        source.RemoveAt(source.Count - 1);
        var subcombinations = GetFirstCombinationEqualTargetSum(source, targetSum, out found);
        if (found)
        {
            return new List<List<int>> { subcombinations[subcombinations.Count - 1] };
        }
        foreach (var sub in subcombinations)
        {
            var sum = sub.Sum();
            if (targetSum <= sum)
            {
                combinations.Add(sub);
            }
            if (sum == targetSum)
            {
                return combinations;
            }
            var subWithLast = sub.ToList();
            subWithLast.Add(last);
            sum = subWithLast.Sum();
            if (sum <= targetSum)
            {
                combinations.Add(subWithLast);
            }
            if (targetSum == sum)
            {
                found = true;
                return combinations;
            }
        }
        return combinations;
    }
    #endregion
}



//******************************************* IMPORTANT INSTRUCTIONS****************
//1) PLEASE NOTE that for all of the questions below, we are more interested in the*** logic and thinking process*** than the actual API names or syntax.
//2) Please type in your answers directly in space provided instead of copying and pasting from an editor.
//3) There is no time limit on the test.
//**********************************************************************************


//Given an array of integers, Write a code containing two threads so that each thread prints an alternate number from the array such that the output is in the same sequence as the array. 
//e.g., given an array { 7,2,1,3}
//Thread 1 will be responsible to print 7 and 1 while Thread 2 will be responsible to print 2,3 but the output will be 7,2,1,3.
