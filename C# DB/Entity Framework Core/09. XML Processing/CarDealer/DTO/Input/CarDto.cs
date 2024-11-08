﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DTO.Input
{
    [XmlType("Car")]
    public class CarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TravellingDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public CarPartsDto[] PartsId { get; set; }
    }
    
}
