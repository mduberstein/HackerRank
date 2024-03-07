using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;

class Solution
{
    static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        int cost = 6;
        //using (var reader = new StreamReader("Input.txt")) {
        using (var reader = new StreamReader("TestCase3Input_corrected.txt"))
        {
            //using (var reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding)) { 
            var line = reader.ReadLine();
            line = DiscardCommentLines(reader, line);
            if (line != null)
            {
                var q = int.Parse(line); //number of graphs
                for (int i = 0; i < q; i++)
                {
                    var graph = BuildGraph(reader, cost);
                    var algorithms = new GraphAlgorithms<int>(graph);

                    algorithms.BFSCalculateDistanceFromStartAndShortestRoute();
                    algorithms.PrintShortestDistancesFromStartNode();
                    algorithms.PrintShortestRoutesFromStartNode();


                    Console.WriteLine($"{Environment.NewLine}Testing Depth First Traversal");
                    algorithms.UnvisitAllNodes();
                    var linkedList = algorithms.DFSTraversal();
                    algorithms.PrintTraversalRoute(linkedList);

                    Console.WriteLine($"{Environment.NewLine}Testing Depth First Search");
                    algorithms.UnvisitAllNodes();
                    int nodeValue = 5;
                    var node = algorithms.DFSFindValue(nodeValue);
                    PrintFoundOrNot(nodeValue, node);
                    nodeValue = 6;
                    algorithms.UnvisitAllNodes();
                    node = algorithms.DFSFindValue(nodeValue);
                    PrintFoundOrNot(nodeValue, node);

                    Console.WriteLine($"{Environment.NewLine}Testing Depth First Search Cycle Detection");
                    algorithms.UnvisitAllNodes();
                    linkedList = algorithms.DFSTraversal(out bool hasCycle);
                    algorithms.PrintTraversalRoute(linkedList);
                    Console.WriteLine($"{(hasCycle ? "Does" : "Doesn't")} have cycles");
                }
            }
        }
        Console.ReadLine();
    }

    private static string DiscardCommentLines(StreamReader reader, string line)
    {
        while (line.StartsWith("#"))
        {
            line = reader.ReadLine();
        }

        return line;
    }

    private static void PrintFoundOrNot(int nodeValue, Node<int> node)
    {
        Console.WriteLine($"Node {nodeValue} {(node != null ? "found" : "not found")}");
    }

    static Graph<int> BuildGraph(StreamReader reader, int cost)
    {
        string line = reader.ReadLine();
        line = DiscardCommentLines(reader, line);
        var items = line.Split(' ');
        var nodes = int.Parse(items[0]);
        var edges = int.Parse(items[1]);
        var graph = new Graph<int>();
        for(int i = 1; i <= nodes; i++) {
            graph.AddNode(i);
        }
        for(int i = 0; i < edges; i++) {
            line = reader.ReadLine();
            line = DiscardCommentLines(reader, line);
            items = line.Split(' ');
            var from = int.Parse(items[0]);
            var to = int.Parse(items[1]);
            graph.AddDirectedEdge(from, to, cost);    
        }
        line = reader.ReadLine();
        line = DiscardCommentLines(reader, line);
        var start = int.Parse(line);
        graph.Start = graph.FindByValue(start);
        return graph;
    }



    class GraphAlgorithms<T>
    {
        public Graph<T> Graph { get; set; }
        /*part Dijksra - only distance table, route table is not required
         distances from start node to each node identified by T value, example its number like in this example or City Name as string*/

        //IMPORTANT
        private Dictionary<T, int> dist;
        private Dictionary<T, Node<T>> route; //Key - Node.Value, Value - preceding Node on the shortest route from Start

        public GraphAlgorithms(Graph<T> graph)
        {
            Graph = graph;
            initDistAndRoute(graph);              
        }
        public void initDistAndRoute(Graph<T> graph)
        {
            dist = new Dictionary<T, int>();
            route = new Dictionary<T, Node<T>>();
            foreach(var node in graph.Nodes) {
                dist[node.Value] = int.MaxValue;
                route[node.Value] = null;
            }
            dist[graph.Start.Value] = 0;
        }

