using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SEWPROJEKT
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int aktuellerWert = 0;
        int letztesErgebnis = 0;
        bool zahelnEingabeLäuft = true;
        char letzteRechernoperation = '=';
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VorleseButton_Click(object sender, RoutedEventArgs e)
        {
            //Spracheausgabe mit SpeechSynthesizer
           SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Speak(AusgangLabel.Content.ToString());
        }

        private void VorleseButton2_Click(object sender, RoutedEventArgs e)
        {
            //Spracheausgabe mit SpeechSynthesizer
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Speak(KontoLabel.Content.ToString());
        }

        List<string> listrechnung = new List<string>();

        decimal Stand = 0;
        decimal Konto = 25000;
        public long ToFile { get; }

        private void EingangButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RechnungsListBox.Items.Add(new Daten() { Betrag = Convert.ToDecimal(BetragTextBox.Text), Text = BeschreibungsTextBox.Text, Zeitpunkt = DateTime.Now });
                Stand += Convert.ToDecimal(BetragTextBox.Text);
                Konto = (Konto) + (Stand);
                AusgangLabel.Content = ($"{Stand}€ Euro");
                KontoLabel.Content = ($"{Konto}€");
            }
            catch
            {
                MessageBox.Show($"Es wurde ein Buchstabe/Zeichen verwendet in {BetragTextBox.Text}.","Information",MessageBoxButton.OK, MessageBoxImage.Information); ;
            }
        }
        /*
        private void AusgangButton_Click(object sender, RoutedEventArgs e)
        {
            // Diser Button ist einfach nur unötig, aber Egal !
            try
            {
                RechnungsListBox.Items.Add(new Daten() { Betrag = Convert.ToDecimal(BetragTextBox.Text), Text = BeschreibungsTextBox.Text, Zeitpunkt = DateTime.Now });
                Stand += Convert.ToDecimal(BetragTextBox.Text);
                // Konto = Konto +- (Stand);
                AusgangLabel.Content = ($"{Stand}€");
                // KontoLabel.Content = ($"{Konto}€");
            }
            catch
            {
                MessageBox.Show($"Es wurde ein Buchstabe/Zeichen verwendet in {BetragTextBox.Text}.");
            }
        }
        */
        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            //Änder durch Suchen des Pathn FILE IO, also Speichern in JSON oder CSV
            string path = "";
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    foreach (var item in RechnungsListBox.Items)
                    {
                        Daten b = (Daten)item;
                        sw.WriteLine($"{b.Betrag},{b.Text},{b.Zeitpunkt}");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Da ist ein Fehler passsiert","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw;
            }

        }

        private void LadenButton_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            OpenFileDialog opF = new OpenFileDialog();
            if (opF.ShowDialog() == true)
            {
                path = opF.FileName;
                MessageBox.Show($"Es wurde in dem Pfad gespeichert: {path}");
            }
            else
            {
                MessageBox.Show($"Abbruch","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string countTxt = Convert.ToString(RechnungsListBox.Items.Count);
            var itemsTxt = Convert.ToString(RechnungsListBox.Items.IndexOf(RechnungsListBox));


            DatenListBox.Items.Add($"Daten in Liste {countTxt}, Bescheibung: {itemsTxt}, Datum: {DateTime.Now}");
        }

        private void DatenListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RechnungLöschenButton_Click(object sender, RoutedEventArgs e)
        {
            RechnungsListBox.Items.Remove(RechnungsListBox.SelectedItem);

            /// Stand = Convert.ToDecimal(LöschSummeTextBox.Text);

            if (Stand >= 0)
            {
                Stand -= Convert.ToDecimal(BetragTextBox.Text);
                //Konto = Konto - (Stand);
                AusgangLabel.Content = ($"{Stand}€ Euro");
                // KontoLabel.Content = ($"{Konto}€");
            }
            else if (Stand <= 0)
            {
                Stand += Convert.ToDecimal(BetragTextBox.Text);
                //Konto = Konto + (Stand);
                AusgangLabel.Content = ($"{Stand}€ Euro");
                // KontoLabel.Content = ($"{Konto}€");
            }

        }

        private void RechnungsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listrechnung.Add(Convert.ToString(RechnungsListBox.SelectedItem));

        }
    //Taschenrechner
        private void ZifferAnhängen(int z)
        {
            if (zahelnEingabeLäuft)
            {
                aktuellerWert = aktuellerWert * 10 + z;
            }
            else
            {
                aktuellerWert = z;
                zahelnEingabeLäuft = true;
            }
            textBlockAusgabe.Text = aktuellerWert.ToString();
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(0);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(1);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(2);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(3);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(4);
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(5);
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(6);
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(7);
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(8);
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            ZifferAnhängen(9);
        }

        private void Rechne(char rechenoperation)
        {
            switch (letzteRechernoperation)
            {
                case '+':
                    letztesErgebnis = letztesErgebnis + aktuellerWert;
                    break;
                case '-':
                    letztesErgebnis = letztesErgebnis - aktuellerWert;
                    break;
                case '*':
                    letztesErgebnis = letztesErgebnis * aktuellerWert;
                    break;
                case '/':
                    letztesErgebnis = letztesErgebnis / aktuellerWert;
                    break;
                case '=':
                    // TODO: Gleichheitszeichen mehrfach drücken?!
                    // TODO: +, -, *, / direkt nach Gleichheitszeichen?!
                    letztesErgebnis = aktuellerWert;
                    break;
            }
            textBlockAusgabe.Text = letztesErgebnis.ToString();
            zahelnEingabeLäuft = false;
            letzteRechernoperation = rechenoperation;
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            Rechne('+');
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            Rechne('-');
        }

        private void ButtonMal_Click(object sender, RoutedEventArgs e)
        {
            Rechne('*');
        }

        private void ButtonGeteilt_Click(object sender, RoutedEventArgs e)
        {
            Rechne('/');
        }

        private void ButtonGleich_Click(object sender, RoutedEventArgs e)
        {
            Rechne('=');
        }
    }

}
