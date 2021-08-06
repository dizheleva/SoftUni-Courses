using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Data.Models
{
    public class Projection
    {
        public Projection()
        {
            this.Tickets = new List<Ticket>();
        }

        public int Id { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
//•	Id – integer, Primary Key
//•	MovieId – integer, Foreign key (required)
//•	Movie – the Projection’s Movie 
//•	DateTime - DateTime (required)
//•	Tickets - collection of type Ticket
