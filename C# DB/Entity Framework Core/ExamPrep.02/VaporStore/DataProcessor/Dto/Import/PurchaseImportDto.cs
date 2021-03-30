using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class PurchaseImportDto
    {
        [XmlElement("Type")]
        [Required]
        public string Type { get; set; }

        [XmlElement("Key")]
        [Required]
        [RegularExpression(@"^([A-Z\d]{4})\-([A-Z\d]{4})\-([A-Z\d]{4})$")]
        public string ProductKey { get; set; }

        [XmlElement("Date")]
        [Required]
        public string Date { get; set; }

        [XmlElement("Card")]
        [Required]
        public string CardNumber { get; set; }

        [Required]
        [XmlAttribute("title")]
        public string GameName { get; set; }
    }
}
