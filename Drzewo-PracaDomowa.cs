using System;
using drzewo5;
using System.Xml.Linq;

namespace drzewo5
{
    public class Node
    {
        public int data;
        public Node lewe;
        public Node prawe;

        public Node(int Data)
        {
            data = Data;
            lewe = null;
            prawe = null;
        }
    }
    public class Drzewo
    {
        private Node root;

        public Drzewo()
        {
            root = null;
        }




        public void dodaj(int data)
        {
            root = dodajrek(root, data);
        }

        private Node dodajrek(Node LiscTrzymany, int data)
        {
            if (LiscTrzymany == null)
            {
                return new Node(data);
            }

            if (data < LiscTrzymany.data)
            {
                LiscTrzymany.lewe = dodajrek(LiscTrzymany.lewe, data);
            }
            else if (data >= LiscTrzymany.data)
            {
                LiscTrzymany.prawe = dodajrek(LiscTrzymany.prawe, data);
            }

            return LiscTrzymany;
        }




        public void Wyswietldrzewo()
        {
            RekurencjaDoWyswietlania(root, 0);
        }

        private void RekurencjaDoWyswietlania(Node liscTrzymany, int poziom)
        {
            if (liscTrzymany != null)
            {
                RekurencjaDoWyswietlania(liscTrzymany.prawe, poziom + 1);

                Console.WriteLine(new string(' ', poziom * 4) + liscTrzymany.data);

                RekurencjaDoWyswietlania(liscTrzymany.lewe, poziom + 1);
            }
        }
        public void usun(int data)
        {
            root = usunZrek(root, data);
        }

        private Node usunZrek(Node liscTrzymany, int data)
        {
            if (liscTrzymany == null)
            {
                return null;
            }

            if (data < liscTrzymany.data)
            {
                liscTrzymany.lewe = usunZrek(liscTrzymany.lewe, data);
            }
            else if (data > liscTrzymany.data)
            {
                liscTrzymany.prawe = usunZrek(liscTrzymany.prawe, data);
            }
            else
            {
                
                if (liscTrzymany.lewe == null && liscTrzymany.prawe == null)
                {
                    return null;
                }

                
                if (liscTrzymany.lewe == null)
                {
                    return liscTrzymany.prawe;
                }

                
                if (liscTrzymany.prawe == null)
                {
                    return liscTrzymany.lewe;
                }

                
                Node nastepnik = ZnajdzMin(liscTrzymany.prawe);
                liscTrzymany.data = nastepnik.data;
                liscTrzymany.prawe = usunZrek(liscTrzymany.prawe, nastepnik.data);
            }

            return liscTrzymany;
        }

        private Node ZnajdzMin(Node liscTrzymany)
        {
            while (liscTrzymany.lewe != null)
            {
                liscTrzymany = liscTrzymany.lewe;
            }
            return liscTrzymany;
        }

    }


    class Sys
    {
        static void Main(string[] args)
        {
            Drzewo drzewo = new Drzewo();

            drzewo.dodaj(12);
            drzewo.dodaj(3);
            drzewo.dodaj(54);
            drzewo.dodaj(20);
            drzewo.dodaj(4);
            drzewo.dodaj(4);

            drzewo.Wyswietldrzewo();

            drzewo.usun(12);

            drzewo.Wyswietldrzewo();

        }
    }
}