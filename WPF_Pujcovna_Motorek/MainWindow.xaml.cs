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
            cmbZakazniciFiltr.ItemsSource = Zakaznik;
            if(Zakaznik.Count > 0)
            {
                cmbZakazniciFiltr.SelectedIndex = 0;
            }
        }
        public void ZobrazMotorky()
        {
            List<Motorka> Motorka = new SqlRepository(cs).NactiMotorky("", true, "");
            lsvMotorkyVypujcky.ItemsSource= Motorka;
        }
        private void colId_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColJmeno_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColPrijmeni_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColMotorka_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbZakazniciFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void menuZakazniciUkoncit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuVypujckyUkoncit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void lsvVypujcky_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lsvVypujcky.SelectedItems.Count > 0)
            {
                Vypujcka vypujcka = (Vypujcka)lsvVypujcky.SelectedItems;
                foreach(Zakaznik zakaznik in lsvVypujcky.Items)
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

        }

        private void BtnVlozitVypujcku_Click(object sender, RoutedEventArgs e)
        {
            if(lsvZakazniciVypujcky.SelectedItems.Count > 0 && lsvMotorkyVypujcky.SelectedItems.Count > 0)
            {
                SqlRepository sqlRepository = new SqlRepository(cs);
                Zakaznik zakaznik = (Zakaznik)lsvZakazniciVypujcky.SelectedItems;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Zakaznik zakaznik = (Zakaznik)cmbZakazniciFiltr.SelectedItem;
            hledani = zakaznik != null ? zakaznik.Id : 0;
            ZobrazVypujcky(sloupecTrideni, sestupne, hledani);
            ZobrazZakaznika();
            ZobrazMotorky();
        }
    }
}