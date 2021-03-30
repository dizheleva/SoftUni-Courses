using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using VaporStore.DataProcessor.Dto.Import;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("User")]
    public class UserDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray("Purchases")]
        public PurchaseDto[] Purchases { get; set; }

        [XmlElement]
        public decimal TotalSpent { get; set; }
    }
}
