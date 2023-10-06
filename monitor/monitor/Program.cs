using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace monitor
{
    internal class Program
    {
        static void Keszletosszeg(List<Monitor> a)
        {
            var nyitoosszeg = a.Sum(m => m.Bruttoar * m.DarabSzam);

            Console.WriteLine($"A bruttó készlet ára: {nyitoosszeg:0}Ft");
        }

        static void Dragamonitorok(List<Monitor> a)
        {
            using var sw = new StreamWriter(@"..\..\..\src\dragamonitorok.txt");
            Console.WriteLine($"50000ft fölötti monitorok:");
            sw.WriteLine($"50000ft fölötti monitorok:");
            foreach (Monitor m in a)
            {
                if (m.NettoAr > 50000)
                {
                    sw.WriteLine($"Gyártó: {m.Gyarto.ToUpper()}; Típus: {m.Tipus.ToUpper()}; Méret: {m.Meret} col; Nettó ár: {m.NettoAr / 1000} ezer Ft; Darab raktáron: {m.DarabSzam}");
                    Console.WriteLine($"Gyártó: {m.Gyarto.ToUpper()}; Típus: {m.Tipus.ToUpper()}; Méret: {m.Meret} col; Nettó ár: {m.NettoAr / 1000} ezer Ft; Darab raktáron: {m.DarabSzam}");
                }
            }
        }

        static void MonitorKereses(List<Monitor> a)
        {
            var atlag = a.Average(m => m.Bruttoar);
            var kereses = a.Where(m => m.Bruttoar > atlag).First();
            Console.WriteLine($"\nMilyen monitort keresel: ");
            string keresettmonitor = Console.ReadLine().ToLower();
            bool vane = false;
            int sorszam = 0;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Gyarto.ToLower() == keresettmonitor || a[i].Tipus.ToLower() == keresettmonitor)
                {
                    vane = true;
                    sorszam = i;
                    break;
                }

            }
            if (vane)
            {
                Console.WriteLine($"Van ilyen monitorból {a[sorszam].DarabSzam}db");
            }
            else
            {
                Console.WriteLine($"Sajnos ilyen monitor nincs készleten, viszont tudunk átlagárért ajánlani egy ilyet: \n{kereses}");
            }
        }
        static void Legolcsobb(List<Monitor> a)
        {
            double legocslobb = 5000000;
            int sorszam = 0;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Bruttoar < legocslobb)
                {
                    legocslobb = a[i].Bruttoar;
                    sorszam = i;
                }
            }
            Console.WriteLine($"\nA legolcsóbb monitor mérete: {a[sorszam].Meret} és ára: {legocslobb}");
        }

        static void SamsungAkcio(List<Monitor> a)
        {
            double learazottbrutto = 0;
            double learazatlanbrutto = 0;
            foreach (Monitor m in a)
            {
                if (m.Gyarto == "Samsung" && m.Bruttoar > 70000)
                {
                    learazottbrutto += m.Bruttoar * 0.95;
                    learazatlanbrutto += m.Bruttoar;
                }
            }
            Console.WriteLine($"\nHa a cég akciósan adná el az összes samsung monitort 70e fölött ennyit veszítene: {learazatlanbrutto * 15 - learazottbrutto * 15:0}Ft\n");
        }

        static void NettoAtlag(List<Monitor> a)
        {
            var nettoatlag = a.Average(m => m.NettoAr);
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].NettoAr > nettoatlag)
                {
                    Console.WriteLine($"A {a[i].Gyarto} {a[i].Tipus} monitor a nettó átlag fölött van");
                }
                else if (a[i].NettoAr < nettoatlag)
                {
                    Console.WriteLine($"A {a[i].Gyarto} {a[i].Tipus} monitor a nettó átlag alatt van");
                }
                else
                {
                    Console.WriteLine($"A {a[i].Gyarto} {a[i].Tipus} monitor a nettó átlag pontos értéke");
                }
            }
        }

        static void Csokkentes(List<Monitor> a)
        {
            double legdragabb = 0;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Bruttoar > legdragabb)
                {
                    legdragabb = a[i].Bruttoar;
                }
            }
            Console.WriteLine($"\nA legdrágább monitor ára le lett csökkentve 10%-al\nRégi ár: {legdragabb}\nÚj ár: {legdragabb * 0.90}");
        }

        static void Megrohamozas(List<Monitor> a)
        {
            var rnd = new Random();
            int i = 0;
            int valasztott = 0;
            int vasarlok = rnd.Next(5, 16);
            while (a.Count > i)
            {
                valasztott = rnd.Next(0, a.Count);
                a[valasztott].DarabSzam -= vasarlok;
                i++;
            }
            Console.WriteLine($"\nEnnyi monitor maradt a rohamozás után: ");
            foreach (Monitor m in a)
            {
                Console.WriteLine(m);
            }
        }

        static void Vanemeg(List<Monitor> a)
        {
            bool vanemeg = true;
            int indexx = 0;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].DarabSzam == 0 || a[i].DarabSzam < 0)
                {
                   vanemeg = false;
                    indexx = i;
                }
            }
            if (vanemeg)
            {
                Console.WriteLine("\nNem fogyott el teljesen egy monitor se a megrohamozás után");
            }
            else
            {
                Console.WriteLine($"\nVan olyan monitor ami elfogyott a rohamozás után: \n{a[indexx]}");
            }
        }

        static void Abc(List<Monitor> a)
        {
            List<Monitor> abc = new List<Monitor>(a);
            var abcsorrend = abc.OrderBy(m => m.Gyarto);
            Console.WriteLine($"\nA gyártók ABC sorrendben: ");
            foreach (var item in abcsorrend)
            {
                Console.WriteLine($"{item.Gyarto}");
            }

        }

        static void Main(string[] args)
        {
            var monitorok = new List<Monitor>();
            using var sr = new StreamReader(@"..\..\..\src\monitorok.txt");

            _ = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                monitorok.Add(new Monitor(sr.ReadLine()));
            }

            foreach (var item in monitorok)
            {
                Console.WriteLine(item);
            }

            Keszletosszeg(monitorok);
            Dragamonitorok(monitorok);
            MonitorKereses(monitorok);
            Legolcsobb(monitorok);
            SamsungAkcio(monitorok);
            NettoAtlag(monitorok);
            Megrohamozas(monitorok);
            Vanemeg(monitorok);
            Abc(monitorok);
            Csokkentes(monitorok);



            Console.ReadLine();

            

        }
    }
}
