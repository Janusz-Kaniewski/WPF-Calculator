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

        private double calculate()
        {
            number2 = Double.Parse(value2);

            switch (operationID)
            {
                case (int)Operation.NONE:
                    return 0;

                case (int)Operation.ADD:
                    return number1 + number2;

                case (int)Operation.SUB:
                    return number1 - number2;

                case (int)Operation.MUL:
                    return number1 * number2;

                case (int)Operation.DIV:
                    return number1 / number2;

                case (int)Operation.POW:
                    return Math.Pow(number1, number2);

                default:
                    return 0;
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

                    if (Double.Parse(value2) == 0 && operationID == (int)Operation.DIV)
                    {
                        Equal_Button.IsEnabled = false;
                    }
                    else
                    {
                        Equal_Button.IsEnabled = true;
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

            if (stateID == (int)State.ENTERING_A)
            {
                if (value1.Length > numbers_left)
                {
                    value1 = value1.Remove(value1.Length - 1);
                    counter--;
                    if (!value1.Contains(","))
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
            }
            else if (stateID == (int)State.ENTERING_B)
            {
                if (value2.Length > numbers_left)
                {
                    value2 = value2.Remove(value2.Length - 1);
                    counter--;
                    if (!value2.Contains(","))
                    {
                        isPoint = false;
                        Point_Button.IsEnabled = true;
                    }
                }
                else
                {
                    value2 = "0";
                    Digit0_Button.IsEnabled = false;
                    isMinus = false;
                }

                if (Double.Parse(value2) == 0 && operationID == (int)Operation.DIV)
                {
                    Equal_Button.IsEnabled = false;
                }
                else
                {
                    Equal_Button.IsEnabled = true;
                }
            }
            refreshDisplay(stateID);
        }

        private void Point_Button_Click(object sender, RoutedEventArgs e)
        {
            if(stateID == (int)State.ENTERING_A && !isPoint)
            {
                value1 += ",";
                counter++;
                isPoint = true;
                Point_Button.IsEnabled = false;
                Digit0_Button.IsEnabled = true;
            }
            else if (stateID == (int)State.ENTERING_B && !isPoint)
            {
                value2 += ",";
                counter++;
                isPoint = true;
                Point_Button.IsEnabled = false;
                Digit0_Button.IsEnabled = true;
            }
            refreshDisplay(stateID);
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
            operationID = (int)Operation.NONE;
            stateID = (int)State.ENTERING_A;
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
            Point_Button.IsEnabled = true;

            if (stateID == (int)State.ENTERING_A)
            {
                number1 = Double.Parse(value1);
                value1 = number1.ToString();
                stateID = (int)State.ENTERING_B;
                refreshDisplay(stateID);
                counter = 1;
                isMinus = false;
                isPoint = false;
            }
            else if (stateID == (int)State.ENTERING_B)
            {
                result = calculate();
                value1 = result.ToString();
                value2 = "0";
                number1 = result;
                number2 = 0;
                refreshDisplay(stateID);
            }

            operationID = set_operationID(button_value);

            if (operationID == (int)Operation.DIV)
            {
                Equal_Button.IsEnabled = false;
            }

            Digit0_Button.IsEnabled = false;
        }

        private void Equal_Button_Click(object sender, RoutedEventArgs e)
        {
            if (operationID != (int)Operation.NONE)
            {
                result = calculate();
                value1 = result.ToString();
                value2 = "0";
                number1 = 0;
                number2 = 0;
                counter = value1.Length;
                operationID = (int)Operation.NONE;
                stateID = (int)State.ENTERING_A;
                refreshDisplay(stateID);

                if(result==0)
                {
                    Digit0_Button.IsEnabled = false;
                }
                else
                {
                    Digit0_Button.IsEnabled = true;
                }
            }
        }

        private void Help_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BLA BLA BLA", "Help");
        }
    }
}