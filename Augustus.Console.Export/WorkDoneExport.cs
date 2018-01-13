using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Augustus.Domain.Enums;

namespace Augustus.Console.Export
{
    [XmlRoot("Export")]
    public class WorkDoneExport : List<WorkDoneItem>
    {

    }

    [XmlRoot("Item")]
    public class WorkDoneItem
    {
        public WorkDoneItem()
        {
        }

        [XmlElement("AccountName")]
        public string AccountName { get; set; }

        [XmlElement("InvoiceName")]
        public string InvoiceName { get; set; }

        [XmlElement("InvoiceDate")]
        public DateTime InvoiceDate{ get; set; }

        [XmlElement("InvoiceMargin")]
        public Decimal InvoiceMargin { get; set; }

        [XmlElement("InvoiceStatus")]
        public InvoiceStatus InvoiceStatus { get; set; }

        [XmlElement("WorkDoneDate")]
        public DateTime WorkDoneDate { get; set; }

        [XmlElement("WorkDoneMargin")]
        public Decimal WorkDoneMargin { get; set; }

        [XmlElement("WorkDoneForecast")]
        public decimal WorkDoneForecast { get; set; }


    }
}
