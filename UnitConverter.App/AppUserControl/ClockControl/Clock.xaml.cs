﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnitConverter.App.AppUserControl.ClockControl
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        public Brush color
        {
            get => (Brush)GetValue(brushProperty);
            set => SetValue(brushProperty, value);
        }

        public static readonly DependencyProperty brushProperty =
            DependencyProperty.Register("color", typeof(Brush), typeof(Clock), new PropertyMetadata(Brushes.Black));


        public DateTime time { get; private set; }

        public Clock()
        {
            InitializeComponent();
            time = DateTime.Parse("00:00");
        }

        
        /// <summary>
        /// Metoda ustawiająca wartość godziny na zegarze.
        /// Metoda ta również dostosowuje wskazówki zegara zgodznie z ustawioną godziną
        /// </summary>
        /// <param name="time"></param>
        public void setTime(string time)
        {
            this.time = DateTime.Parse(time);
            this.hourPointerRectangle.RenderTransform = new RotateTransform((this.time.Hour / 12.0) * 360);
            this.minutePointerRectangle.RenderTransform = new RotateTransform((this.time.Minute / 60.0) * 360);
        }

        
        /// <summary>
        /// Metoda, która pokazuje zegar
        /// </summary>
        public void show() => ((Storyboard)Resources["mainClockBorderShowStoryBoard"]).Begin();

        /// <summary>
        /// Metoda, która ukrywa zegar
        /// </summary>
        public void hide() => ((Storyboard)Resources["mainClockBorderHideStoryBoard"]).Begin();
    }
}
