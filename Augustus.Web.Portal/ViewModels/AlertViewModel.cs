using System;
using System.Collections.Generic;

namespace Augustus.Web.Portal.ViewModels
{
    public enum AlertLevel
    {
        Success,
        Info,
        Warning,
        Danger
    }

    public class AlertViewModel
    {
        public string Text { get; set; }
        public AlertLevel Level { get; set; }
    }
}