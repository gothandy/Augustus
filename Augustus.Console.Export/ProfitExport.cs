using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Augustus.Domain.Enums;
using Augustus.Domain.Objects;

namespace Augustus.Console.Export
{
    [XmlRoot("Profit")]
    public class ProfitExport : List<AccountByMonthExport>
    {

    }

    [XmlRoot("AccountByMonth")]
    public class AccountByMonthExport
    {
        public AccountByMonthExport()
        {
        }

        [XmlElement("Account")]
        public string Account { get; set; }

        [XmlElement("MonthStart")]
        public DateTime MonthStart { get; set; }

        [XmlElement("Year")]
        public int Year { get; set; }

        [XmlElement("Quarter")]
        public int Quarter { get; set; }

        [XmlElement("Month")]
        public int Month { get; set; }

        [XmlElement("BillableDays")]
        public Decimal BillableDays { get; set; }
        
        [XmlElement("CostAllocation")]
        public Decimal CostAllocation { get; set; }

        [XmlElement("Margin")]
        public Decimal Margin { get; set; }

        [XmlElement("Profit")]
        public Decimal Profit { get; set; }

        [XmlElement("ForecastDayRate")]
        public Decimal ForecastDayRate { get; set; }

        [XmlElement("ForecastMargin")]
        public decimal ForecastMargin { get; set; }
    }
}
