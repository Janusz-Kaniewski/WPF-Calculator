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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string value1 = "0";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Display.Text = value1;
        }

        private void Digit_Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show((sender as Button).Content.ToString());

            string button_value = ((Button)sender).Content.ToString();

            if (Display.Text == "0")
            {
                value1 = button_value;
            }
            else
            {
                value1 += button_value;
            }
            Display.Text = value1;
        }

        private void Backspace_Button_Click(object sender, RoutedEventArgs e)
        {
            if (value1.Length > 1)
            {
                value1 = value1.Remove(value1.Length - 1);
            }
            else
            {
                value1 = "0";
            }
            Display.Text = value1;
        }
    }
}
