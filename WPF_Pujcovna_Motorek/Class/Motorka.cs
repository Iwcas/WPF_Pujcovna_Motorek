using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Pujcovna_Motorek.Class
{
    internal class Motorka
    {
        public int Id { get; set; }
        public string Nazev { get; set; }
        public string Model { get; set; }
        public string Barva {  get; set; }

        public Motorka(int id, string nazev, string model, string barva)
        {
            Id = id;
            Nazev = nazev;
            Model = model;
            Barva = barva;
        }
    }
}
