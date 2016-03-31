using System;

namespace Augustus.Web.Portal.Models
{
    public class FormButtons
    {
        public Guid? Id { get; set; }
        public string CancelUrl { get; set; }

        public FormButtons(string cancelUrl)
        {
            CancelUrl = cancelUrl;
        }

        public FormButtons(Guid? id, string cancelUrl)
        {
            Id = id;
            CancelUrl = cancelUrl;
        }
    }
}