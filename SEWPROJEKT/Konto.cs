using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEWPROJEKT
{
    public class Konto
    {

        public decimal Kontostand { get; set; }

        public  Konto()
        {

        }

        /* Test Versuch für Uplode funktionierte nicht ganz
        public decimal Betrag { get; set; }
        public string Text { get; set; }
        public DateTime Zeitpunkt { get; set; }
        

        public Konto(decimal betrag, string text, DateTime zeitpunkt)
        {
            this.Betrag = betrag;
            this.Text = text;
            this.Zeitpunkt = zeitpunkt;
        }
        public override string ToString()
        {
            return ($"{Betrag} Euro - {Text}-{Zeitpunkt}");
        }*/

    }
}