        public void BFSCalculateDistanceFromStartAndShortestRoute()
        {
            var queue = new Queue<Node<T>>();
            Graph.Start.State = State.Visiting;
            queue.Enqueue(Graph.Start);
            while(queue.Count > 0) {
                var node = queue.Dequeue();
                if(node.State != State.Visited) {
                    // IMPORTANT
                    node.State = State.Visited;
                    for(int i = 0; i< node.Neighbors.Count; i++) {
                        var nbor = node.Neighbors[i];
                        UpdateDistanceAndRouteFromStartToNodeTo(node, nbor.Value, node.Costs[i]);
                        if(nbor.State == State.Unvisited) { //prevent cycles
                            nbor.State = State.Visiting;
                            queue.Enqueue(nbor);
                        }
                    }
                }
            }
        }

        
        public LinkedList<T> DFSTraversal()
        {
            var stack = new Stack<Node<T>>();
            var list = new LinkedList<T>();
            Graph.Start.State = State.Visiting;
            stack.Push(Graph.Start);
            while(stack.Count > 0)
            {
                var node = stack.Pop();
                if(node.State != State.Visited)
                {
                    node.State = State.Visited;
                    list.AddLast(node.Value);
                    foreach(var nbor in node.Neighbors)
                    {
                        if(nbor.State == State.Unvisited) //prevent cycles
                        {
                            nbor.State = State.Visiting;
                            stack.Push(nbor);
                        }
                    }    
                }
            }
            return list;
        }

        //returns travesalList until a cycle is encountered, last node in the returned list shows first node encountered twice ( last -> is the back edge)
        public LinkedList<T> DFSTraversal(out bool hasCycle)
        {
            hasCycle = false;
            var traversalList = new LinkedList<T>();
            var st = new Stack<Node<T>>();
            Graph.Start.State = State.Visiting;
            st.Push(Graph.Start);
            while(st.Count > 0)
            {
                var n = st.Pop();
                if(n.State != State.Visited)
                {
                    n.State = State.Visited;
                    traversalList.AddLast(n.Value);
                    foreach(var nbor in n.Neighbors)
                    {
                        if(nbor.State == State.Visited)
                        {
                            hasCycle = true;
                            traversalList.AddLast(nbor.Value);
                            return traversalList;
                        }
                        else if (nbor.State == State.Unvisited)
                        {
                            nbor.State = State.Visiting;
                            st.Push(nbor);
                        }
                    }
                }
            }
            return traversalList;
        }

        public void PrintTraversalRoute(LinkedList<T> list)
        {
            Console.WriteLine(string.Join("->", list.Select(item => item.ToString())));
        }

