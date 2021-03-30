using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DTO.Output
{
    [XmlType("customer")]
    public class TotalSalesOutputModel
    {
        [XmlAttribute("full-name")]
        public string Name { get; set; }

        [XmlAttribute("bought-cars")]
        public int CarsCount { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
//customer full-name=&quot; Hai Everton&quot; bought - cars = &quot; 1 & quot; spent - money = &quot; 2544.67 & quot; / &gt;