using System;

public class Huffman
{
    public char znak;
    public int czestotliwosc;
    public Huffman lewe;
    public Huffman prawe;

    
    public bool czyLisc()
    {
        return lewe == null && prawe == null;
    }
}

public class kodHuffmana
{
    public Huffman Root;
    private Dictionary<char, string> kody = new Dictionary<char, string>();

    public void utworzDrzewo(string znak)
    {
        var iloscZnaku = znak.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var kolejka = new PriorityQueue<Huffman, int>();
        foreach (var klucz in iloscZnaku)
        {
            kolejka.Enqueue(new Huffman { znak = klucz.Key, czestotliwosc = klucz.Value }, klucz.Value);
        }
        while (kolejka.Count > 1)
        {
            var lewe = kolejka.Dequeue();
            var prawe = kolejka.Dequeue();

            var rodzic = new Huffman
            {
                znak = '\0', 
                czestotliwosc = lewe.czestotliwosc + prawe.czestotliwosc,
                lewe = lewe,
                prawe = prawe
            };

            kolejka.Enqueue(rodzic, rodzic.czestotliwosc);
        }

        Root = kolejka.Dequeue();

        kodDlaZnaku(Root, "");
    }

    private void kodDlaZnaku(Huffman znak, string kod)
    {
        if (znak == null)
            return;

        if (znak.czyLisc())
        {
            kody[znak.znak] = kod;
        }

        kodDlaZnaku(znak.lewe, kod + "0");
        kodDlaZnaku(znak.prawe, kod + "1");
    }

    public string kompresja(string input)
    {
        var encoded = string.Join("", input.Select(c => kody[c]));
        return encoded;
    }

    public string dekompresja(string tekst)
    {
        var trzymany = Root;
        var odkodowany = "";

        foreach (var bit in tekst)
        {
            trzymany = bit == '0' ? trzymany.lewe : trzymany.prawe;

            if (trzymany.czyLisc())
            {
                odkodowany = odkodowany + trzymany.znak;
                trzymany = Root;
            }
        }

        return decoded;
    }

    public void wypisz()
    {
        foreach (var klucz in kody)
        {
            Console.WriteLine($"{klucz.Key}: {klucz.Value}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Podaj tekst do skompresowania:");
        string znak = Console.ReadLine();
        var drzewo = new kodHuffmana();
        drzewo.utworzDrzewo(znak);
        Console.WriteLine("\nKody Huffmana:");
        drzewo.wypisz();
        string kompresja = drzewo.kompresja(znak);
        Console.WriteLine($"\nSkompresowany tekst: {kompresja}");
        string dekompresja = drzewo.dekompresja(kompresja);
        Console.WriteLine($"\nOdkodowany tekst: {dekompresja}");

    }
}

