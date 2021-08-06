using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;
using Cinema.Data.Models;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]
    public class ImportProjectionsDto
    {
        [Required]
        [XmlElement("MovieId")]
        public int MovieId { get; set; }
        
        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }
    }
}
//•	Id – integer, Primary Key
//•	MovieId – integer, Foreign key (required)
//•	Movie – the Projection’s Movie 
//•	DateTime - DateTime (required)
//•	Tickets - collection of type Ticket