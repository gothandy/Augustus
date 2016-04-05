using System;
using System.Collections.Generic;

namespace Augustus.Web.Portal.ViewModels
{
    public class DropDownViewModel<TModel>
    {
        public Guid? SelectedId { get; set; }
        public bool AllowNull { get; set; }
        public IEnumerable<TModel> Items { get; set; }
        public Guid? HideSelfId { get; set; }
    }
}