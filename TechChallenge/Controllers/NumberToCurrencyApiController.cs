namespace TechChallenge.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TechChallenge.Models;
    using TechChallenge.Services;

    public class NumberToCurrencyApiController : Controller
    {
        public INumberToCurrencyConverter numberToCurrencyConverter { get; set; }

        public NumberToCurrencyApiController(
            INumberToCurrencyConverter numberToCurrencyConverter)
        {
            this.numberToCurrencyConverter = numberToCurrencyConverter;
        }

        [HttpPost]
        public IActionResult Index(Person person)
        {
            if (ModelState.IsValid && person.Number.HasValue)
            {
                return View("~/Views/Home/Result.cshtml", new Person
                {
                    Name = person.Name,
                    Currency = numberToCurrencyConverter.Convert(person.Number.Value)
                });
            }
            else
            {
                return View("~/Views/Home/Index.cshtml", person);
            }
        }
    }
}