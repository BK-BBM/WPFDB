using System.Windows;
using System.Windows.Controls;

namespace WPFDBConnection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Nav_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button b) {
                Navigate(b.Tag?.ToString()?? "Students");
            }
        }

        private void Navigate(string page)
        {
            Page? view = page switch
            {
                "Students" => new Page1(), //don't be like me, name your pages when you add them
                "Modules" => new Modules() //student activity??
,
                _ => null // this is the switch default case
            };
            if (view != null) ContentFrame.Navigate(view);
        }
    }
}