namespace TechChallenge.UnitTests.Services
{
    using TechChallenge.Services;
    using Xunit;

    public class NumberToCurrencyConverterTests
    {
        [Theory]
        [InlineData(1, "one dollar")]
        [InlineData(0.01, "one cent")]
        [InlineData(1.1, "one dollar and ten cents")]
        [InlineData(10.56, "ten dollars and fifty-six cents")]
        [InlineData(0, "")]
        [InlineData(-10, "minus ten dollars")]
        [InlineData(-10.5, "minus ten dollars and fifty cents")]
        [InlineData(100, "one hundred dollars")]
        [InlineData(134, "one hundred thirty-four dollars")]
        [InlineData(765.9, "seven hundred sixty-five dollars and ninety cents")]
        [InlineData(-765.9, "minus seven hundred sixty-five dollars and ninety cents")]
        [InlineData(100.5, "one hundred dollars and fifty cents")]
        [InlineData(1000, "one thousand dollars")]
        [InlineData(1680, "one thousand six hundred eighty dollars")]
        [InlineData(10000, "ten thousand dollars")]
        [InlineData(1000000, "one million dollars")]
        [InlineData(1000000000, "one billion dollars")]
        public void ShouldConvert(double number, string expectedWord)
        {
            var numberConverter = new NumberToCurrencyConverter();
            var word = numberConverter.Convert(number);

            Assert.Equal(expectedWord, word);
        }
    }
}
