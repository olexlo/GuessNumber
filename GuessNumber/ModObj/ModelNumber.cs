using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumber.ModObj
{
    public class ModelNumber
    {
        public int HiddenNum { get; set; }
        
        public ModelNumber( int hiddenNum ) 
        {
            HiddenNum = hiddenNum;  
        }
        // проверка, является ли число кратным трем
        public bool IsDivisibleByThree()
        {
            return HiddenNum % 3 == 0;
        }
        // проверка, является ли число простым числом
        public bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            if (number == 2 || number == 3)
            {
                return true;
            }

            if (number % 2 == 0)
            {
                return false;
            }

            int limit = (int)Math.Sqrt(number);
            for (int i = 3; i <= limit; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
    public class RangeNumber : ModelNumber
    {
        public int ID { get; set; }
        public int LimitNum { get; set; }
        //public bool PrimeNum { get; set; }
        public int StepСountNum { get; set; }
        //public bool MultipleThreeNum { get; set; }
        public int NumberOfClues { get; set; }

        public RangeNumber(int id, int hiddenNum, int limitNum, int stepCountNum, int numberOfClues) : base(hiddenNum) 
        {
            ID = id;
            LimitNum = limitNum;
            StepСountNum = stepCountNum;
            NumberOfClues = numberOfClues;
        }
    }
}
