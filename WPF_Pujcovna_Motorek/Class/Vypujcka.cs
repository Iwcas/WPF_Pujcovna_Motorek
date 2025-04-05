using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Pujcovna_Motorek.Class
{
    internal class Vypujcka
    {
        public int Id { get; set; }
        public int Id_Zakaznik {  get; set; }
        public int Id_Motorka { get; set; }
        public DateTime Pujceno { get; set; }
        public DateTime? Vraceno { get; set; }
        public string Jmeno_Zakaznika { get; set; }
        public string Prijmeni_Zakaznika { get; set; }
        public string Nazev_Motorky { get; set; }

        public Vypujcka(int id, int id_Zakaznik, int id_Motorka, DateTime pujceno, DateTime? vraceno)
        {
            Id = id;
            Id_Zakaznik = id_Zakaznik;
            Id_Motorka = id_Motorka;
            Pujceno = pujceno;
            Vraceno = vraceno;
        }

        public Vypujcka(int id, int id_Zakaznik, int id_Motorka, DateTime pujceno, DateTime? vraceno, string jmeno_Zakaznika, string prijmeni_Zakaznika, string nazev_Motorky) : this(id, id_Zakaznik, id_Motorka, pujceno, vraceno)
        {
            Jmeno_Zakaznika = jmeno_Zakaznika;
            Prijmeni_Zakaznika = prijmeni_Zakaznika;
            Nazev_Motorky = nazev_Motorky;
        }
    }
}
