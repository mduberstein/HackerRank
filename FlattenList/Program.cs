
// This is the text editor interface. 
// Anything you type or change here will be seen by the other person in real time.

/*
    //Init
    [1] -> [2] -> [3] -> [8] -> [10]
               |      |
               |     [9]
               |
              [4] -> [5] -> [6]
                             |
                            [7]


    [1] -> [2] -> [3] -> [4] -> [5] -> [6] -> [7] -> [8] -> [9] -> [10]

    //Init1
    [1] 
    //Init2
    [1] -> [2]

    //Init3
    [1] -
        |   
        |     
        |
        [2] -> [3] -> [4]
                       |
                      [5]

    //Init4
    [1] -> [2] -
               |   
               |     
               |
              [3] -> [4] -> [5]
                             |
                            [6]

    //Init5
    [1] -> [2] -> [3]
               |   
               |     
               |
              [4] -> [5] -> [6]
                             |
                            [7]
    
*/

/*if down,
    Flatten
*/
using System;
namespace FlattenList {
    class BranchedList<T>
    {
        //return last actual node in down branch or last actual node
        public Node<T> Flatten(Node<T> cur)
        {
            if (cur == null)
            {
                throw new ArgumentException("Null is invalid argument");
            }

            Node<T> prev;
            //iterate first, flatten after
            //cur is used for iteration and linking next to the end of down branch, prev for relinking start of down branch
            while (cur.Next != null)
            {
                prev = cur;
                cur = cur.Next;
                if (prev.Down != null)
                {
                    var keptCur = cur;
                    // reference to Node<T> passed by value

                    cur = Flatten(prev.Down); //cur points to the flattened end - Down == null and Next == null
                    cur.Next = keptCur.Next; // link main list to flattened end
                    cur.Down = keptCur.Down; // link main list to flattened end
                    keptCur.Next = prev.Down; // link flattened start to main current
                    keptCur.Down = null;
                    prev.Down = null;
                }
            }
            // edge case - last node with Next = null, Down != null, don't move forward
            prev = cur; //keeping cur in prev is a must!!!
            if (prev.Down != null)
            {
                cur = Flatten(prev.Down);
                prev.Next = prev.Down;
                prev.Down = null;
            }
            return cur;
        }
        /// <summary>
        /// Prerequisite: flattened
        /// </summary>
        public void Print()
        {
            Console.WriteLine("Flattened List");
            for (var cur = First; cur != null; cur = cur.Next)
            {
                if (cur.Next == null)
                {
                    Console.Write($"[{cur.Data}]{Environment.NewLine}");
                }
                else
                {
                    Console.Write($"[{cur.Data}]->");
                }

            }
        }

        public Node<T> First { get; set; }
    }

    class Node<T>
    {
        public T Data;
        public Node<T> Next { get; set; }
        public Node<T> Down { get; set; }
    }

    class Solution
    {
        static void Main(string[] args)
        {
            var bl = Init();
            bl.Flatten(bl.First);
            bl.Print();
            bl = Init1();
            bl.Flatten(bl.First);
            bl.Print();
            bl = Init2();
            bl.Flatten(bl.First);
            bl.Print();
            bl = Init3();
            bl.Flatten(bl.First);
            bl.Print();
            bl = Init4();
            bl.Flatten(bl.First);
            bl.Print();
            bl = Init5();
            bl.Flatten(bl.First);
            bl.Print();
            Console.ReadKey();
        }

        static BranchedList<int> Init()
        {
            var list = new BranchedList<int>();
            list.First = new Node<int>
            {
                Data = 1,
                Next = new Node<int>
                {
                    Data = 2,
                    Next = new Node<int>
                    {
                        Data = 3,
                        Next = new Node<int>
                        {
                            Data = 8,
                            Next = new Node<int>
                            {
                                Data = 10
                            }
                        },
                        Down = new Node<int>
                        {
                            Data = 9
                        }
                    },
                    Down = new Node<int>
                    {
                        Data = 4,
                        Next = new Node<int>
                        {
                            Data = 5,
                            Next = new Node<int>
                            {
                                Data = 6,
                                Down = new Node<int>
                                {
                                    Data = 7
                                }
                            }
                        }
                    }
                }
            };
            return list;
        }

