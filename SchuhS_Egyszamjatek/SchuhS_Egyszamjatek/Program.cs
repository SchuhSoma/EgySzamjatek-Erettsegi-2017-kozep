using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SchuhS_Egyszamjatek
{
    class Program
    {
        static List<Szamjatek> SzamjatekList;
        static List<int> ForduloSzamai;
        static Dictionary<string, int> ForduloJatekosaiTippjei;
        static Dictionary<int, int> ForduloSzamaiElofordulassal;
        static List<int> TisztazottTippel;
        static int BekertSzam;
        static int MinTipp;
        static void Main(string[] args)
        {
            Feladat2Beolvas(); Console.WriteLine("\n---------------------------------------------\n");
            Feladat3JatekosokSzama(); Console.WriteLine("\n---------------------------------------------\n");
            Feladat4ForduloSzam(); Console.WriteLine("\n---------------------------------------------\n");
            Feladat5ELdontTipp(); Console.WriteLine("\n---------------------------------------------\n");
            Feladat6LEgnagyobTipp(); Console.WriteLine("\n---------------------------------------------\n");
            Feladat7Bekeres(); Console.WriteLine("\n---------------------------------------------\n");
            Console.ReadKey();
        }

        private static void Feladat7Bekeres()
        {
            Console.WriteLine("7.Feladat: Bekérés és keresés");
            Console.Write("\tKérem a forduló sorszámát[1−10]: ");
            BekertSzam = int.Parse(Console.ReadLine());
            if(0<BekertSzam && BekertSzam<11)
            {
                Console.WriteLine("\tJó értéket adott meg");
                Console.WriteLine("\n---------------------------------------------\n");
                Feladat8();
                
            }
            else
            {
                Console.WriteLine("Sajnos rossz értéket adott meg az 1. fordulóval kell dolgozni");
                BekertSzam = 1;
                Console.WriteLine("\n---------------------------------------------\n");
                Feladat8();
            }
        }

        private static void Feladat8()
        {
            Console.WriteLine("8.Feladat: Mi a forduló nyerőszáma");
            ForduloSzamai = new List<int>();
            ForduloJatekosaiTippjei = new Dictionary<string, int>();
            foreach (var f in SzamjatekList)
            {
                ForduloSzamai.Add(f.SzamTippek[BekertSzam - 1]);
                ForduloJatekosaiTippjei.Add(f.Nev, f.SzamTippek[BekertSzam - 1]);
            }
            foreach (var a in ForduloSzamai)
            {
                //Console.WriteLine("{0}",a);
            }
            foreach (var b in ForduloJatekosaiTippjei)
            {
                //Console.WriteLine("\t{0,-8} : {1}",b.Key,b.Value);
            }
            TisztazottTippel = new List<int>();
            foreach (var f in ForduloSzamai)
            {
                if (!TisztazottTippel.Contains(f))
                { TisztazottTippel.Add(f); }
            }
            ForduloSzamaiElofordulassal = new Dictionary<int, int>();
            TisztazottTippel.Sort();
            TisztazottTippel.Reverse();
            foreach (var t in TisztazottTippel)
            {
                int db = 0;
                foreach (var fo in ForduloSzamai)
                {
                    if (t == fo)
                    {
                        db++;
                    }
                }
                ForduloSzamaiElofordulassal.Add(t, db);
            }
            foreach (var k in ForduloSzamaiElofordulassal)
            {
                //Console.WriteLine("\t{0} : {1}",k.Key,k.Value);
            }
            MinTipp = int.MaxValue;
            foreach (var l in ForduloSzamaiElofordulassal)
            {
                if (l.Value == 1 && l.Key < MinTipp)
                { MinTipp = l.Key; }
            }
            if (0 < MinTipp && MinTipp < 101)
            {
                Console.WriteLine("\tA forduló nyerőszáma : {0}", MinTipp);
                Console.WriteLine("\n---------------------------------------------\n");
                Feladat9();

            }
            else
            {
                Console.WriteLine("\tNem volt egyedi tipp a megadott fordulóban!");
            }


        }

        private static void Feladat9()
        {
            Console.WriteLine("9.Feladat: Az adott forduló nyertesének neve");
            string NyertesNeve = "Cica";
            foreach (var b in ForduloJatekosaiTippjei)
            {
                if(MinTipp==b.Value)
                { NyertesNeve = b.Key; }
            }
            var sw = new StreamWriter(@"nyertes.txt", false, Encoding.UTF8);
            sw.WriteLine("\tA forduló sorszáma: {0}", BekertSzam);
            sw.WriteLine("\tA forduló nyerőszáma : {0}", MinTipp);
            sw.WriteLine("\tAz adott forduló nyertesének neve: {0}", NyertesNeve);
            sw.Close();
            Console.WriteLine("\tAz adott forduló nyertesének neve: {0}", NyertesNeve);
        }

        private static void Feladat6LEgnagyobTipp()
        {
            Console.WriteLine("6.Feladat: Határozza meg a legnagyobb tipp értékét");
            int MaxTipp = int.MinValue;
            foreach (var s in SzamjatekList)
            {
                for (int i = 0; i < SzamjatekList[0].JatekokSzama; i++)
                {
                    if(s.SzamTippek[i]>MaxTipp)
                     {
                        MaxTipp = s.SzamTippek[i];
                     }
                }                
            }
            Console.WriteLine("\tA legnagyobb tipp: {0}", MaxTipp);
        }

        private static void Feladat5ELdontTipp()
        {
            Console.WriteLine("5.Feladat: az első fordulóban tippelt-e valaki az 1 - es számra");
            bool Dontes = false;
            int db=0;
            foreach (var s in SzamjatekList)
            {
                if (s.SzamTippek[0]==1)
                {
                    Dontes = true;
                }
            }
            if (Dontes == true) { Console.WriteLine("\tIgen, az első fordulóban volt egyes tipp"); }
            else { Console.WriteLine("\tNem, az eslő fordulóban nem volt egyes tipp"); }
            foreach (var sz in SzamjatekList)
            {
                if(sz.SzamTippek[0]==1)
                {
                    db++;
                }
            }
            if(db>0) { Console.WriteLine("\tIgen, az első fordulóban volt egyes tipp, enyiedik tipp volt egyes: {0}", db); }
            else { Console.WriteLine("\tNem, az eslő fordulóban nem volt egyes tipp"); }
        }

        private static void Feladat4ForduloSzam()
        {
            Console.WriteLine("4.Feladat: Határozza meg a fordulók számát");
            Console.WriteLine("\tA fordulók száma: {0}",SzamjatekList[0].JatekokSzama);
        }

        private static void Feladat3JatekosokSzama()
        {
            Console.WriteLine("3.Feladat: a játékban hány játékos vettrészt");
            Console.WriteLine("\tJátékosok száma: {0}", SzamjatekList.Count);
        }

        private static void Feladat2Beolvas()
        {
            Console.WriteLine("2.Feladat: Beolvasás");
            SzamjatekList = new List<Szamjatek>();
            var sr = new StreamReader(@"egyszamjatek.txt",Encoding.UTF8);
            int db = 0;
            while(!sr.EndOfStream)
            {
                SzamjatekList.Add(new Szamjatek(sr.ReadLine()));
                db++;
            }
            sr.Close();
            if(db>0)
            { Console.WriteLine("\tSikeres beolvasás, beolvasott sorok száma : {0}", db); }
            else
            { Console.WriteLine("\tSikertelen beolvasás"); }

        }
    }
}
