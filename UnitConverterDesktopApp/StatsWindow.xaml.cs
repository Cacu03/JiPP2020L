﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using unit_converter;

namespace UnitConverterDesktopApp
{
    public partial class StatsWindow : Window
    {
        private IEnumerable<dynamic> _records;
        private static readonly int _maxRecordsPerPage = 10;
        private int _currentPage;
        private double _lastPage;

        public StatsWindow()
        {
            InitializeComponent();
            FilterByConverterListBox.ItemsSource = new ConverterService().GetConverters().Keys;
            _currentPage = 1;
        }
        public void PageCountRefresh()
        {
            PageCountTextBox.Text = $"Page {_currentPage} of {_lastPage}";
        }
        private List<string> GetFilterByConverterItems()
        {
            List<string> nameFilters = new List<string>();
            if (FilterByConverterListBox.SelectedItems.Count == 0)
            {
                foreach (string s in FilterByConverterListBox.Items)
                {
                    nameFilters.Add(s);
                }
            }
            else
            {
                foreach (string s in FilterByConverterListBox.SelectedItems)
                {
                    nameFilters.Add(s);
                }
            }
            return nameFilters;
        }
        private void RunQuery()
        {
            this._records = Database.SelectResults(
                this.GetFilterByConverterItems(), this.DateFromPicker.SelectedDate, this.DateToPicker.SelectedDate, this.TopCheckBox.IsChecked);
            _lastPage = Math.Ceiling((Enumerable.Count(this._records) / Convert.ToDouble(_maxRecordsPerPage)));
        }
        private void FilterDataButton_Click(object sender, RoutedEventArgs e)
        {
            RunQuery();
            _currentPage = 1;
            TableForStats.ItemsSource = this._records
                .Skip((_currentPage - 1) * _maxRecordsPerPage)
                .Take(_maxRecordsPerPage);
            PageCountRefresh();
        }
        private void TableForStats_Loaded(object sender, RoutedEventArgs e)
        {
            RunQuery();
            TableForStats.ItemsSource = this._records.Take(_maxRecordsPerPage);
            PageCountRefresh();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage - 1 > 0)
            {
                _currentPage--;
                TableForStats.ItemsSource = this._records
                    .Skip((_currentPage - 1) * _maxRecordsPerPage)
                    .Take(_maxRecordsPerPage);
                PageCountRefresh();
            }
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {        
            if (_currentPage < _lastPage)
            {
                _currentPage++;
                TableForStats.ItemsSource = this._records
                    .Skip((_currentPage - 1) * _maxRecordsPerPage)
                    .Take(_maxRecordsPerPage);
                PageCountRefresh();
            }
        }
    }
}
