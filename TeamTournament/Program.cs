using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTournament
{


    class Program
    {
        struct ProbabilityKey
        {
            public string Left;
            public string Right;
            //overrides are not necessary for structs containing strings or primitive type fields
            //https://msdn.microsoft.com/en-us/library/system.object.gethashcode(v=vs.110).aspx
            //https://msdn.microsoft.com/en-us/library/bsc2ak47(v=vs.110).aspx
            //public static bool operator == (ProbabilityKey l, ProbabilityKey r)
            //{
            //    return l.Left == r.Left && l.Right == r.Right;
            //}
            //public static bool operator != (ProbabilityKey l, ProbabilityKey r)
            //{
            //    return !(l == r);
            //}
            //public override bool Equals(object obj)
            //{
            //    return obj != null && obj is ProbabilityKey && this == (ProbabilityKey)obj;
            //}
            //public override int GetHashCode()
            //{
            //    return Left.GetHashCode() ^ Right.GetHashCode();
            //}
        }

        class Node
        {
            public Node Left;
            public Node Right;
            //probability of Key, i.e. the team name, of reaching this Node in the Tournement structure 
            public Dictionary<string, float> Outcomes; //probabilies of wins over all teams it can play
        }

        //Probability of Victory of Left over Right
        static Dictionary<ProbabilityKey, float> probabilityLookup = new Dictionary<ProbabilityKey, float>
        {
                { new ProbabilityKey { Left = "A", Right = "B" }, 0.3F },
                { new ProbabilityKey { Left = "A", Right = "C" }, 0.3F },
                { new ProbabilityKey { Left = "A", Right = "D" }, 0.3F },
                { new ProbabilityKey { Left = "B", Right = "C" }, 0.3F },
                { new ProbabilityKey { Left = "B", Right = "D" }, 0.3F },
                { new ProbabilityKey { Left = "C", Right = "D" }, 0.3F }
        };
        /*
         * A------------|
         *              |---------------|
         * B------------|               |
         *                              |------root
         * C------------|               |
         *              |---------------|
         * D------------|
         */
        static Node InitStaticTree()
        {
            return new Node
            {
                Left = new Node
                {
                    Left = new Node { Outcomes = new Dictionary<string, float> { { "A", 1 } } },
                    Right = new Node { Outcomes = new Dictionary<string, float> { { "B", 1 } } }
                },
                Right = new Node
                {
                    Left = new Node
                    {
                        Outcomes = new Dictionary<string, float> { { "C", 1 } }
                    },
                    Right = new Node
                    {
                        Outcomes = new Dictionary<string, float> { { "D", 1 } }
                    }
                }
            };
        }

        // This method calculates returns the Dictionary where the Keys are TeamNames and
        // the Value are the probabilites of the TeamName or reaching this Node in the tournament
        static Dictionary<string, float> CalculateProbabilitiesRecursive(Node n)
        {
            if(null != n.Outcomes)
            {
                return n.Outcomes;
            }
            n.Outcomes = new Dictionary<string, float>();
            foreach(var keyValueLeft in CalculateProbabilitiesRecursive(n.Left))
            {
                n.Outcomes.Add(keyValueLeft.Key, 0.0F);
                foreach (var keyValueRight in CalculateProbabilitiesRecursive(n.Right)){
                    n.Outcomes[keyValueLeft.Key] += keyValueLeft.Value * keyValueRight.Value * Victory(keyValueLeft.Key, keyValueRight.Key);
                    //float prob; C#7.0
                    if(!n.Outcomes.TryGetValue(keyValueRight.Key, out float prob))
                    {
                        n.Outcomes.Add(keyValueRight.Key, 0.0F);
                    }
                    n.Outcomes[keyValueRight.Key] += keyValueLeft.Value * keyValueRight.Value * Victory(keyValueRight.Key, keyValueLeft.Key);
                }
            }
            return n.Outcomes;
        }

        // Partial explanation of the recursive steps
        //static Dictionary<string, float> CalculateProbabilitiesRecursive(Node n)
        //{
        //    if (null != n.Outcomes)
        //    {
        //        return n.Outcomes;
        //          // Step 2.1: root.Left.Left.Outcomes = {{"A", 1 }}
        //          // Step 4.1: root.Left.Right.Outcomes = {{ "B", 1}}
        //    }
        //    n.Outcomes = new Dictionary<string, float>(); 
        //      // Step 1: root.Outcomes = {empty}
        //      // Step 2.1: root.Left.Outcomes = {empty}, root.Left.Left.Outcomes = {{"A", 1 }}
        //    foreach (var keyValueLeft in CalculateProbabilitiesRecursive(n.Left))
        //      // Call in Step 2 with root.Left, Return in Step 2.1
        //      // Call in Step 3 with root.Left.Left, Return in Step 3.1
        //    {
        //        n.Outcomes.Add(keyValueLeft.Key, 0.0F);
        //          // Step 3.2: root.Left.Outcomes: {{"A", 0.0F}}
        //          
        //        foreach (var keyValueRight in CalculateProbabilitiesRecursive(n.Right))
        //          // Call in Step 4 with root.Left.Right, Return in Step 4.1 root.Left.Right.Outcomes = {{"B", 1}}
        //   
        //        {
        //            n.Outcomes[keyValueLeft.Key] += keyValueLeft.Value * keyValueRight.Value * Victory(keyValueLeft.Key, keyValueRight.Key);
        //              // Step 4.2: keyValueLeft.Key = "A", keyValueRight.Key = "B", root.Left.Right = {{"A", 0.3}}
        //            if (!n.Outcomes.TryGetValue(keyValueRight.Key, out float prob))
        //            {
        //                n.Outcomes.Add(keyValueRight.Key, 0.0F);
        //            }
        //            n.Outcomes[keyValueRight.Key] += keyValueLeft.Value * keyValueRight.Value * Victory(keyValueRight.Key, keyValueLeft.Key);
        //              // Step 4.3: keyValueLeft.Key = "A", keyValueRight.Key = "B", root.Left.Right = {{"B", 0.7}}
        //        }
        //    }
        //    return n.Outcomes;
        //}

        //probability of Victory of Left over Right
        static float Victory(string l, string r)
        {
            // float prob; // C# 7.0
            if (probabilityLookup.TryGetValue(new ProbabilityKey { Left = l, Right = r }, out var prob))
            {
                return prob;
            }
            else if (probabilityLookup.TryGetValue(new ProbabilityKey { Left = r, Right = l }, out prob))
            {
                return 1 - prob;
            }
            else
            {
                throw new ArgumentException("Not in Lookup");
            }
        }

        static void Main(string[] args)
        {
            Node root = InitStaticTree();
            // Win Probability of a team is the probability of reaching the Root Node
            foreach(var pair in CalculateProbabilitiesRecursive(root).OrderBy(pair => pair.Key))
            {
                Console.WriteLine($"Team {pair.Key}, Win Probability {pair.Value}");
            }
            Console.ReadKey();
        }

        //http://collabedit.com/kx2rp
        //static float Victory(string l, string r)
        //{
        //    if (Lookup.TryGetValue(new ProbabilityKey { Left = l, Right = r }, out var float){
        //        return prob;
        //    }
        //    else if (Lookup.TryGetValue(new ProbabilityKey { Left = r, Right = l }, out var float){
        //        return 1 - prob;
        //    }
        //    else
        //    {
        //        throw new ArgumentExeption("Not in the lookup");
        //    }
        //}

        //static Dictionary<string, float> CalculateProbabilitiesRecursive(Node n)
        //{
        //    if (n.Outcomes != null)
        //        return n.Outcomes;

        //    n.Outcomes = new Dictionary<string, float>();
        //    foreach (var leftKVPair in CalculateProbabilitiesRecursive(n.Left))
        //    {
        //        n.Outcomes.Add(leftKVPair.Key, 0.0F);
        //        foreach (var rightKVPair in CalculateProbabilitiesRecursive(n.Right))
        //        {
        //            n.Outcomes[leftKVPair.Key] += leftKVPair.Value * rightKVPair.Value * Victory(leftKVPair.Key, rightKVPair.Key);
        //            if (!n.Outcomes.TryGetValue(rightKVPair.Key, out float prob){
        //                n.Outcomes.Add(rightKVPair.Key, 0.0F);
        //            }
        //            n.Outcomes[rightKVPair.Key] += rightKVPair.Value * leftKVPair.Value * Victory(rightKVPair.Key, leftKVPair.Key);
        //        }
        //    }
        //    return n.Outcomes;
        //}
    }
}


