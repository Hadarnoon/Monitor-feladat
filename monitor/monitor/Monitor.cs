using System;
using System.Collections.Generic;
using System.Text;

namespace monitor
{
    internal class Monitor
    {
        public string Gyarto { get; set; }
        public string Tipus { get; set; }
        public double Meret { get; set; }
        public int NettoAr { get; set; }
        public bool Gamer { get; set; }
        public double Bruttoar { get; set; }
        public int DarabSzam { get; set; }
        public double Atlag { get; set; }

        public Monitor(string sor)
        {
            var v = sor.Split(';');
            this.Gyarto = v[0];
            this.Tipus = v[1];
            this.Meret = double.Parse(v[2]);
            this.NettoAr = int.Parse(v[3]);
            if (v.Length > 4)
            {
                this.Gamer = true;
            }
            else 
            { 
                this.Gamer = false; 
            }
            this.Bruttoar = NettoAr * 1.27;
            this.DarabSzam = 15;
        }
        public override string ToString()
        {
            return $"Gyártó: {Gyarto}; Típus: {Tipus}; Méret: {Meret} col; Nettó ár: {NettoAr} Ft; Darabszám: {DarabSzam}";
        }
    }
}
