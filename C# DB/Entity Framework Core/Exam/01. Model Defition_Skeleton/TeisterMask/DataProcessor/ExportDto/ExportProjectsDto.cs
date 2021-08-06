﻿using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ExportProjectsDto
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public TaskDto[] Tasks { get; set; }
    }
    [XmlType("Task")]
    public class TaskDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Label")]
        public string Label { get; set; }
    }
}
//< Project TasksCount = "10" >
//< ProjectName > Hyster - Yale </ ProjectName >
//< HasEndDate > No </ HasEndDate >
//< Tasks >
//< Task >
//< Name > Broadleaf </ Name >
//< Label > JavaAdvanced </ Label >
//</ Task >
