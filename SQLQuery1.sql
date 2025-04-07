select Vypujcky.Id as VId, Jmeno, Prijmeni, Motorka.Id as MId, Zakaznik.Id as ZId, Motorka.Nazev as MNazev,
                        Vypujcky.Pujceno as VPujceno,
                        Vypujcky.Vraceno as VVraceno
                        from Vypujcky
                        inner join Zakaznik on (Vypujcky.Id_Zakaznik = Zakaznik.Id)
                        inner join Motorka on (Vypujcky.Id_Motorka = Motorka.Id) 