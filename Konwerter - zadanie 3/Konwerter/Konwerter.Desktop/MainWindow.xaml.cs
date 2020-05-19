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
using Konwerter.Logic;

namespace Konwerter.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Konwerter.ItemsSource = new Konwertery().WybierzKonwerter();
            //Konwerter.DisplayMemberPath = "Name";
        }

        private void Konwerter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JednostkaZ.ItemsSource = ((IKonwerter)Konwerter.SelectedItem).Jednostki;
            JednostkaDo.ItemsSource = ((IKonwerter)Konwerter.SelectedItem).Jednostki;
            JednostkaZ.SelectedIndex = 0;
            JednostkaDo.SelectedIndex = 1;

            if (Konwerter.SelectedItem.GetType() == typeof(KonwerterZegara))
                Zegar.Visibility = Visibility.Visible;
            else
                Zegar.Visibility = Visibility.Hidden;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Konwertuj_Click(object sender, RoutedEventArgs e)
        {
            Wynik.Text = string.Empty;
            string Rezultat = ((IKonwerter)Konwerter.SelectedItem)
                     .Konwertuj(
                        (string)JednostkaZ.SelectedValue,
                        (string)JednostkaDo.SelectedValue,
                        Wartosc.Text);
            Wynik.Text = Rezultat;

            if (Konwerter.SelectedItem.GetType() == typeof(KonwerterZegara))
            {
                DateTime temp = DateTime.Parse(Rezultat);
                Godzinowka.RenderTransform = new RotateTransform((360 / 12) * temp.Hour);
                Minutowka.RenderTransform = new RotateTransform((360 / 60) * temp.Minute);
            }
        }
    }
}
