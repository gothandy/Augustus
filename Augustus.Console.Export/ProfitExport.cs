using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Augustus.Domain.Enums;
using Augustus.Domain.Objects;

namespace Augustus.Console.Export
{
    [XmlRoot("Profit")]
    public class ProfitExport : List<MonthExport>
    {

    }

    [XmlRoot("Month")]
    public class MonthExport
    {
        public MonthExport()
        {
        }

        [XmlElement("Account")]
        public string Account { get; set; }

        [XmlElement("Date")]
        public DateTime Date { get; set; }

        [XmlElement("Year")]
        public int Year { get; set; }

        [XmlElement("Quarter")]
        public int Quarter { get; set; }

        [XmlElement("Month")]
        public int Month { get; set; }

        [XmlElement("Days")]
        public Decimal Days { get; set; }
        
        [XmlElement("Cost")]
        public Decimal Cost { get; set; }

        [XmlElement("Margin")]
        public Decimal Margin { get; set; }

        [XmlElement("Profit")]
        public Decimal Profit { get; set; }

        [XmlElement("Forecast")]
        public Decimal Forecast { get; set; }

    }
}
