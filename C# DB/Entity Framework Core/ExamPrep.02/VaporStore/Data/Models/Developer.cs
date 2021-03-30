namespace VaporStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class Developer
    {
        public Developer()
        {
            this.Games = new List<Game>();
        }

        [Key]
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
//•	Id – integer, Primary Key
//•	Name – text (required)
//•	Games - collection of type Game
