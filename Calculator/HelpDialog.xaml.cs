using System;
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
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for HelpDialog.xaml
    /// </summary>
    public partial class HelpDialog : Window
    {
        public HelpDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            author.Content = "Calculator v0.11 - Janusz Kaniewski 2018";
            licence.Content = "This software is under MIT licence.";
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
