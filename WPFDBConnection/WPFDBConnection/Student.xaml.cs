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
        private readonly StudentCRUD studentCRUD = new();
        private Student? student;
        public Page1()
        {
            InitializeComponent();
            Load();
            
        }

        private void Load(string q = "")
        {
            try
            {
                var list = studentCRUD.GetAll(q);
                Grid.ItemsSource = list;
                //TxtCount.Text = 
            }
            catch (Exception ex) 
            {
                Error(ex);
            }
        }

      

        private void TxtSearch_Changed(object sender, TextChangedEventArgs e)
        {
            Load(TxtSearch.Text.Trim());
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            student = null;
            ClearForm();
            TxtNo.Focus();
            
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Grid.SelectedItem is not Student student_) return;

            student = student_;
            TxtNo.Text = student_.StudentNo;
            TxtFirst.Text = student_.FirstName;
            TxtLast.Text = student_.LastName;
            TxtEmail.Text = student_.Email;
            TxtPhone.Text = student_.Phone;
            TxtDob.Text = student_.DateOfBirth?.ToString("yyyy-MM-dd")?? "";
            TxtNo.Text = student_.StudentNo;
            SetCombo(CmbStatus, student_.Status);
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            student = null;
            ClearForm();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (student is null)
            {
                MessageBox.Show("Select a student first", "Delete",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var notification = MessageBox.Show(
                $"Delete {student.FirstName}?", "Confirm Deletion",
                MessageBoxButton.YesNo, MessageBoxImage.Warning
                );

            if (notification == MessageBoxResult.Yes) {
                try
                {
                    studentCRUD.Delete(student.Id);
                    ClearForm();
                    Load();
                }
                catch (Exception ex)
                {
                    Error(ex);
                }

            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //if(string.IsNullOrWhiteSpace(TxtNo.Text))

            DateTime? dob = null;
            if (!string.IsNullOrEmpty(TxtDob.Text))
            {
                if (!DateTime.TryParse(TxtDob.Text, out var date)) {
                    MessageBox.Show("Format date as yyyy-MM-dd");
                    return;
                }
                dob = date;
            }
            try
            {
                var stu = new Student
                {
                    Id = student?.Id ?? 0,
                    StudentNo = TxtNo.Text,
                    FirstName = TxtFirst.Text,
                    LastName = TxtLast.Text,
                    Email = TxtEmail.Text,
                    Phone = TxtPhone.Text,
                    DateOfBirth = dob,
                    Status = (CmbStatus.SelectedItem as
                    ComboBoxItem)?.Content?.ToString()?? "Active",
                };

                if (student is null)
                {
                    studentCRUD.Insert(stu);
                }
                else
                {
                    studentCRUD.Update(stu);
                }
                ClearForm();
                Load();
            }
            catch(Exception ex)
            {
                Error(ex);
            } 

        }

        private void ClearForm()
        {
            student = null;
            TxtFormTitle.Text = "Add Student";
            TxtNo.Text = TxtFirst.Text = TxtLast.Text
                = TxtEmail.Text = TxtPhone.Text = TxtDob.Text = "";
            CmbStatus.SelectedIndex = 0;
            Grid.SelectedItem = null;
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
