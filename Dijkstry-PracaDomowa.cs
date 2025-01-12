using System;
using System.Collections.Generic;
using System.Linq;

class NodeG
{
    public string Data;

    public NodeG(string data)
    {
        Data = data;
    }
}

class Krawedz
{
    public NodeG Node1;
    public NodeG Node2;
    public int Waga;

    public Krawedz(NodeG node1, NodeG node2, int weight)
    {
        Node1 = node1;
        Node2 = node2;
        Waga = weight;
    }
}

class Graf
{
    public List<NodeG> Nodes = new List<NodeG>();
    public List<Krawedz> Edges = new List<Krawedz>();

    public NodeG dodajWierzcholek(string data)
    {
        var newNode = new NodeG(data);
        Nodes.Add(newNode);
        return newNode;
    }

    public void dodajKrawedz(NodeG node1, NodeG node2, int weight)
    {
        Edges.Add(new Krawedz(node1, node2, weight));
    }

    public void Dijkstra(NodeG startNode)
    {
        var trasa = new Dictionary<NodeG, int>();
        var poprzednie = new Dictionary<NodeG, NodeG>();
        var nieodwiedzone = new List<NodeG>(Nodes);

        
        foreach (var node in Nodes)
        {
            trasa[node] = int.MaxValue;
            poprzednie[node] = null;
        }
        trasa[startNode] = 0;

        while (nieodwiedzone.Count > 0)
        {
            
            var trzymany = nieodwiedzone.OrderBy(node => trasa[node]).First();

            nieodwiedzone.Remove(trzymany);

            foreach (var edge in Edges.Where(edge => edge.Node1 == trzymany || edge.Node2 == trzymany))
            {
                var sasiad = edge.Node1 == trzymany ? edge.Node2 : edge.Node1;
                if (!nieodwiedzone.Contains(sasiad)) continue;

                var newDist = trasa[trzymany] + edge.Waga;
                if (newDist < trasa[sasiad])
                {
                    trasa[sasiad] = newDist;
                    poprzednie[sasiad] = trzymany;
                }
            }
        }
        Console.WriteLine("Dany wierzcholek \t Odległość");
        foreach (var node in Nodes)
        {
            Console.WriteLine($"{node.Data} \t\t\t {trasa[node]}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var graf = new Graf();

        var A = graf.dodajWierzcholek("A");
        var B = graf.dodajWierzcholek("B");
        var C = graf.dodajWierzcholek("C");
        var D = graf.dodajWierzcholek("D");
        var E = graf.dodajWierzcholek("E");

        graf.dodajKrawedz(A, B, 5);
        graf.dodajKrawedz(A, C, 9);
        graf.dodajKrawedz(B, C, 1);
        graf.dodajKrawedz(B, D, 3);
        graf.dodajKrawedz(C, E, 7);
        
        graf.Dijkstra(B);
    }
}

