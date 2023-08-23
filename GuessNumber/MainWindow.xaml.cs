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

namespace GuessNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int limitNum = 0, upperNum = 0, lowerNum = 1000;
        public int hiddenNum = 0;
        public bool start_end = false, flagEnd = false;
        Random rdm = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RangeTen_Click(object sender, RoutedEventArgs e)
        {
            limitNum = 10;
        }

        private void RangeHundred_Click(object sender, RoutedEventArgs e)
        {
            limitNum = 100;
        }

        private void RangeThousand_Click(object sender, RoutedEventArgs e)
        {
            limitNum = 1000;
        }

        private void BeginToPlay_Click(object sender, RoutedEventArgs e)
        {
            start_end = !start_end;
            if (start_end) 
            {
                hiddenNum = rdm.Next(limitNum);
                InformationTable.Content = "Гра почалася! Вгадай число в диапазоне від 0 до " + limitNum.ToString() + ".";
            }
            else 
            {
                InformationTable.Content = "Ви здалися. Загадане число це => " + hiddenNum.ToString();
            }
        }

        private void ClickAndGuess_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AccidentallyCrop_Click(object sender, RoutedEventArgs e)
        {
            upperNum = rdm.Next(hiddenNum, limitNum+1);
            lowerNum = rdm.Next(0, limitNum-1);
        }

    }
}
