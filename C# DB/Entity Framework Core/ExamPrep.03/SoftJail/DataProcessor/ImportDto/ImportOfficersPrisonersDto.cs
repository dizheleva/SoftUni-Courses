using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("OfficerPrisoner")]
    public class ImportOfficersPrisonersDto
    {
        [XmlElement("Name")]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [XmlElement("Money")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Money { get; set; }

        [XmlElement("Position")]
        [EnumDataType(typeof(Position))]
        public string Position { get; set; }

        [XmlElement("Weapon")]
        [EnumDataType(typeof(Weapon))]
        public string Weapon { get; set; }

        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public PrisonerInputModel[] Prisoners { get; set; }

    }
}
[XmlType("Prisoner")]
public class PrisonerInputModel
{
    [XmlAttribute("id")]
    public int Id { get; set; }
}
//< Officers >
//< Officer >
//< Name > Minerva Kitchingman </ Name >

//< Money > 2582 </ Money >

//< Position > Invalid </ Position >

//< Weapon > ChainRifle </ Weapon >

//< DepartmentId > 2 </ DepartmentId >

//< Prisoners >

//< Prisoner id = "15" />

//</ Prisoners >