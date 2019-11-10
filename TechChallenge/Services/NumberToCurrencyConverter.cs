namespace TechChallenge.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TechChallenge.Enums;

    public class NumberToCurrencyConverter : INumberToCurrencyConverter
    {
        public string Convert(double numberToConvert)
        {
            var beforeDecimalPointWord = new List<string>();
            var beforeDecimalPoint = Math.Sign(numberToConvert) < 0
                ? (int)Math.Ceiling(numberToConvert)
                : (int)Math.Floor(numberToConvert);
            if (beforeDecimalPoint != 0)
            {
                beforeDecimalPointWord.AddRange(NumberToWords(beforeDecimalPoint));
                beforeDecimalPointWord.Add(Math.Abs(beforeDecimalPoint) == 1 ?
                "dollar" : "dollars");
            }

            var afterDecimalPointWord = ConvertAfterDecimalPointNumber(numberToConvert, beforeDecimalPoint);

            if (afterDecimalPointWord == null || !afterDecimalPointWord.Any())
            {
                return string.Join(" ", beforeDecimalPointWord);
            }

            if (beforeDecimalPointWord == null || !beforeDecimalPointWord.Any())
            {
                return string.Join(" ", afterDecimalPointWord);
            }

            return $"{string.Join(" ", beforeDecimalPointWord)} and {string.Join(" ", afterDecimalPointWord)}";
        }

        private List<string> ConvertAfterDecimalPointNumber(double numberToConvert, int beforeDecimalPoint)
        {
            var afterDecimalPointWord = new List<string>();
            var afterDecimalPoint = numberToConvert > 1 ?
                (int)(Math.Round(numberToConvert - beforeDecimalPoint, 2) * 100) :
                (int)(Math.Abs(Math.Round(numberToConvert - beforeDecimalPoint, 2) * 100));
            if (afterDecimalPoint != 0)
            {
                SmallNumberToWord(afterDecimalPoint, afterDecimalPointWord);
                afterDecimalPointWord.Add(Math.Abs(afterDecimalPoint) == 1 ?
                "cent" : "cents");
            }

            return afterDecimalPointWord;
        }

        private List<string> NumberToWords(int number)
        {
            var wordsList = new List<string>();

            if (number == 0)
            {
                wordsList.Add(string.Empty);
                return wordsList;
            }

            if (number < 0)
            {
                wordsList.Add("minus");
                wordsList.AddRange(NumberToWords(Math.Abs(number)));
                return wordsList;
            }

            if (number / 1000000000 > 0)
            {
                wordsList.AddRange(NumberToWords(number / 1000000000));
                wordsList.Add("billion");
                number %= 1000000000;
            }

            if (number / 1000000 > 0)
            {
                wordsList.AddRange(NumberToWords(number / 1000000));
                wordsList.Add("million");
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                wordsList.AddRange(NumberToWords(number / 1000));
                wordsList.Add("thousand");
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                wordsList.AddRange(NumberToWords(number / 100));
                wordsList.Add("hundred");
                number %= 100;
            }

            SmallNumberToWord(number, wordsList);

            return wordsList;
        }

        private List<string> SmallNumberToWord(int number, List<string> words)
        {
            if (number <= 0) return words;

            if (number < 20)
            {
                words.Add(Enum.GetName(typeof(Units), number));
            }
            else
            {
                var unitTensWords = string.Empty;
                unitTensWords += Enum.GetName(typeof(Tens), number / 10);
                if ((number % 10) > 0)
                {
                    var unit = Enum.GetName(typeof(Units), number % 10);
                    unitTensWords += "-" + unit.ToString();
                }
                words.Add(unitTensWords);
            }
            return words;
        }
    }
}