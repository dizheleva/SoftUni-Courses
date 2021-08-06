using System.ComponentModel.DataAnnotations;
using SharedBook.Data.Models.Enums;

namespace SharedBook.Data.Models
{
    public class Book
    {
        [Key]
        //[HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter a book title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please, enter a book author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please, enter a book genre")]
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Please, enter a book condition")]
        public string Condition { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please, enter a book description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please, enter a book type")]
        public Type Type { get; set; }

        [Required(ErrorMessage = "Please, enter a book price")]
        [Range(0.1, 9999.99, ErrorMessage = "Please, enter a positive valid price")]
        public decimal Price { get; set; }
    }
}
