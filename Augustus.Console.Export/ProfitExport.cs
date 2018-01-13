using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Augustus.Domain.Enums;

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

        [XmlElement("Month")]
        public DateTime Month { get; set; }

        [XmlElement("Profit")]
        public Decimal Profit { get; set; }

        [XmlElement("Cost")]
        public Decimal Cost { get; set; }

        [XmlElement("Margin")]
        public Decimal Margin { get; set; }

        [XmlElement("Days")]
        public Decimal Days { get; set; }
    }
}