#region TypeTest
// This is test in Typing timing

//using System;
//using System.IO;
//using System.Collections.Generic;

//class Program
//{
//    struct ProbKey
//    {
//        public string L;
//        public string R;
//    }

//    class Node
//    {
//        public Node L;
//        public Node R;
//        Dictionary<string, float> Outs;
//    }

//    static Dictionary<ProbKey, float> pLook = new Dictionary<ProbKey, float>
//    {
//        {new ProbKey {L = "A", R = "B"}, 0.3F},
//        {new ProbKey {L = "A", R = "C"}, 0.3F},
//        {new ProbKey {L = "A", R = "D"}, 0.3F},
//        {new ProbKey {L = "B", R = "C"}, 0.3F},
//        {new ProbKey {L = "B", R = "D"}, 0.3F},
//        {new ProbKey {L = "C", R = "C"}, 0.3F}
//    };

//    static float Vic(string l, string r)
//    {
//        float p;
//        if (pLook.TryGetValue(new ProbKey { L = l, R = r }, out p){
//            return p;
//        }
//        if (pLook.TryGetValue(new ProbKey { L = r, R = l, out p){
//            return 1 - p;
//        }
//        else
//            throw new ArgumentException("Invalid");
//    }

//    static Node InitTree
//    {
//        return new Node {
//           L = new Node {
//               L = new Node {
//                   L = new Node { Outs = new Dictionary {"A", 1}}},
//                   R = new Node { Outs = new Dictionary {"B", 1}}}
//               }
//           },
//           R = new Node {
//               L = new Node {
//                   L = new Node { Outs = new Dictionary {"C", 1}}},
//                   R = new Node { Outs = new Dictionary {"D", 1}}}
//               }
//           }
//        };
//    }


//    static Dictionary<string, float> CalcProbs(Node n)
//{
//    if (null != Outs)
//    {
//        return n.Outs;
//    }
//    n.Outs = new Dictionary<string, float>();
//    foreach (var kVL in CalProbs(n.Left))
//    {
//        n.Outs.Add(kVL, 0.0F);
//        foreach (var kVR in CalProbs(n.Right))
//        {
//            n.Out[kVL.Key] += kVL.Value * kVR.Value * Vic(kVL.Key, kVR.Key);
//            float p;
//            if (!n.Outs.TryGetValue(kVR.Key, out p){
//                n.Outs.Add(kVR.Key, 0.0F);
//            }
//            n.Outs[kVR.Key] += kVR.Value * kVL.Value * Vic(kVR.Key, kVL.Key);
//        }
//    }
//}


//}
#endregion