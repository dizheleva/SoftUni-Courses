using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Dtos.Export
{
    [XmlType("SoldProducts")]
    public class SoldProductsContainer
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public SoldProductOutputModel[] Products { get; set; }

    }
}
