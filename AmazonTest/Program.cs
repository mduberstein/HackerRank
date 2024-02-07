//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//// IMPORT LIBRARY PACKAGES NEEDED BY YOUR PROGRAM
//// SOME CLASSES WITHIN A PACKAGE MAY BE RESTRICTED
//// DEFINE ANY CLASS AND METHOD NEEDED
//// CLASS BEGINS, THIS CLASS IS REQUIRED
//public class Solution
//{
//    static void Main(string[] args)
//    {
//        getAnagramIndices("abbc", "ab");
//    }
//    public static List<string> FindPermutations(string s)
//    {
//        var l = new List<string>();
//        if (s.Length == 1) {
//            l.Add(s);
//            return l;
//        }

//        if (s.Length == 2) {
//            var a = s.ToCharArray();
//            var ns = new string(new char[] { a[1], a[0] });
//            l.Add(s);
//            l.Add(ns);
//        }

//        //var t = new List<char> (s.ToCharArray());
//        //for(int i = 0; i < t.Count; i++) {
//        //    t.RemoveAt(i);
//        //    string tes = new string(t.ToArray());
//        //    foreach(var p in FindPermutations(tes)) {
//        //        l.Add(s[i] + p);
//        //    }
//        //}
//        var sub = FindPermutations(s.Substring(1));
//        foreach(var ss in sub) {
//            l.Add(s[0].ToString() + sub);
//        }
//        return l;
//    }

//    // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
//    // RETURN AN EMPTY LIST IF NO ANAGRAM FOUND
//    public static List<int> getAnagramIndices(string haystack, string needle)
//    {
//        // WRITE YOUR CODE HERE
//        var l = new List<int>();
//        if (string.IsNullOrEmpty(needle)) {
//            return l;
//        }
//        var p = FindPermutations(needle);
//        for (int i = 0; i < haystack.Length; i++) {
//            foreach (var an in p) {
//                if (haystack.Substring(i).Contains(an)) {
//                    l.Add(i); 
//                }
//             }
//         }
//        return l;
//    }

//    // METHOD SIGNATURE ENDS
//}

using System;
using System.Collections.Generic;

public class Solution
{

    /*
    teamCuisinePreference matrix represents pairs <TeamMemberName, CuisinePreference>, ex. <Tom, *>, <Grace, Italian>
        * for CusinePreference represents any cuisine
    lunchMenuPairs matrix represents pairs <LunchMenuOption, CusinePreference>, ex. <Taco, Mexican>, <Pasta, Italian>
    NOTE: I renamed lunchMenuPairs to menuCuisinePairs to reflect the meaning
    code method with this signature
        public static string[,] matchLunches(string[,] lunchMenuPairs,
                                            string[,] teamCuisinePreference)
    returning all possible pairs <TeamMemberName, LunchMenuOption> in a matrix 
    // RETURN AN EMPTY MATRIX IF PREFERRED LUNCH IS NOT FOUND
    */

    static void Main(string[] args)
    {
        //string[,] teamCuisinePreference = new string[,] { { "Tom", "French" }, { "Grace", "Jewish" }, {"Jane", "Mongolian"} };
        string[,] teamCuisinePreference = new string[,] { { "Tom", "*" }, { "Grace", "Italian" }, {"Jane", "German"} };
        //string[,] teamCuisinePreference = new string[0, 0];
        string[,] menuCuisinePairs = new string[,] { { "Pasta", "Italian" }, { "Pizza", "Italian" }, { "Weiner", "German" } };
        //string[,] lunchMenuPairs = new string[0, 0];
        
        var nameLunchOptionPairs = matchLunches(menuCuisinePairs, teamCuisinePreference);
        if(nameLunchOptionPairs.GetLength(0) == 0) {
            Console.WriteLine("Empty Matrix");
        }
        for(int i = 0; i < nameLunchOptionPairs.GetLength(0); i++) {
             Console.WriteLine($"{{{nameLunchOptionPairs[i, 0]}, {nameLunchOptionPairs[i, 1]}}}");
        }
        Console.ReadKey();
    }

    // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
    // RETURN AN EMPTY MATRIX IF PREFERRED LUNCH IS NOT FOUND
    public static string[,] matchLunches(string[,] menuCuisinePairs,
                                            string[,] teamCuisinePreference)
    {
        Dictionary<string, HashSet<string>> cuisineMenuLookup;
        int totalMenus;
        CreateCuisineMenuLookup(menuCuisinePairs, out cuisineMenuLookup, out totalMenus);
        int size0 = CalculateResultSize(teamCuisinePreference, cuisineMenuLookup, totalMenus);
        string[,] result = new string[size0, 2];
        for (int teamI = 0, resI = 0; teamI < teamCuisinePreference.GetLength(0); teamI++) {
            HashSet<string> menus;
            var cuisineKey = teamCuisinePreference[teamI, 1];
            if (cuisineKey.Equals("*")) {
                for (int mI = 0; mI < menuCuisinePairs.GetLength(0); mI++) {
                    result[resI, 0] = teamCuisinePreference[teamI, 0];
                    result[resI, 1] = menuCuisinePairs[mI, 0];
                    resI++;
                }
            }
            else if (cuisineMenuLookup.TryGetValue(cuisineKey, out menus)) {
                foreach (var m in menus) {
                    result[resI, 0] = teamCuisinePreference[teamI, 0];
                    result[resI, 1] = m;
                    resI++;
                }
             }
        }
        return result;
    }

    private static void CreateCuisineMenuLookup(string[,] lunchMenuPairs, out Dictionary<string, HashSet<string>> cuisineMenuLookup, out int totalMenus)
    {
        cuisineMenuLookup = new Dictionary<string, HashSet<string>>();
        totalMenus = lunchMenuPairs.GetLength(0);
        for (int i = 0; i < totalMenus; i++) {
            HashSet<string> menus;
            var cuisineKey = lunchMenuPairs[i, 1];
            if (!cuisineMenuLookup.TryGetValue(cuisineKey, out menus)) {
                menus = new HashSet<string>();
                cuisineMenuLookup.Add(cuisineKey, menus);
            }
            menus.Add(lunchMenuPairs[i, 0]);
        }
    }

    private static int CalculateResultSize(string[,] teamCuisinePreference, Dictionary<string, HashSet<string>> cuisineMenuLookup, int totalMenus)
    {
        int size0 = 0; //number of output pairs
        for (int i = 0; i < teamCuisinePreference.GetLength(0); i++) {
            var cuizine = teamCuisinePreference[i, 1];
            HashSet<string> menus;
            if (cuizine.Equals("*")) {
                size0 += totalMenus;               
            }
            else if(cuisineMenuLookup.TryGetValue(cuizine, out menus)) {
                size0 += menus.Count;
            }
        }

        return size0;
    }
}


// printed in the http:
// seach for wrong
//    #using System.Collections.Generics;
 
//void CreateCuisineMenuLookup(string[,] menuCuis, out Dictionary<string, HashSet<string>> lUp, out int numMenus)
//{
//    lUp = new Dictionary<string, HashSet<string>>();
//    //var allMenues = Hashset<string>(); - wrong
//    numMenues = menuCui.GetLength(0);
//    for (int i = 0; i < menuCuis.GetLength(0); i++)
//    {
//        /*
//                var menu = menuCuis[i,0];
//                if(!allMenues.Contains(menu)){
//                    numMenues++;
//                }
//        */
//        HashSet<string> menues;
//        var cuis = menuCuis[i, 1];
//        if (!lUp.TryGetValue(cuis, out menues))
//        {
//            menues = new HashSet<string>();
//            lUp[cuis] = menues;
//        }
//        menues.Add(menuCuis[i, 0]);
//    }
//}

//int CalculateResultSize(string[,] perCuis, Dictionary<string, <Hashset<string>> lookCuisMenu, int totalMenues)
//{
//    int resSize = 0;
//    for (int i = 0; i < perCuis.GetLength(0); i++)
//    {
//        var cuis = perCuis[i, 1];
//        HashSet<string> menues;
//        if (perCuis[i, 1] == "*")
//        {
//            resize += totalMenues;
//        }
//        else if (!lookCuisMenu.TryGetValue(cuis, out menues){
//            //var menues = lookCuisMenu[cuis]; wrong may not be         
//            resSize += menues.Count;
//        }
//    }
//    return resSize;
//}

//string[,] personMenu = matchLunches(string[,] menuCuis, string[,] personCuis){
//  Dictionary<string, HashSet<string>> lookCuisMenu;
//int numMenus;
//  CreateCuisineMenuLookup(menuCuis, out lookCuisMenu, out numMenus);
//var size0 = CalculateResultSize(personCuis, lookCuisMenu, numMenues);

//var numPersons = personCuis.GetLength(0);
//var personMenu = new string[size0, 2];
//  for(int i=0; i<numPersons; i++){
//      if(personCuis[i, 1]=="*"){
//          for(int j=0; j<menuCuis.GetLenght(0); j++;i++){
//              personMenu[i + j] = personCuis[i, 0];
//              personMenu[i + j, 1]=menuCuis[j, 0];
//          }
//      }else{
//          var menues = lookupCuisMenu[persCuis[i, 1]];
//          foreach(var menu in menues){
//              personMenu[i + j] = personCuis[i, 0];
//              personMenu[i++, 1]=menu;
//          }
//      }
//  }
//  return personMenu;
//}