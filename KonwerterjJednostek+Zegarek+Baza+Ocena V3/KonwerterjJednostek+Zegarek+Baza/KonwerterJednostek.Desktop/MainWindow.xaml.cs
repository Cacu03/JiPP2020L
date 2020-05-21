﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using KonwerterJednostek2.Logic;
using KonwerterJednostek;
using KonwerterJednostek2;







namespace KonwerterJednostek.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            combobox_Rodzaj_konwersji.ItemsSource = new KonwerterSerwis().GetKonwerters();
            combobox_Filtr_Rodzaj_konwersji.ItemsSource = new KonwerterSerwis().GetKonwerters();

            WyswietlDaneEF(DataGridStatystyka, 1);
            WyswietlDaneEF_TOP3(DataGridStatystyka_TOP3, 1);

            textbox_numer_strony.Text = "1";


            DatePickerDataOd.SelectedDate = new DateTime(2020, 1, 1);
            DatePickerDataDo.SelectedDate = new DateTime(2021, 1, 1);

            //############################################# odczyt wartości oceny z bazy danych
            RateControl.RateValue = 2;
            

        }



        public static void WyswietlDaneEF(DataGrid DataGridStatystyka, int Strona)
        {
            using (BazaDanychKonwerterEntities12 context = new BazaDanychKonwerterEntities12())
            {              
                List<TabelaKonwerter> TabelaKonwerters = context.TabelaKonwerter
                    .OrderBy(T => T.ID)
                    .Skip((Strona - 1) * 10)
                    .Take(10)
                    .ToList();
                DataGridStatystyka.DataContext = TabelaKonwerters;                
            }
        }

        public static void WyswietlDaneEF_TOP3(DataGrid DataGridStatystyka_TOP3, int Strona)
        {
            using (BazaDanychKonwerterEntities12 context = new BazaDanychKonwerterEntities12())
            {
                List<TabelaKonwerter> stats = context.TabelaKonwerter
                    .OrderBy(T => T.ID)
                    .Skip((Strona - 1) * 10)
                    .Take(10)
                    .ToList();
                DataGridStatystyka_TOP3.DataContext = stats;

                List<TabelaKonwerter> term = context.TabelaKonwerter
                    //  .Where(T => T.Data >= DataOd && T.Data < DataDo && T.RodzajKonwertera.StartsWith(RodzajKonwertersji))
                    .ToList();

                List<string> pop = term.Select(T => T.RodzajKonwertera).ToList();

                DataGridStatystyka_TOP3.ItemsSource = term.GroupBy(l => new { l.RodzajKonwertera, l.Jednostka_IN, l.Jednostka_OUT })
                    .Select(g => new { g.Key.RodzajKonwertera, g.Key.Jednostka_IN, g.Key.Jednostka_OUT, count = g.Count() })
                    .OrderByDescending(g => g.count)
                    .ToList()
                    .Take(3);
            }
        }



        public static void WyswietlDaneEF_Filtr(DataGrid DataGridStatystyka, int Strona, string RodzajKonwertersji, DateTime DataOd, DateTime DataDo)
        {
            using (BazaDanychKonwerterEntities12 context = new BazaDanychKonwerterEntities12())
            {
                List<TabelaKonwerter> TabelaKonwerters = context.TabelaKonwerter
                    .Where(T => T.Data >= DataOd && T.Data < DataDo && T.RodzajKonwertera.StartsWith(RodzajKonwertersji))
                    .OrderBy(T => T.ID)
                    .Skip((Strona - 1) * 10)
                    .Take(10)
                    .ToList();
                DataGridStatystyka.DataContext = TabelaKonwerters;
            }
        }

        public static void WyswietlDaneEF_Filtr_TOP3(DataGrid DataGridStatystyka_TOP3, int Strona, string RodzajKonwertersji, DateTime DataOd, DateTime DataDo)
        {
            using (BazaDanychKonwerterEntities12 context = new BazaDanychKonwerterEntities12())
            {
                List<TabelaKonwerter> stats = context.TabelaKonwerter
                    .Where(T => T.Data >= DataOd && T.Data < DataDo && T.RodzajKonwertera.StartsWith(RodzajKonwertersji))
                    .OrderBy(T => T.ID)
                    .Skip((Strona - 1) * 10)
                    .Take(10)
                    .ToList();
                DataGridStatystyka_TOP3.DataContext = stats;

                List<TabelaKonwerter> term = context.TabelaKonwerter
                    .Where(T => T.Data >= DataOd && T.Data < DataDo && T.RodzajKonwertera.StartsWith(RodzajKonwertersji))
                    .ToList();

                List<string> pop = term.Select(T => T.RodzajKonwertera).ToList();

                DataGridStatystyka_TOP3.ItemsSource = term.GroupBy(l => new { l.RodzajKonwertera, l.Jednostka_IN, l.Jednostka_OUT })
                    .Select(g => new { g.Key.RodzajKonwertera, g.Key.Jednostka_IN, g.Key.Jednostka_OUT, count = g.Count() })
                    .OrderByDescending(g => g.count)
                    .ToList()
                    .Take(3);
            }
        }


        private void combobox_Rodzaj_konwersji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combobox_Jednostka_bazowa.ItemsSource = ((IKonwerter)combobox_Rodzaj_konwersji.SelectedItem).Units;
            combobox_Jednostka_wynikowa.ItemsSource = ((IKonwerter)combobox_Rodzaj_konwersji.SelectedItem).Units;


            if (combobox_Rodzaj_konwersji.SelectedItem.ToString() == "KonwerterjJednostek2.Format_12_24_class") { uwaga_format.Visibility = Visibility.Visible; }
            if (combobox_Rodzaj_konwersji.SelectedItem.ToString() != "KonwerterjJednostek2.Format_12_24_class") { uwaga_format.Visibility = Visibility.Hidden; }
        }

        private void button_action_Click(object sender, RoutedEventArgs e)
        {
            //((Storyboard) Resources["KoloStory2"]) to nie dziala
            


            string tekst_wejsciowy = textbox_input.Text;
            string costam = combobox_Rodzaj_konwersji.SelectedItem.ToString();

            if (combobox_Rodzaj_konwersji.SelectedItem.ToString() != "KonwerterjJednostek2.Format_12_24_class")
            {
                double wartosc_wejsciowa = double.Parse(tekst_wejsciowy);

                double wynik = 0;

                wynik = ((IKonwerter)combobox_Rodzaj_konwersji.SelectedItem).Konwerter(combobox_Jednostka_bazowa.SelectedItem.ToString(), combobox_Jednostka_wynikowa.SelectedItem.ToString(), wartosc_wejsciowa);
                textbox_wynik.Text = wynik.ToString();
            }


            if (combobox_Rodzaj_konwersji.SelectedItem.ToString() == "KonwerterjJednostek2.Format_12_24_class")
            {
                string wrocony_wynik;

                Format_12_24_class F1 = new Format_12_24_class();
                F1.Konwerter2(tekst_wejsciowy);


                wrocony_wynik = F1.Konwerter2(tekst_wejsciowy);
                textbox_wynik.Text = wrocony_wynik;

                double godzina_kat;
                double minuta_kat;
                godzina_kat = double.Parse(wrocony_wynik.Remove(2));



                string minuta_kat_tmp = wrocony_wynik.Remove(0, 3);


                minuta_kat = double.Parse(minuta_kat_tmp.Remove(2));

                godzinowa_rotacja.Angle = 0;
                minutowa_rotacja.Angle = 0;

                godzinowa_rotacja.Angle = ((godzina_kat+3)*30);
                minutowa_rotacja.Angle = (minuta_kat + 15) * 6;                
            }

            WtyczkaBazaDanych W2 = new WtyczkaBazaDanych();
            W2.WstawDaneEF(combobox_Rodzaj_konwersji.SelectedItem.ToString(), combobox_Jednostka_bazowa.SelectedItem.ToString(), combobox_Jednostka_wynikowa.SelectedItem.ToString(), double.Parse(textbox_input.Text), double.Parse(textbox_wynik.Text));

        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            int strona;
            strona = int.Parse(textbox_numer_strony.Text);
            
            strona = strona+1;
            textbox_numer_strony.Text = strona.ToString();
            WyswietlDaneEF(DataGridStatystyka, strona);
        }

        private void button_previous_Click(object sender, RoutedEventArgs e)
        {
            int strona;
            strona = int.Parse(textbox_numer_strony.Text);
            if (strona > 1)
            {
                strona = strona - 1;
                textbox_numer_strony.Text = strona.ToString();
                WyswietlDaneEF(DataGridStatystyka, strona);
            }
        }

        private void button_Filtruj_Click(object sender, RoutedEventArgs e)
        {
            
            WyswietlDaneEF_Filtr(DataGridStatystyka, 1, combobox_Filtr_Rodzaj_konwersji.SelectedItem.ToString(), (DateTime)DatePickerDataOd.SelectedDate, (DateTime)DatePickerDataDo.SelectedDate);
            WyswietlDaneEF_Filtr_TOP3(DataGridStatystyka, 1, combobox_Filtr_Rodzaj_konwersji.SelectedItem.ToString(), (DateTime)DatePickerDataOd.SelectedDate, (DateTime)DatePickerDataDo.SelectedDate);
        }

        private void button_Odfiltruj_Click(object sender, RoutedEventArgs e)
        {
            WyswietlDaneEF(DataGridStatystyka, 1);
            WyswietlDaneEF_TOP3(DataGridStatystyka_TOP3, 1);
        }

        private void combobox_Filtr_Rodzaj_konwersji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
 