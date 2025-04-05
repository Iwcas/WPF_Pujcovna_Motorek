using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Pujcovna_Motorek
{
    internal class Motorka
    {
        public int Id { get; set; }
        public string Nazev { get; set; }
        public string Model { get; set; }
        public int Najezd { get; set; }
        public int Max_Nadrz
        {
            get { return Max_Nadrz; }
            set
            {
                if (value <= 0) // Check if the value is valid
                {
                    throw new ArgumentException("MaxNadrz musi byt vetsi nez 0.");
                }
                Max_Nadrz = value;
            }
        }
        public int Aktualni_Nadrz
        {
            get { return Aktualni_Nadrz; }
            set
            {
                if (value < 0) // Can't be less than 0
                {
                    throw new ArgumentException("AktualniNadrz nemuze byt mensi nez 0.");
                }
                if (value > Max_Nadrz) // Can't exceed maximum tank capacity
                {
                    throw new ArgumentException("AktualniNadrz nemuzes byt vetsi nez Max Nadrz.");
                }
                Aktualni_Nadrz = value;
            }
        }
        public DateTime Rok_Vyroby { get; set; }
        public string Barva {  get; set; }
        public bool Volny { get; set; }

        public Motorka(int id, string nazev, string model, int najezd, int max_Nadrz, int aktualni_Nadrz, DateTime rok_Vyroby, string barva, bool volny)
        {
            Id = id;
            Nazev = nazev;
            Model = model;
            Najezd = najezd;
            Max_Nadrz = max_Nadrz;
            Aktualni_Nadrz = aktualni_Nadrz;
            Rok_Vyroby = rok_Vyroby;
            Barva = barva;
            Volny = volny;
        }
    }
}
