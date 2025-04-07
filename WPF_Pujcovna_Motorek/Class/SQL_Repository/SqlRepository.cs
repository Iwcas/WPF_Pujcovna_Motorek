using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WPF_Pujcovna_Motorek.Class.SQL_Repository
{
    class SqlRepository
    {
        public string Connection_String { get; set; }

        public SqlRepository(string connection_String)
        {
            Connection_String = connection_String;
        }

        public List<Vypujcka> NactiVypujcky(string sloupecTrideni, bool sestupne, int hledani)
        {
            List<Vypujcka> vypujcka = new List<Vypujcka>();
            using (SqlConnection conn = new SqlConnection(Connection_String))
            {
                using(SqlCommand cmd = new SqlCommand("", conn))
                {
                    string text_hledani = hledani == 0 ? "" : $"where Vypujcky.Id_Zakaznik = {hledani} ";
                    string jak_Tridit = !sestupne ? "asc" : "desc";
                    cmd.CommandText = 
                        $"select Vypujcky.Id as VId, Jmeno, Prijmeni, Motorka.Id as MId, Zakaznik.Id as ZId, Motorka.Nazev as MNazev, " +
                        $"Vypujcky.Pujceno as VPujceno, " +
                        $"Vypujcky.Vraceno as VVraceno " +
                        $"from Vypujcky " +
                        $"inner join Zakaznik on (Vypujcky.Id_Zakaznik = Zakaznik.Id) " +
                        $"inner join Motorka on (Vypujcky.Id_Motorka = Motorka.Id) " +
                        $"{text_hledani}" +
                        $"order by {sloupecTrideni} {jak_Tridit};";
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            vypujcka.Add(new Vypujcka(int.Parse(dr["VId"].ToString()),
                                int.Parse(dr["ZId"].ToString()),
                                int.Parse(dr["MId"].ToString()),
                                dr.GetDateTime("VPujceno"),
                                !dr.IsDBNull("VVraceno") ? dr.GetDateTime("VVraceno") : null,
                                dr["Jmeno"].ToString().Trim(),
                                dr["Prijmeni"].ToString().Trim(),
                                dr["MNazev"].ToString().Trim()));
                        }
                    }
                    conn.Close();
                }
            }
            return vypujcka;
        }

        public List<Zakaznik> NactiZakaznika(string sloupecTrideni, bool sestupne, string hledani)
        {
            List<Zakaznik> zakaznik = new List<Zakaznik>();
            zakaznik.Add(new Zakaznik(0, "", "Vše"));
            using (SqlConnection conn = new SqlConnection(Connection_String))
            {
                using (SqlCommand cmd = new SqlCommand("", conn)) 
                {
                    cmd.CommandText = $"select * from Zakaznik";
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zakaznik.Add(new Zakaznik(
                                int.Parse(reader["Id"].ToString()),
                                reader["Jmeno"].ToString(),
                                reader["Prijmeni"].ToString()
                                ));
                        }
                    }
                    conn.Close();
                }
            }
            return zakaznik;
        }

        public List<Motorka> NactiMotorky(string sloupecTrideni, bool sestupne, string hledani)
        {
            List<Motorka> motorka = new List<Motorka>();
            using(SqlConnection conn = new SqlConnection(Connection_String))
            {
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = $"select * from Motorka";
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            motorka.Add(new Motorka(
                                int.Parse(dr["Id"].ToString()),
                                dr["Nazev"].ToString(),
                                dr["Model"].ToString(),
                                int.Parse(dr["Najezd"].ToString()),
                                dr["Barva"].ToString()
                                ));
                        }
                    }
                }
            }
            return motorka;
        }

        public Vypujcka VratJednuVypujcku(int Id)
        {
            Vypujcka vypujcka = null;
            using (SqlConnection conn = new SqlConnection(Connection_String))
            {
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = $"select * from Vypujcky Where Id = {Id}";
                    conn.Open();
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            vypujcka = new Vypujcka(
                                (int)dr["Id"],
                                (int)dr["Id_Zakaznik"],
                                (int)dr["Id_Motorka"],
                                dr.GetDateTime("Pujceno"),
                                !dr.IsDBNull("Vraceno") ? dr.GetDateTime("Vraceno") : null);
                        }
                    }
                    conn.Close();
                }
            }

            return vypujcka;
        }
        
        public void VypujcitMotorku(int IdZakaznika, int IdMotorka, DateTime Pujceno)
        {
            using(SqlConnection conn =new SqlConnection(Connection_String))
            {
                using (SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = $"insert into Vypujcky (Id_Zakaznik, Id_Motorka, Pujceno)" +
                        $"values (@IdZ, @IdM, @P);";
                    cmd.Parameters.AddWithValue("IdZ", IdZakaznika);
                    cmd.Parameters.AddWithValue("IdM", IdMotorka);
                    cmd.Parameters.AddWithValue("P", Pujceno);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public void EditovatMotorku(int Id, int IdZakaznik, int IdMotorka, DateTime Pujceno, DateTime Vraceno)
        {
            using(SqlConnection conn = new SqlConnection(Connection_String))
            {
                using(SqlCommand cmd = new SqlCommand("", conn))
                {
                    cmd.CommandText = $"update Vypujcky " +
                        $"set Id_Zakaznik=@IdZ, Id_Motorka=@IdM, Pujceno=@P, Vraceno=@V " +
                        $"where Id=@Id;";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@IdZ", IdZakaznik);
                    cmd.Parameters.AddWithValue("@IdM", IdMotorka);
                    cmd.Parameters.AddWithValue("@P", Pujceno);
                    cmd.Parameters.AddWithValue("@V", Vraceno);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
