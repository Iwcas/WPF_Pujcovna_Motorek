select Vypujcky.Id as VId, Jmeno, Prijmeni, Motorka.Id as MId, Zakaznik.Id as ZId, Motorka.Nazev as MNazev,
                        Motorka.model as MModel, Motorka.Najezd as MNajezd, Motorka.Aktualni_Nadrz as MAkt_nadrz,
                        Motorka.RokVyroby as MRok, Motorka.Barva as MBarva, Vypujcky.Pujceno as VPujceno,
                        Vypujcky.Vraceno as VVraceno
                        from Vypujcky
                        inner join Zakaznik on (Vypujcky.Id_Zakaznik = Zakaznik.Id)
                        inner join Motorka on (Vypujcky.Id_Motorka = Motorka.Id)