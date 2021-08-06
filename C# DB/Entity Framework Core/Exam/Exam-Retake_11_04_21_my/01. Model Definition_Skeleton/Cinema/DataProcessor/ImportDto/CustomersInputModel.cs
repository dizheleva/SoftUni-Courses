using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class CustomersInputModel
    {
        [Required]
        [XmlElement("FirstName")]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [XmlElement("LastName")]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [XmlElement("Age")]
        [Range(12, 110)]
        public int Age { get; set; }

        [Required]
        [XmlElement("Balance")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        
        public List<TicketCustomerInputModel> Tickets { get; set; }
    }
}