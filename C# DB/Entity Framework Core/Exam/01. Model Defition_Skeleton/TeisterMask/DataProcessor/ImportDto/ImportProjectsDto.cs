using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ImportProjectsDto
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public ImportTasksDto[] Tasks { get; set; }
    }
}
[XmlType("Task")]
public class ImportTasksDto
{
    [Required]
    [StringLength(40, MinimumLength = 2)]
    [XmlElement("Name")]
    public string Name { get; set; }

    [Required]
    [XmlElement("OpenDate")]
    public string OpenDate { get; set; }

    [Required]
    [XmlElement("DueDate")]
    public string DueDate { get; set; }

    [Required]
    [XmlElement("ExecutionType")]
    [EnumDataType(typeof(ExecutionType))]
    public string ExecutionType { get; set; }

    [Required]
    [XmlElement("LabelType")]
    [EnumDataType(typeof(LabelType))]
    public string LabelType { get; set; }
}

//•	Id - integer, Primary Key
//•	Name - text with length [2, 40] (required)
//•	OpenDate - date and time(required)
//•	DueDate - date and time(can be null)
//•	Tasks - collection of type Task

//< Name > Australian </ Name >
//< OpenDate > 19 / 08 / 2018 </ OpenDate >
//< DueDate > 13 / 07 / 2019 </ DueDate >
//< ExecutionType > 2 </ ExecutionType >
//< LabelType > 0 </ LabelType >
//Name - text with length[2, 40] (required)
//•	OpenDate - date and time(required)
//•	DueDate - date and time(required)
