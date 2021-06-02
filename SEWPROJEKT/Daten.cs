using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEWPROJEKT
{
    class Daten
    {
        public decimal Betrag { get; set; }
        public string Text { get; set; }
        public DateTime Zeitpunkt { get; set; }
        
        public double Eingang { get; set; }
        public double Ausgang { get; set; }

        public Daten()
        {

        }
        //Methode

        public override string ToString()
        {
            return ($"{Betrag} Euro - {Text}-{Zeitpunkt}");
        }
    }
}
