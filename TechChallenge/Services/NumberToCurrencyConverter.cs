namespace TechChallenge.Services
{
    using System;

    public class NumberToCurrencyConverter : INumberToCurrencyConverter
    {
        public string Convert(double numberToConvert)
        {
            string beforeDecimalPointWord = string.Empty;
            var beforeDecimalPoint = Math.Sign(numberToConvert) < 0
                ? (int)Math.Ceiling(numberToConvert)
                : (int)Math.Floor(numberToConvert);
            if (beforeDecimalPoint != 0)
            {
                beforeDecimalPointWord = NumberToWords(beforeDecimalPoint);
                beforeDecimalPointWord += Math.Abs(beforeDecimalPoint) == 1 ?
                " dollar" : " dollars";
            }
            string afterDecimalPointWord = ConvertAfterDecimalPointNumber(numberToConvert, beforeDecimalPoint);

            if (string.IsNullOrWhiteSpace(afterDecimalPointWord))
            {
                return beforeDecimalPointWord;
            }

            if (string.IsNullOrWhiteSpace(beforeDecimalPointWord))
            {
                return afterDecimalPointWord;
            }

            return $"{beforeDecimalPointWord} and {afterDecimalPointWord}";
        }

        private string ConvertAfterDecimalPointNumber(double numberToConvert, int beforeDecimalPoint)
        {
            string afterDecimalPointWord = string.Empty;
            var afterDecimalPoint = numberToConvert > 1 ?
                (int)((numberToConvert - beforeDecimalPoint) * 100) :
                (int)(Math.Abs((numberToConvert - beforeDecimalPoint) * 100));
            if (afterDecimalPoint != 0)
            {
                afterDecimalPointWord = SmallNumberToWord(afterDecimalPoint, string.Empty, string.Empty);
                afterDecimalPointWord += Math.Abs(afterDecimalPoint) == 1 ?
                " cent" : " cents";
            }

            return afterDecimalPointWord;
        }

        private string NumberToWords(int number)
        {
            if (number == 0)
                return string.Empty;

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            var words = "";

            if (number / 1000000000 > 0)
            {
                words += NumberToWords(number / 1000000000) + " billion";
                number %= 1000000000;
            }

            if (number / 1000000 > 0)
            {
                words += NumberToWords(number / 1000000) + " million";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words += NumberToWords(number / 1000) + " thousand";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += NumberToWords(number / 100) + " hundred";
                number %= 100;
            }

            words = SmallNumberToWord(number, words, string.Empty);

            return words;
        }

        private string SmallNumberToWord(int number, string words, string suffix)
        {
            if (number <= 0) return words;
            //if (words != "")
            //    words += " ";

            if (number == 1) return "one";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
            {
                var unit = (Units)number;
                words += unit.ToString();
            }
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
            return words + suffix;
        }
    }

    public enum Units
    {
        zero = 0,
        one,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        eleven,
        twelve,
        thirteen,
        fourteen,
        fifteen,
        sixteen,
        seventeen,
        eighteen,
        nineteen
    };
}