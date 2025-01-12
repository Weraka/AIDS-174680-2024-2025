using System;

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

    public Krawedz(NodeG node1, NodeG node2, int waga)
    {
        Node1 = node1;
        Node2 = node2;
        Waga = waga;
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

    public void dodajKrawedz(NodeG node1, NodeG node2, int waga)
    {
        Edges.Add(new Krawedz(node1, node2, waga));
    }
}

class DrzewoRozpinajace
{
    public static List<Krawedz> Generuj(Graf graf)
    {
        List<Krawedz> result = new List<Krawedz>(); 
        Dictionary<NodeG, NodeG> rodzic = new Dictionary<NodeG, NodeG>();

        NodeG znajdz(NodeG node)
        {
            if (rodzic[node] != node)
                rodzic[node] = znajdz(rodzic[node]);
            return rodzic[node];
        }

        void Union(NodeG node1, NodeG node2)
        {
            NodeG root1 = znajdz(node1);
            NodeG root2 = znajdz(node2);
            if (root1 != root2)
                rodzic[root1] = root2;
        }
        foreach (var node in graf.Nodes)
        {
            rodzic[node] = node;
        }
        var sortedEdges = graf.Edges.OrderBy(edge => edge.Waga).ToList();
        foreach (var edge in sortedEdges)
        {
            NodeG root1 = znajdz(edge.Node1);
            NodeG root2 = znajdz(edge.Node2);

            
            if (root1 != root2)
            {
                result.Add(edge);
                Union(root1, root2);
            }
        }

        return result;
    }
}


class Program
{
    static void Main(string[] args)
    {
        Graf graf = new Graf();

        var A = graf.dodajWierzcholek("A");
        var B = graf.dodajWierzcholek("B");
        var C = graf.dodajWierzcholek("C");
        var D = graf.dodajWierzcholek("D");
        var E = graf.dodajWierzcholek("E");
        var F = graf.dodajWierzcholek("F");

        graf.dodajKrawedz(A, B, 3);
        graf.dodajKrawedz(A, C, 2);
        graf.dodajKrawedz(B, C, 5);
        graf.dodajKrawedz(C, D, 4);
        graf.dodajKrawedz(B, D, 1);
        graf.dodajKrawedz(E, B, 9);
        graf.dodajKrawedz(E, F, 3);

        List<Krawedz> drzewo = DrzewoRozpinajace.Generuj(graf);

        foreach (var edge in drzewo)
        {
            Console.WriteLine($"{edge.Node1.Data} - ({edge.Waga}) => {edge.Node2.Data}");
        }
    }
}

