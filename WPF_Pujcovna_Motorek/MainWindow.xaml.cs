using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Pujcovna_Motorek.Class;
using WPF_Pujcovna_Motorek.Class.SQL_Repository;

namespace WPF_Pujcovna_Motorek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\okurk\source\repos\WPF_Pujcovna_Motorek\WPF_Pujcovna_Motorek\Pujcovna.mdf;Integrated Security=True";
        bool sestupne = false;
        string sloupecTrideni = "Vypujcky.Id";
        int hledani = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ZobrazVypujcky(string sloupecTrideni, bool sestupne, int hledani)
        {
            List<Vypujcka> vypujcka = new SqlRepository(cs).NactiVypujcky(sloupecTrideni, sestupne, hledani);
            lsvVypujcky.ItemsSource = vypujcka;
        }
        public void ZobrazZakaznika()
        {
            List<Zakaznik> Zakaznik = new SqlRepository(cs).NactiZakaznika("", true, "");
            List<Zakaznik> zakaznikLsv = Zakaznik.Where(c => c.Id != 0).ToList();
            lsvZakazniciVypujcky.ItemsSource = zakaznikLsv;
        }
        public void ZobrazMotorky()
        {
            List<Motorka> Motorka = new SqlRepository(cs).NactiMotorky("", true, "");
            lsvMotorkyVypujcky.ItemsSource= Motorka;
        }
        private void colId_Click(object sender, RoutedEventArgs e)
        {
            sloupecTrideni = "Vypujcky.Id";
            sestupne = !sestupne;
            ZobrazVypujcky(sloupecTrideni, sestupne, hledani);
        }

        private void ColJmeno_Click(object sender, RoutedEventArgs e)
        {
            sloupecTrideni = "Ctenari.Jmeno";
            sestupne = !sestupne;
            ZobrazVypujcky(sloupecTrideni, sestupne, hledani);
        }

        private void ColPrijmeni_Click(object sender, RoutedEventArgs e)
        {
            sloupecTrideni = "Ctenari.Prijmeni";
            sestupne = !sestupne;
            ZobrazVypujcky(sloupecTrideni, sestupne, hledani);
        }

        private void ColMotorka_Click(object sender, RoutedEventArgs e)
        {
            sloupecTrideni = "Motorka.Nazev";
            sestupne = !sestupne;
            ZobrazVypujcky(sloupecTrideni, sestupne, hledani);
        }

        private void menuZakazniciUkoncit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuVypujckyUkoncit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvVypujcky_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lsvVypujcky.SelectedItems.Count > 0)
            {
                Vypujcka vypujcka = (Vypujcka)lsvVypujcky.SelectedItem;
                foreach(Zakaznik zakaznik in lsvZakazniciVypujcky.Items)
                {
                    if(zakaznik.Id == vypujcka.Id_Zakaznik)
                    {
                        lsvZakazniciVypujcky.SelectedItem = zakaznik;
                    }
                }

                foreach (Motorka motorka in lsvMotorkyVypujcky.Items)
                {
                    if (motorka.Id == vypujcka.Id_Motorka)
                    {
                        lsvMotorkyVypujcky.SelectedItem = motorka;
                    }
                }
                CalVypujceno.SelectedDate = vypujcka.Pujceno;
                CalVypujceno.DisplayDate = vypujcka.Pujceno;
                CalVraceno.SelectedDate = vypujcka.Vraceno;
                CalVraceno.DisplayDate = (DateTime)(vypujcka.Vraceno != null ? vypujcka.Vraceno : DateTime.Now);
            }
        }

        private void BtnEditovatVypujcku_Click(object sender, RoutedEventArgs e)
        {
            if (lsvZakazniciVypujcky.SelectedItems.Count > 0 && lsvMotorkyVypujcky.SelectedItems.Count > 0 &&
                lsvVypujcky.SelectedItems.Count > 0)
            {
                SqlRepository sqlRepository = new SqlRepository(cs);
                Vypujcka vypujcka = (Vypujcka)lsvVypujcky.SelectedItem;
                Zakaznik ctenar = (Zakaznik)lsvZakazniciVypujcky.SelectedItem;
                Motorka kniha = (Motorka)lsvMotorkyVypujcky.SelectedItem;
                DateTime pujceno = CalVypujceno.SelectedDate == null ?
                    DateTime.Now :
                    (DateTime)CalVypujceno.SelectedDate;
                DateTime vraceno = CalVraceno.SelectedDate == null ?
                    DateTime.Now :
                    (DateTime)CalVraceno.SelectedDate;

                sqlRepository.EditovatMotorku(vypujcka.Id,
                    ctenar.Id,
                    kniha.Id,
                    pujceno,
                    vraceno);
                ZobrazVypujcky(sloupecTrideni, sestupne, hledani);
            }
            else
            {
                MessageBox.Show("Vyberte výpůjčku, Motorku a Zakaznika");
            }
        }

        private void BtnVlozitVypujcku_Click(object sender, RoutedEventArgs e)
        {
            if (lsvZakazniciVypujcky.SelectedItems.Count > 0 && lsvMotorkyVypujcky.SelectedItems.Count > 0)
            {
                SqlRepository sqlRepository = new SqlRepository(cs);
                Zakaznik zakaznik = (Zakaznik)lsvZakazniciVypujcky.SelectedItem;
                Motorka motorka = (Motorka)lsvMotorkyVypujcky.SelectedItem;
                int pocetPujcenych = 0;
                foreach (Vypujcka vypujcka in lsvVypujcky.Items)
                {
                    pocetPujcenych += (vypujcka.Vraceno == null && vypujcka.Id_Motorka == motorka.Id) ? 1 : 0;
                }
                DateTime pujceno = DateTime.Now;
                if (pocetPujcenych == 0)
                {
                    sqlRepository.VypujcitMotorku(
                        int.Parse(zakaznik.Id.ToString()),
                        int.Parse(motorka.Id.ToString()),
                        pujceno);
                    ZobrazVypujcky(sloupecTrideni, sestupne, hledani);
                }
                else
                {
                    MessageBox.Show($"Motorku {motorka.Nazev.Trim()} musíte nejdříve vrátit!!!");
                }
            }
            else
            {
                MessageBox.Show("Vyberte Motorku a Zakaznika");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ZobrazVypujcky(sloupecTrideni, sestupne, hledani);
            ZobrazZakaznika();
            ZobrazMotorky();
            CalVraceno.SelectedDate = null;
        }
    }
}