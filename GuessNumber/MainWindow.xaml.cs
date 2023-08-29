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
using static GuessNumber.ServObj.ServiceNumber;

namespace GuessNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public int limitNum = 0, upperNum = 0, lowerNum = 1000, idNum = 0;
        public int hiddenNum = 0, treasure = 0, numOfBenefits = 0, stepCountNum = 1;
        public bool start_end = false, isPrime = false, isDivisibleByThree = false, flNumRecord = false;
        Random rdm = new Random();
        ServiceNumber rezultGames = new ServiceNumber();
        
        public MainWindow()
        {
            InitializeComponent();
            ListOfRecords.Content = "ID  Діапазон  Шукане  Спроб  Допомоги \n";
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

        private void DifficultyRecordTable_Click(object sender, RoutedEventArgs e)
        {
            rezultGames.SortNum(SortBy.limitNum);
            ListOfRecords.Content = "ID  Діапазон  Шукане  Спроб  Допомоги \n";
            ListOfRecords.Content += rezultGames.PrintAllRezultGame();
            flNumRecord = true;
        }

        private void HintsRecordsTable_Click(object sender, RoutedEventArgs e)
        {
            rezultGames.SortNum(SortBy.stepCountNum);
            ListOfRecords.Content = "ID  Діапазон  Шукане  Спроб  Допомоги \n";
            ListOfRecords.Content += rezultGames.PrintAllRezultGame();
            flNumRecord = true;
        }

        private void BiggerSmaller_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(PlayingField.Text) > hiddenNum) 
            {
                LabelBiggerSmaller.Content = "Меньше";
            }
            if (int.Parse(PlayingField.Text) < hiddenNum)
            {
                LabelBiggerSmaller.Content = "Більше";
            }
            if (int.Parse(PlayingField.Text) == hiddenNum)
            {
                LabelBiggerSmaller.Content = "Вгадал";
            }
            BiggerSmaller.IsEnabled = false;
            numOfBenefits++;
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
            numOfBenefits++;
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
            numOfBenefits++;
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
                hiddenNum = rdm.Next(0,limitNum);
                ModelNumber mn = new ModelNumber(hiddenNum);
                isPrime = mn.IsPrime(mn.HiddenNum);
                isDivisibleByThree = mn.IsDivisibleByThree();
                InformationTable.Content = "Вгадай число в диапазоне від 0 до " + limitNum.ToString() + ".";
                if (flNumRecord) ListOfRecords.Content = "ID  Діапазон  Шукане  Спроб  Допомоги \n";
                BeginToPlay.Content = "Здатися";
                FieldOfHistory.Text = "";
                RangeTen.IsEnabled = false;
                RangeHundred.IsEnabled = false;
                RangeThousand.IsEnabled = false;
                ClickAndGuess.IsEnabled = true;
                flNumRecord = false;
                stepCountNum = 1;
                numOfBenefits = 0;
            }
            else 
            {
                InformationTable.Content = "Ви здалися. Загадане число це => " + hiddenNum.ToString();
                //ListOfRecords.Content = "ID  Діапазон  Шукане  Спроб  Допомоги \n";
                BeginToPlay.Content = "Грати";
                BeginToPlay.IsEnabled = false;
                ClickAndGuess.IsEnabled = false;
                PlayingField.Text = "0";
                RangeTen.IsEnabled = true;
                RangeHundred.IsEnabled = true;
                RangeThousand.IsEnabled = true;
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
                FieldOfHistory.Text += $"{treasure} ПЕРЕМОГА. \n";
                BeginToPlay.Content = "Грати";
                BeginToPlay.IsEnabled = false;
                ClickAndGuess.IsEnabled = false;
                RangeTen.IsEnabled = true;
                RangeHundred.IsEnabled = true;
                RangeThousand.IsEnabled = true;
                BiggerSmaller.IsEnabled = false;
                AccidentallyCrop.IsEnabled = false;
                PrimeNumber.IsEnabled = false;
                MultipleOfThree.IsEnabled = false;
                start_end = false;
                idNum++;
                RangeNumber victory = new RangeNumber(idNum,hiddenNum,limitNum,stepCountNum,numOfBenefits);
                rezultGames.AddRezult(victory);
                ListOfRecords.Content += rezultGames.PrintOnRezultGame(victory);
                DifficultyRecordTable.IsEnabled = true;
                HintsRecordsTable.IsEnabled = true;
            }
            else 
            {
                InformationTable.Content = " НЕВДАЧА. Спробуй ще раз";
                FieldOfHistory.Text += $"{treasure} НЕВДАЧА. \n";
                stepCountNum++;
            }
            PlayingField.Text = "";
        }

        private void AccidentallyCrop_Click(object sender, RoutedEventArgs e)
        {
            upperNum = rdm.Next(hiddenNum+10, limitNum-1);
            lowerNum = rdm.Next(1, hiddenNum-10);
            AccidentallyCrop.IsEnabled = false;
            LabelAccidentallyCrop.Content = $"від {lowerNum.ToString()} до {upperNum.ToString()}";
            numOfBenefits++;
        }

    }
}
