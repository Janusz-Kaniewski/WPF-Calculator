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
        string value2 = "0";
        double number1;
        double number2;
        double result;
        int counter = 1;
        bool isPoint = false;
        bool isMinus = false;
        int operationID;
        int stateID;
        string operation_sign = "";

        enum State
        {
            NONE,       //0
            ENTERING_A, //1
            ENTERING_B  //2
        }

        enum Operation
        {
            NONE,   //0
            ADD,    //1
            SUB,    //2
            MUL,    //3
            DIV,    //4
            POW     //5
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        //method below will be activated first - when window is loaded and displayed
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            operationID = (int)Operation.NONE;
            stateID = (int)State.ENTERING_A;
            refreshDisplay(stateID);
            Digit0_Button.IsEnabled = false;

            Help_Button.IsEnabled = false;
            Save_result_Button.IsEnabled = false;
            Delete_result_Button.IsEnabled = false;
        }

        private int set_operationID(string bv)
        {
            if (bv.Equals("+")) return (int)Operation.ADD;
            else if (bv.Equals("-")) return (int)Operation.SUB;
            else if (bv.Equals("*")) return (int)Operation.MUL;
            else if (bv.Equals("/")) return (int)Operation.DIV;
            else if (bv.Equals("^")) return (int)Operation.POW;
            else return (int)Operation.NONE;
        }

        private void refreshDisplay(int state)
        {
            if(state == (int)State.ENTERING_A)
            {
                Display.Text = value1;
            }
            else if(state == (int)State.ENTERING_B)
            {
                Display.Text = value1 + "\n" + operation_sign + "\n" + value2;
            }
        }

        private void Digit_Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show((sender as Button).Content.ToString());

            string button_value = ((Button)sender).Content.ToString();

            if (counter < 12)
            {
                if (stateID == (int)State.ENTERING_A)
                {
                    if (Display.Text == "0")
                    {
                        value1 = button_value;
                    }
                    else
                    {
                        value1 += button_value;
                        counter++;
                    }

                    if (Double.Parse(value1) > 0)
                    {
                        Digit0_Button.IsEnabled = true;
                    }
                }
                else if(stateID == (int)State.ENTERING_B)
                {
                    if (value2 == "0")
                    {
                        value2 = button_value;
                    }
                    else
                    {
                        value2 += button_value;
                        counter++;
                    }

                    if (Double.Parse(value2) > 0)
                    {
                        Digit0_Button.IsEnabled = true;
                    }
                }
                refreshDisplay(stateID);
            }

            if (counter == 12)
            {
                Point_Button.IsEnabled = false;
            }
        }

        private void Backspace_Button_Click(object sender, RoutedEventArgs e)
        {
            int numbers_left;

            if(isMinus)
            {
                numbers_left = 2;
            }
            else
            {
                numbers_left = 1;
            }

            if (value1.Length > numbers_left)
            {
                value1 = value1.Remove(value1.Length - 1);
                counter--;
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
                isMinus = false;
            }
            Display.Text = value1;
        }

        private void Point_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!isPoint)
            {
                value1 += ",";
                counter++;
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
            counter = 1;
            Point_Button.IsEnabled = true;
            Digit0_Button.IsEnabled = false;
            value1 = "0";
            value2 = "0";
            stateID = (int)State.ENTERING_A;
            operationID = (int)Operation.NONE;
            refreshDisplay(stateID);
        }

        private void Negate_Button_Click(object sender, RoutedEventArgs e)
        {
            if (stateID == (int)State.ENTERING_A)
            {
                if (Double.Parse(value1) != 0)
                {
                    if (!isMinus)
                    {
                        value1 = "-" + value1;
                        isMinus = true;
                    }
                    else
                    {
                        value1 = value1.Remove(0, 1);
                        isMinus = false;
                    }
                }
            }
            else if (stateID == (int)State.ENTERING_B)
            {
                if (Double.Parse(value2) != 0)
                {
                    if (!isMinus)
                    {
                        value2 = "-" + value2;
                        isMinus = true;
                    }
                    else
                    {
                        value2 = value2.Remove(0, 1);
                        isMinus = false;
                    }
                }
            }
            refreshDisplay(stateID);
        }

        private void Operation_Button_Click(object sender, RoutedEventArgs e)
        {
            string button_value = ((Button)sender).Content.ToString();
            operation_sign = button_value;

            operationID = set_operationID(button_value);

            if (stateID == (int)State.ENTERING_A)
            {
                number1 = Double.Parse(value1);
                value1 = number1.ToString();
                refreshDisplay(stateID);
                stateID = (int)State.ENTERING_B;
                counter = 1;
                isMinus = false;
                isPoint = false;
            }
            else if (stateID == (int)State.ENTERING_B)
            {

            }
        }
    }
}