namespace BookShop.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class Book
    {
        public Book()
        {
            this.AuthorsBooks = new HashSet<AuthorBook>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
         public string Name { get; set; }

        [Required]
        public Genre Genre { get; set; }
        
        public decimal Price { get; set; }
        
        public int Pages { get; set; }

        [Required]
        public DateTime PublishedOn { get; set; }

        [JsonIgnore]
        public ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}