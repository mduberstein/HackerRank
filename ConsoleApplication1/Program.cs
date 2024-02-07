using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

class Solution
{
    static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        int cost = 6;
        //using (var reader = new StreamReader("Input.txt")) {
        using (var reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding)) { 
            var line = reader.ReadLine();
            if(line != null) {
                var q = int.Parse(line); //number of graphs
                for(int i = 0; i < q; i++) {
                    var graph = BuildGraph(reader, cost);
                    var shortReach = new ShortReach<int>(graph);
                    shortReach.BFSCalculateDistance();
                    shortReach.PrintDistances();
                }
            }
        }
        //Console.ReadKey();
    }

    static Graph<int> BuildGraph(StreamReader reader, int cost)
    {
        string line = reader.ReadLine();
        var items = line.Split(' ');
        var nodes = int.Parse(items[0]);
        var edges = int.Parse(items[1]);
        var graph = new Graph<int>();
        for(int i = 1; i <= nodes; i++) {
            graph.AddNode(i);
        }
        for(int i = 0; i < edges; i++) {
            line = reader.ReadLine();
            items = line.Split(' ');
            var from = int.Parse(items[0]);
            var to = int.Parse(items[1]);
            graph.AddDirectedEdge(from, to, cost);    
        }
        line = reader.ReadLine();
        var start = int.Parse(line);
        graph.Start = graph.FindByValue(start);
        return graph;
    }



    class ShortReach<T>
    {
        public Graph<T> Graph { get; set; }
        public Node<T> First { get; set; }
        /*part Dijksra - only distance table, route table is not required*/
        private Dictionary<T, int> dist;
        public ShortReach(Graph<T> graph)
        {
            Graph = graph;
            initDist(graph);              
        }
        public void initDist(Graph<T> graph)
        {
            dist = new Dictionary<T, int>();
            foreach(var node in graph.Nodes) {
                dist[node.Value] = int.MaxValue;   
            }
            dist[graph.Start.Value] = 0;
        }
        public void BFSCalculateDistance()
        {
            var queue = new Queue<Node<T>>();
            Graph.Start.State = State.Visiting;
            queue.Enqueue(Graph.Start);
            while(queue.Count > 0) {
                var node = queue.Dequeue();
                if(node.State != State.Visited) {
                    node.State = State.Visited;
                    for(int i = 0; i< node.Neighbors.Count; i++) {
                        var nbor = node.Neighbors[i];
                        UpdateDistanceFromNodeToNode(node.Value, nbor.Value, node.Costs[i]);
                        if(nbor.State == State.Unvisited) {
                            nbor.State = State.Visiting;
                            queue.Enqueue(nbor);
                        }
                    }
                }
            }
        }
        public void PrintDistances()
        {
            int count = 0;
            foreach(var key in dist.Keys) {
                count++;
                var val = dist[key];
                if(val >= int.MaxValue) {
                    val = -1;
                }
                if(!key.Equals(Graph.Start.Value)) {
                    Console.Write($"{val}");
                    if (count < dist.Count) {
                        Console.Write(' ');
                    }

                }
            }
            Console.WriteLine();
        }
        //Could update the Route table of Djkstra, if needed
        public void UpdateDistanceFromNodeToNode(T fromValue, T toValue, int cost)
        {
            var distToFrom = dist[fromValue];
            var distToTo = dist[toValue];
            if(distToTo > distToFrom + cost) {
                dist[toValue] = distToFrom + cost;  
            }         
        }

    }

    class Graph<T> : IEnumerable<T>
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
            from.Neighbors.Add(to);
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
    }

    enum State { Unvisited, Visiting, Visited};

    class Node<T>
    {
        public T Value { get; private set; }
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