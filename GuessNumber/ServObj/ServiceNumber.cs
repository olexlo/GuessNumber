using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using GuessNumber.ModObj;

namespace GuessNumber.ServObj
{
    internal class ServiceNumber
    {
        private List<ModObj.RangeNumber> rezultGame;
        public enum SortBy { stepCountNum, limitNum }
        public ServiceNumber()
        {
            rezultGame = new List<ModObj.RangeNumber>();
        }
        // проверка, является ли число кратным трем
        public void AddRezult(ModObj.RangeNumber rezultat) // Добавлення результата игры
        {
            rezultGame.Add(rezultat);
        }
        public string PrintOnRezultGame(ModObj.RangeNumber rezultat) // Вивод результата игры
        {
            return $"{rezultat.ID} \t { rezultat.LimitNum} \t { rezultat.HiddenNum} \t { rezultat.StepСountNum} \t { rezultat.NumberOfClues} \n";
        }
        public string PrintAllRezultGame() // Вывод всех результатов игр
        {
            string result = "";
            if (rezultGame != null && rezultGame.Count > 0)
            {
                foreach (var rezultat in rezultGame)
                {
                    result += PrintOnRezultGame(rezultat);
                }
            }
            else
            {
                return "Список результатов игр пуст";
            }
            return result;
        }
        public void SortNum(SortBy sortBy)
        {
            if (rezultGame != null && rezultGame.Count > 0)
            {
                switch (sortBy)
                {
                    case SortBy.stepCountNum:
                        rezultGame.Sort((x, y) => y.StepСountNum.CompareTo(x.StepСountNum));
                        break;
                    case SortBy.limitNum:
                        rezultGame.Sort((x, y) => y.LimitNum.CompareTo(x.LimitNum));
                        break;
                }
            }
        }
    }
}
