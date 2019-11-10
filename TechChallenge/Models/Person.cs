namespace TechChallenge.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Person
    {
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Name cannot be longer than 40 characters and less than 4 characters")]
        [Required(ErrorMessage = "This name field needs a value!")]
        public string Name { get; set; }

        [Range(-999999999, 999999999999.99)]
        [Required(ErrorMessage = "This number field needs a value!")]
        public double? Number { get; set; }

        public string Currency { get; set; }
    }
}
