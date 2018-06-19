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
        bool isPoint = false;
        bool isMinus = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Display.Text = value1;
            Digit0_Button.IsEnabled = false;
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
            
            if(Double.Parse(value1) > 0)
            {
                Digit0_Button.IsEnabled = true;
            }

            Display.Text = value1;
        }

        private void Backspace_Button_Click(object sender, RoutedEventArgs e)
        {
            if (value1.Length > 1)
            {
                value1 = value1.Remove(value1.Length - 1);
                if(!value1.Contains(","))
                {
                    isPoint = false;
                    Point_Button.IsEnabled = true;
                }
            }
            else
            {
                value1 = "0";
                Digit0_Button.IsEnabled = false;
            }
            Display.Text = value1;
        }

        private void Point_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!isPoint)
            {
                value1 += ",";
                isPoint = true;
                Point_Button.IsEnabled = false;
                Digit0_Button.IsEnabled = true;
                Display.Text = value1;
            }
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            isPoint = false;
            isMinus = false;
            Point_Button.IsEnabled = true;
            Digit0_Button.IsEnabled = false;
            value1 = "0";
            Display.Text = value1;
        }
    }
}
