using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDBConnection
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
       
        public Page1()
        {
            InitializeComponent();
            
        }

      

        private void TxtSearch_Changed(object sender, TextChangedEventArgs e)
        {
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            ClearForm();
            
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            
            ClearForm();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ClearForm()
        {
            
        }
        private static void SetCombo(ComboBox cmb, string value)
        {
            foreach (ComboBoxItem item in cmb.Items)
                if (item.Content?.ToString() == value) { cmb.SelectedItem = item; return; }
            cmb.SelectedIndex = 0;
        }

        private static void Error(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
