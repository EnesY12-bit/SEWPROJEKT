using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //Taschenrechner
        int aktuellerWert = 0;
        int letztesErgebnis = 0;
        bool zahelnEingabeLäuft = true;
        char letzteRechernoperation = '=';

        decimal Stand = 0;
        decimal Konto;
       // decimal Konto = 2500;
        // Ende Taschenrechner
        public MainWindow()
        {
            InitializeComponent();
            
            //Konto k = new Konto() { Kontostand = 2500 };
            Konto = 2500;
           // decimal Konto = Convert.ToDecimal(k.Kontostand);
        }

        //Hinzufüge von einem Speaker im Taschenrechner und CSV Datein Uplode und noch ein Button zum Schließen des Fenster Neue Idee!! noch nicht drinnen.

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

        //ObservabelCollection
        ObservableCollection<Daten> buchungen = new ObservableCollection<Daten>();

        List<string> listrechnung = new List<string>();

        


        public long ToFile { get; }

  
        private void EingangButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RechnungsListBox.Items.Add(new Daten() { Betrag = Convert.ToDecimal(BetragTextBox.Text), Text = BeschreibungsTextBox.Text, Zeitpunkt = DateTime.Now });
                //Speicher die Daten in eine ObservableCollation extra
                buchungen.Add(new Daten() { Betrag = Convert.ToDecimal(BetragTextBox.Text), Text = BeschreibungsTextBox.Text, Zeitpunkt = DateTime.Now });
                //
                Stand += Convert.ToDecimal(BetragTextBox.Text);
                //Konto = Convert.ToDecimal(KontoTextBox.Text) + (Stand);
                Konto = (Konto) + Convert.ToDecimal(BetragTextBox.Text);
                AusgangLabel.Content = ($"{Stand}€");
                //KontoTextBox.Text = ($"{Konto}€");
                KontoLabel.Content = ($"{Konto}€");
            }
            catch
            {
                MessageBox.Show($"Es wurde ein Buchstabe/Zeichen verwendent in {BetragTextBox.Text}.","Information",MessageBoxButton.OK, MessageBoxImage.Information); ;
            }
        }
        /* Brauch man nicht ist unötig gewesen
        private void AusgangButton_Click(object sender, RoutedEventArgs e)
        {
            // Diser Button ist einfach nur unötig, aber Egal !
            trys
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
            //Wird durch einen einfach in ein CSV Datei gespeichert            
            //string path = ""; --> Durch CSV
            try
            {                                             //path, true
                using (StreamWriter sw = new StreamWriter("Speicher.csv"))
                {
                    foreach (var item in RechnungsListBox.Items)
                    {
                        Daten b = (Daten)item;
                        sw.WriteLine($"{b.Betrag};{b.Text};{b.Zeitpunkt}");
                    }
                }

                MessageBox.Show("Gespeichert!");
            }
            catch (Exception)
            {
                MessageBox.Show("Da ist ein Fehler passsiert","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
       /* private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            RechnungsListBox.Items.Clear();
            using (StreamReader sr = new StreamReader("Hochladen.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splittedLine = line.Split(';');

                    Konto k = new Konto(Convert.ToInt32(splittedLine[0]), splittedLine[1], Convert.ToDateTime(splittedLine[2]));
                    RechnungsListBox.Items.Add(k);
                    //Stand += Convert.ToDecimal(BetragTextBox.Text);
                }
            }
        }
       */

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
            //var itemsTxt = Convert.ToString(RechnungsListBox.Items.IndexOf(RechnungsListBox));


            DatenListBox.Items.Add($"Daten in Liste {countTxt}, Datum: {DateTime.Now}"); // Bescheibung:{itemsTxt}

        }

        private void DatenListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void RechnungLöschenButton_Click(object sender, RoutedEventArgs e)
        {
            // RechnungsListBox.Items.Remove(RechnungsListBox.SelectedItem);
            /// Stand = Convert.ToDecimal(LöschSummeTextBox.Text);
            try
            {
                if (Stand >= 0)
                {
                    Stand -= Convert.ToDecimal(BetragTextBox.Text);
                    //Konto = Convert.ToDecimal(KontoTextBox.Text) - (Stand);
                    //Konto funktioniert nicht ganz
                    Konto = Konto - Convert.ToDecimal(BetragTextBox.Text);
                    AusgangLabel.Content = ($"{Stand}€");
                    KontoLabel.Content = ($"{Konto}€");
                    //KontoTextBox.Text = ($"{Konto}€");
                    RechnungsListBox.Items.Remove(RechnungsListBox.SelectedItem);
                }
                else if (Stand <= 0)
                {
                    //Konto funktioniert nichz ganz
                    Stand += Convert.ToDecimal(BetragTextBox.Text);
                    //Konto = Convert.ToDecimal(KontoTextBox.Text) + (Stand);
                    Konto = Konto + Convert.ToDecimal(BetragTextBox.Text);
                    AusgangLabel.Content = ($"{Stand}€");
                    KontoLabel.Content = ($"{Konto}€");
                    //KontoTextBox.Text = ($"{Konto}€");
                    RechnungsListBox.Items.Remove(RechnungsListBox.SelectedItem);
                }
            }
            catch
            {
                MessageBox.Show($"Fehler, keine Datei ausgewält!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void TSpeaker_Click(object sender, RoutedEventArgs e)
        {
            //Spracheausgabe mit SpeechSynthesizer
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Speak(textBlockAusgabe.Text.ToString());
        }

        private void ProgammEndButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }

}
