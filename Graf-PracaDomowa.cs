using System;
class NodeG
{
    public int data;
    public List<NodeG> sasiedzi;

    public NodeG(int data)
    {
        this.data = data;
        sasiedzi = new List<NodeG>();
    }

    public void dodajSasiada(NodeG sasiad)
    {
        sasiedzi.Add(sasiad);
    }
    public void usunSasiada(NodeG sasiad)
    {
        sasiedzi.Remove(sasiad);
    }
}

class Graf
{
    public List<NodeG> Nodes;

    public Graf()
    {
        Nodes = new List<NodeG>();
    }
    public NodeG dodajWierzcholek(int data)
    {
        NodeG newNode = new NodeG(data);
        Nodes.Add(newNode);
        return newNode;
    }

    public void dodajKrawedz(NodeG node1, NodeG node2)
    {
        node1.dodajSasiada(node2);
        node2.dodajSasiada(node1);
    }

    public void wyswietl()
    {
        foreach (var node in Nodes)
        {
            Console.Write(node.data + " -> ");
            foreach (var sasiad in node.sasiedzi)
            {
                Console.Write(sasiad.data + " ");
            }
            Console.WriteLine();
        }
    }

    public void usun(NodeG node)
    {
        foreach (var obecnynode in Nodes)
        {
            obecnynode.usunSasiada(node);
        }
        Nodes.Remove(node);
    }


}

class Program
{
    static void Main(string[] args)
    {
        Graf graf = new Graf();

        NodeG A = graf.dodajWierzcholek(1);
        NodeG B = graf.dodajWierzcholek(7);
        NodeG C = graf.dodajWierzcholek(3);


        graf.dodajKrawedz(A, B);
        graf.dodajKrawedz(A, C);



        graf.wyswietl();

    }
}
