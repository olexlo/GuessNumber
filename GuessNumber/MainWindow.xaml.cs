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
using GuessNumber.ModObj;
using GuessNumber.ServObj;

namespace GuessNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public int limitNum = 0, upperNum = 0, lowerNum = 1000;
        public int hiddenNum = 0, treasure = 0, numOfBenefits = 0;
        public bool start_end = false, isPrime = false, isDivisibleByThree = false;
        Random rdm = new Random();
        
        public MainWindow()
        {
            InitializeComponent();
            InformationTable.Content = " Виберіть рівень (діапазон) ";

        }

        private void RangeTen_Click(object sender, RoutedEventArgs e)
        {
            limitNum = 10;
            BeginToPlay.IsEnabled = true;
            PlayingField.MaxLength = 1;
        }

        private void RangeHundred_Click(object sender, RoutedEventArgs e)
        {
            limitNum = 100;
            BeginToPlay.IsEnabled = true;
            PlayingField.MaxLength = 2;
        }

        private void BiggerSmaller_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(PlayingField.Text) > hiddenNum) 
            {
                LabelBiggerSmaller.Content = "Більше";
            }
            if (int.Parse(PlayingField.Text) < hiddenNum)
            {
                LabelBiggerSmaller.Content = "Меньше";
            }
            if (int.Parse(PlayingField.Text) == hiddenNum)
            {
                LabelBiggerSmaller.Content = "Вгадал";
            }
            BiggerSmaller.IsEnabled = false;
        }

        private void MultipleOfThree_Click(object sender, RoutedEventArgs e)
        {
            if (isDivisibleByThree)
            {
                LabelMultipleOfThree.Content = "Число кратне трьом";
            }
            else
            {
                LabelMultipleOfThree.Content = "Число не кратне трьом";
            }
            MultipleOfThree.IsEnabled = false;
        }

        private void PrimeNumber_Click(object sender, RoutedEventArgs e)
        {
            if (isPrime)
            {
                LabelPrimeNumber.Content = "Це просте число";
            }
            else 
            {
                LabelPrimeNumber.Content = "Це складове число";
            }
            PrimeNumber.IsEnabled = false;
        }

        private void RangeThousand_Click(object sender, RoutedEventArgs e)
        {
            limitNum = 1000;
            BeginToPlay.IsEnabled = true;
            PlayingField.MaxLength = 3;
        }

        private void PlayingField_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BeginToPlay_Click(object sender, RoutedEventArgs e)
        {
            start_end = !start_end;
            if (start_end) 
            {
                hiddenNum = rdm.Next(limitNum);
                ModelNumber mn = new ModelNumber(hiddenNum);
                isPrime = mn.IsPrime(mn.HiddenNum);
                isDivisibleByThree = mn.IsDivisibleByThree();
                InformationTable.Content = "Гра почалася! Вгадай число в диапазоне від 0 до " + limitNum.ToString() + ".";
                RangeTen.IsEnabled = false;
                RangeHundred.IsEnabled = false;
                RangeThousand.IsEnabled = false;
                ClickAndGuess.IsEnabled = true;
            }
            else 
            {
                InformationTable.Content = "Ви здалися. Загадане число це => " + hiddenNum.ToString();
                PlayingField.Text = "0";
                RangeTen.IsEnabled = true;
                RangeHundred.IsEnabled = true;
                RangeThousand.IsEnabled = true;
                ClickAndGuess.IsEnabled = false;
                BeginToPlay.IsEnabled = false;
                BiggerSmaller.IsEnabled = false;
                AccidentallyCrop.IsEnabled = false;
                PrimeNumber.IsEnabled = false;
                MultipleOfThree.IsEnabled = false;
            }
            switch (limitNum)
            {
                case 10:
                    BiggerSmaller.IsEnabled = true;
                    break;
                case 100:
                    BiggerSmaller.IsEnabled = true;
                    AccidentallyCrop.IsEnabled = true;
                    break;
                case 1000:
                    BiggerSmaller.IsEnabled = true;
                    AccidentallyCrop.IsEnabled = true;
                    PrimeNumber.IsEnabled = true;
                    MultipleOfThree.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        private void ClickAndGuess_Click(object sender, RoutedEventArgs e)
        {
            treasure = int.Parse(PlayingField.Text);
            if (treasure == hiddenNum) 
            {
                InformationTable.Content = " ПЕРЕМОГА. Загадане число це => " + hiddenNum.ToString();
            }
            else 
            {
                InformationTable.Content = " НЕВДАЧА. Спробуй ще раз";
            }
            PlayingField.Text = "";
        }

        private void AccidentallyCrop_Click(object sender, RoutedEventArgs e)
        {
            upperNum = rdm.Next(hiddenNum+10, limitNum-1);
            lowerNum = rdm.Next(1, hiddenNum-10);
            AccidentallyCrop.IsEnabled = false;
            LabelAccidentallyCrop.Content = $"від {lowerNum.ToString()} до {upperNum.ToString()}";
        }

    }
}
