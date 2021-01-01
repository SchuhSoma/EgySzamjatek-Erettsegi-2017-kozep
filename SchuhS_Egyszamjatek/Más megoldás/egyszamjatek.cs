using System;
using System.Collections.Generic;
using System.IO;

namespace Egyszamjatek
{
    class Játékos
    {
        public string Név { get; private set; }
        public List<int> Tippek { get; private set; }

        public int FordulókSzáma { get { return Tippek.Count - 1; } } //3. feladathoz

        public Játékos(string sor)
        {
            string[] m = sor.Split();
            Név = m[m.Length-1];
            Tippek = new List<int>();
            Tippek.Add(-1); //Így a forduló sorszáma egyenlő lesz az indexel
            for (int i = 0; i < m.Length-1; i++)
            {
                Tippek.Add(int.Parse(m[i]));
            }
        }
    }

    class egyszamjatek
    {
        static void Main()
        {
            //2. feladat
            List<Játékos> t = new List<Játékos>();
            foreach (var i in File.ReadAllLines("egyszamjatek.txt"))
            {
                t.Add(new Játékos(i));
            }

            //3. feladat
            Console.WriteLine($"3. feladat: Játékosok száma: {t.Count}");

            //4.feladat
            Console.WriteLine($"4. feladat: Fordulók száma: {t[0].FordulókSzáma}");

            //5. feladat: Az első fordulóban volt-e 1-es tipp
            bool voltEgyes = false;
            foreach (var i in t)
            {
                if (i.Tippek[1] == 1)
                {
                    voltEgyes = true;
                    break;
                }
            }
            Console.WriteLine("5. feladat: Az első fordulóban {0}volt egyes tipp!", voltEgyes?"":"nem ");

            //6. feladat: Legnagyobb tipp
            int max = 0;
            foreach (var i in t)
            {
                foreach (var j in i.Tippek)
                {
                    if (j > max) max = j;
                }
            }
            Console.WriteLine($"6. feladat: A legnagyobb tipp a fordulók során: {max}");

            //7. feladat: Forduló sorszámámának (N) bekérése
            Console.Write($"7. feladat: Kérem a forduló sorszámát [1-{t[0].FordulókSzáma}]: ");
            int fordulóSorszáma = int.Parse(Console.ReadLine());
            if (fordulóSorszáma < 1 || fordulóSorszáma > t[0].FordulókSzáma) fordulóSorszáma = 1;



            //8. feladat: Melyik volt a nyertes tipp az N. fordulóban?
            int[] stat = new int[100];
            foreach (var i in t)
            {
                stat[i.Tippek[fordulóSorszáma]]++;
            }

            int nyertesTipp = -1;
            for (int i = 1; i < stat.Length; i++)
            {
                if (stat[i] == 1)
                {
                    nyertesTipp = i;
                    break;
                }
            }

            if (nyertesTipp!=-1) Console.WriteLine($"8. feladat: A nyertes tipp a megadott fordulóban: {nyertesTipp}");
            else Console.WriteLine($"8. feladat: Nem volt egyedi tipp a megadott fordulóban!");


            //9. Nyertes játékos neve az N. fordulóban
            string fordulóNyertese = "";
            if (nyertesTipp!=-1)
            {
                foreach (var i in t)
                {
                    if (i.Tippek[fordulóSorszáma] == nyertesTipp)
                    {
                        fordulóNyertese = i.Név;
                        break;
                    }
                }
                Console.WriteLine($"9. feladat: A megadott forduló nyertese: {fordulóNyertese}");
            }
            else Console.WriteLine($"9. feladat: Nem volt nyertes a megadott fordulóban!");

            //10. feladat: nyertes.txt
            if (nyertesTipp != -1)
            {
                List<string> ki = new List<string>();
                ki.Add($"Forduló sorszáma: {fordulóSorszáma}.");
                ki.Add($"Nyertes tipp: {nyertesTipp}");
                ki.Add($"Nyertes játékos: {fordulóNyertese}");
                File.WriteAllLines("nyertes.txt", ki);
            }

            Console.ReadKey();

        }
    }
}
