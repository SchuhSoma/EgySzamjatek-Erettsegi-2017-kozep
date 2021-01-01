using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuhS_Egyszamjatek
{
    class Szamjatek
    {
        public List<int> SzamTippek;
        public string Nev;
        public int JatekokSzama
        {
            get
            {
                return  SzamTippek.Count;
            }
        }
           
        public Szamjatek (string sor)
        {
            var dbok = sor.Split(' ');
            SzamTippek = new List<int>();
            for (int i = 0; i < dbok.Length-1; i++)
            {
               SzamTippek.Add(int.Parse(dbok[i]));
            }
            this.Nev = dbok[dbok.Length-1];
            
        }
    }
}