        //Stop once found, return null if not found
        public Node<T> DFSFindValue(T val)
        {
            var stack = new Stack<Node<T>>();
            Graph.Start.State = State.Visiting;
            stack.Push(Graph.Start);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node.State != State.Visited)
                {
                    node.State = State.Visited;
                    if (node.Value.Equals(val))
                    {
                        return node;
                    }
                    foreach (var nbor in node.Neighbors)
                    {
                        if (nbor.State == State.Unvisited)
                        {
                            nbor.State = State.Visiting;
                            stack.Push(nbor);
                        }
                    }
                }
            }
            return null;
        }

        //Depth first search: For any node, the search occures in order current->left most child->second left most child
        //recursive
        public void DFSCalculateDistanceRecursive()
        {
            
        }

        private void DFSCalculateDistanceRecursiveStep(Node<T> node, int neigbourIndex)
        {

        }

        public void PrintShortestDistancesFromStartNode()
        {
            int count = 0;
            foreach(var key in dist.Keys) {
                count++;
                var val = dist[key];
                if(val >= int.MaxValue) {
                    val = -1; //not reachable from start node
                }
                if(!key.Equals(Graph.Start.Value)) {
                    Console.Write($"Node {key} dist {(val != -1? val.ToString() : "inf")}");
                    if (count < dist.Count) {
                        Console.Write("; ");
                    }

                }
            }
            Console.WriteLine();
        }

        public void PrintShortestRoutesFromStartNode()
        {
            foreach(var key in route.Keys)
            {
                var st = new Stack<T>();
                st.Push(key);
                for(var previousNode = route[key]; previousNode != null; previousNode = route[previousNode.Value])
                {
                    st.Push(previousNode.Value);                  
                }
                if(st.Count == 1)
                {
                    if (Graph.Start.Value.Equals(st.Peek()))
                    {
                        Console.Write($"Start node ");
                    }
                    else
                    {
                        Console.Write("Not reachable node ");
                    }
                    
                }
                while(st.Count > 0)
                {
                    var nodeValue = st.Pop();
                    Console.Write($"{nodeValue}");
                    if(st.Count > 0)
                    {
                        Console.Write("->");
                    }
                }
                Console.WriteLine();
            }
        }


        //Could update the Route table of Djkstra, if needed
        public void UpdateDistanceAndRouteFromStartToNodeTo(Node<T> from, T toValue, int cost)
        {
            var distToFrom = dist[from.Value];
            var distToTo = dist[toValue];
            if(distToTo > distToFrom + cost) {
                dist[toValue] = distToFrom + cost;
                route[toValue] = from;
            }         
        }

        public void UnvisitAllNodes()
        {
            Graph.UnvisitAllNodes();
        }
    }

    class Graph<T> : IEnumerable<T>, IEnumerable<Node<T>>
    { 
        public List<Node<T>> Nodes
        {
            get
            {
                if (nodes == null)
                    nodes = new List<Node<T>>();
                return nodes;
            }
        }

        private List<Node<T>> nodes;

        public Node<T> Start { get; set; }
        public void AddNode(T val)
        {
            if (!Contains(val)) {
                Nodes.Add(new Node<T>(val));
            }
        }
        public bool Contains(T val)
        {
            return FindByValue(val) != null;
        }

        public void AddDirectedEdge(Node<T> from, Node<T> to, int cost)
        {
            //repeated neighbor insertion now taken care of
            if (from.Neighbors.Contains(to))
            {
                return;
            }
            from.Neighbors.Add(to);
            //LATER
            //hack to take care of repeated combination of same edge in the input, 
            //10 1
            //10 1
            //by inserting 1 10 instead of second combination
            from.Costs.Add(cost);
        }
        public void AddDirectedEdge(T from, T to, int cost)
        {
            var fromN = FindByValue(from);
            var toN = FindByValue(to);
            AddDirectedEdge(fromN, toN, cost);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in Nodes) {
                yield return node.Value;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var node in Nodes) {
                yield return node.Value;
            }

        }
        public Node<T> FindByValue(T val)
        {
            foreach (var node in Nodes) {
                if (node.Value.Equals(val)) {
                    return node;
                }
            }
            return null;
        }

        public void UnvisitAllNodes()
        {
            foreach(var node in Nodes)
            {
                node.State = State.Unvisited;
            }
        }

        IEnumerator<Node<T>> IEnumerable<Node<T>>.GetEnumerator()
        {
            return ((IEnumerable<Node<T>>)Nodes).GetEnumerator();
        }
    }



    enum State { Unvisited, Visiting, Visited};
    class Node<T>
    {
        public T Value { get; private set; }
        // State of the node could be better saved in IDictionary<T, State> in the GraphAlgorithms class
        public State State { get; set; }
        public Node(T val, List<Node<T>> nbors = null)
        {
            this.Value = val;
            this.neighbors = nbors;
            this.State = State.Unvisited; 
        }
        public List<Node<T>> Neighbors
        {
            get
            {
                if(neighbors == null) {
                    neighbors = new List<Node<T>>();
                }
                return neighbors;
            }               
        }
        public List<int> Costs
        {
            get
            {
                if(costs == null) {
                    costs = new List<int>();
                }
                return costs;
            }
        }
        private List<Node<T>> neighbors;
        private List<int> costs;
    }
}