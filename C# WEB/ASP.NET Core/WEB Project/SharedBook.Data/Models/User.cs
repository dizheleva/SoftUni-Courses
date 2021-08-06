using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedBook.Data.Models
{
    public class User
    {
        [Key]
        //[HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter a book title")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please, enter a book title")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please, enter a book title")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter a book title")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please, enter a book title")]
        public string UserType { get; set; } //regular/premium

        [Required(ErrorMessage = "Please, enter a book title")]
        public decimal Deposit { get; set; }

        public IEnumerable<Book> MyBooks { get; set; }

        public IEnumerable<Book> RentedBooks { get; set; }

        public IEnumerable<Book> WishList { get; set; }
    }
}
