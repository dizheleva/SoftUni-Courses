namespace BookShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;
    public class AuthorBook
    {
        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [JsonIgnore]
        public Author Author { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }
    }
}