        static BranchedList<int> Init1()
        {
            var list = new BranchedList<int>();
            list.First = new Node<int>
            {
                Data = 1
            };
            return list;
        }

        static BranchedList<int> Init2()
        {
            var list = new BranchedList<int>();
            list.First = new Node<int>
            {
                Data = 1,
                Next = new Node<int>
                {
                    Data = 2
                }
            };
            return list;
        }

        static BranchedList<int> Init3()
        {
            var list = new BranchedList<int>();
            list.First = new Node<int>
            {
                Data = 1,
                Down = new Node<int>
                {
                    Data = 2,
                    Next = new Node<int>
                    {
                        Data = 3,
                        Next = new Node<int>
                        {
                            Data = 4,
                            Down = new Node<int>
                            {
                                Data = 5
                            }
                        }
                    }
                }
            };
            return list;
        }

        static BranchedList<int> Init4()
        {
            var list = new BranchedList<int>();
            list.First = new Node<int>
            {
                Data = 1,
                Next = new Node<int>
                {
                    Data = 2,
                    Down = new Node<int>
                    {
                        Data = 3,
                        Next = new Node<int>
                        {
                            Data = 4,
                            Next = new Node<int>
                            {
                                Data = 5
                            },
                            Down = new Node<int>
                            {
                                Data = 6
                            }
                        }
                    }
                }
            };
            return list;
        }

        static BranchedList<int> Init5()
        {
            var list = new BranchedList<int>();
            list.First = new Node<int>
            {
                Data = 1,
                Next = new Node<int>
                {
                    Data = 2,
                    Next = new Node<int>
                    {
                        Data = 3
                    },
                    Down = new Node<int>
                    {
                        Data = 4,
                        Next = new Node<int>
                        {
                            Data = 5,
                            Next = new Node<int>
                            {
                                Data = 6
                            },
                            Down = new Node<int>
                            {
                                Data = 7
                            }
                        }
                    }
                }
            };
            return list;
        }
    }
}
#region http://collabedit.com/w9eg6
//class BranchedList<T>
//{
//    class Node<T>
//    {
//        public T Dt { get; set; }
//        public Node<T> Nt { get; set; }
//        public Node<T> Dn { get; set; }
//    }

//    public Node<T> F { get; set; }
//    //Init
//    [1] -> [2] -> [3] ->[6] 
//               |      |
//               |     [7]
//               |
//              [4] -> [5]

//    public static void Init1()
//    {
//        var bl = new BranchedList<T>
//        {
//            F = new Node<T>
//            {
//                Dt = 1,
//                Nt = new Node<T>
//                {
//                    Dt = 2,
//                    Nt = new Node<T>
//                    {
//                        Dt = 3,
//                        Nt = new Node<T>
//                        {
//                            Dt = 6,
//                        },
//                        Dn = new Node<T>
//                        {
//                            Dt = 7
//                        }
//                    },
//                    Dn = new Node<T>
//                    {
//                        Dt = 4,
//                        Nt = new Node<T>
//                        {
//                            Dt = 5
//                        }
//                    }
//                }
//            }
//        };
//    }

//    public Node<T> Flatten(Node<T> cur)
//    {
//        if (cur == null)
//        {
//            throw new ArgumentException("ccc");
//        }
//        while (cur.Next != null)
//        {
//            var prev = cur;
//            cur = cur.Next;
//            if (prev.Down != null)
//            {
//                var keptCur = cur;
//                cur = Flatten(prev.Down);
//                cur.Next = keptCur.Next;
//                cur.Down = keptCur.Down;
//                prev.Down = null;
//                keptCur.Down = null;
//            }
//        }

//        //also viable
//        if (cur.Down != null)
//        {
//            var keptCur = cur;
//            cur = Flatten(cur.Down);
//            keptCur.Down = null;
//        }
//        return cur;
//    }
//}
#endregion