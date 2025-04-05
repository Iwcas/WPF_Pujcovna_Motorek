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
        public int Max_Nadrz
        {
            get { return max_Nadrz; }
            set
            {
                if (value <= 0) // Check if the value is valid
                {
                    throw new ArgumentException("MaxNadrz musi byt vetsi nez 0.");
                }
                max_Nadrz = value;
            }
        }
        public int Aktualni_Nadrz
        {
            get { return aktualni_Nadrz; }
            set
            {
                if (value < 0) // Can't be less than 0
                {
                    throw new ArgumentException("AktualniNadrz nemuze byt mensi nez 0.");
                }
                if (value > max_Nadrz) // Can't exceed maximum tank capacity
                {
                    throw new ArgumentException("AktualniNadrz nemuzes byt vetsi nez Max Nadrz.");
                }
                aktualni_Nadrz = value;
            }
        }
        public DateTime Rok_Vyroby { get; set; }
        public string Barva {  get; set; }
        public bool Volny { get; set; }

        public Motorka(int id, string nazev, string model, int najezd, int _max_Nadrz, int _aktualni_Nadrz, DateTime rok_Vyroby, string barva, bool volny)
        {
            Id = id;
            Nazev = nazev;
            Model = model;
            Najezd = najezd;
            Max_Nadrz = _max_Nadrz;
            Aktualni_Nadrz = _aktualni_Nadrz;
            Rok_Vyroby = rok_Vyroby;
            Barva = barva;
            Volny = volny;
        }
    }
}
