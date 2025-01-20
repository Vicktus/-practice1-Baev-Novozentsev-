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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InputTextBox1.ToolTip = "Введите значение X";
            InputTextBox2.ToolTip = "Введите значение Y";
            ResultTextBox.ToolTip = "Результат расчёта";
            CalculateButton.ToolTip = "Рассчитать";
            ClearButton.ToolTip = "Очистить";

            this.Closing += MainWindow_Closing;
        }

        private double Function(double x)
        {
            if (FunctionRadioButton1.IsChecked == true) // sh(x)
            {
                return Math.Sinh(x);
            }
            else if (FunctionRadioButton2.IsChecked == true) // x^2
            {
                return x * x;
            }
            else if (FunctionRadioButton3.IsChecked == true) // e^x
            {
                return Math.Exp(x);
            }
            else
            {
                throw new Exception("Не выбрана функция f(x).");
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = Convert.ToDouble(InputTextBox1.Text);
                double y = Convert.ToDouble(InputTextBox2.Text);

                double fx = Function(x);

                double result = 0;

                // 1
                result += Math.Log(fx) + Math.Pow(fx * fx + y, 3);

                // 2 
                if (x * y > 0)
                {
                    result += Math.Log(Math.Abs(fx / y));
                }

                // 3 
                if (x * y < 0)
                {
                    result += Math.Pow(fx + y, 3) * Math.Pow(fx * fx + y, 3);
                }

                ResultTextBox.Text = result.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка ввода. Проверьте значения X, Y и выбор функции.");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox1.Text = "";
            InputTextBox2.Text = "";
            ResultTextBox.Text = "";
            FunctionRadioButton1.IsChecked = false;
            FunctionRadioButton2.IsChecked = false;
            FunctionRadioButton3.IsChecked = false;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти из приложения?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
