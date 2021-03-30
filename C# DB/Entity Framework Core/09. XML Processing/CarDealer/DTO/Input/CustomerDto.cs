using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DTO.Input
{
    [XmlType("Customer")]
    public class CustomerDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("birthDate")]

        public DateTime BirthDate { get; set; }

        [XmlElement("isYoungerDriver")]

        public bool IsYoungerDriver { get; set; }
    }
}
