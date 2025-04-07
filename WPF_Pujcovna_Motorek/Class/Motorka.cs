using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Pujcovna_Motorek.Class
{
    internal class Motorka
    {
        // Private fields for storing the values
        private int max_Nadrz;          // Maximum tank capacity
        private int aktualni_Nadrz;     // Current tank level

        public int Id { get; set; }
        public string Nazev { get; set; }
        public string Model { get; set; }
        public int Najezd { get; set; }
        public int Max_Nadrz { get; set; }
        public string Barva {  get; set; }

        public Motorka(int id, string nazev, string model, int najezd, int _max_Nadrz, string barva)
        {
            Id = id;
            Nazev = nazev;
            Model = model;
            Najezd = najezd;
            Max_Nadrz = _max_Nadrz;
            Barva = barva;
        }
    }
}
