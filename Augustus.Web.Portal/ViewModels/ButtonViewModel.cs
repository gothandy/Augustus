namespace Augustus.Web.Portal.ViewModels
{
    public partial class ButtonViewModel
    {
        private ButtonViewModel() { }

        public string Name { get; private set; }
        public string Verb { get; private set; }
        public string Icon { get; private set; }
        public string Type { get; private set; }
        public string Url { get; private set; }

        public static readonly ButtonViewModel Edit = new ButtonViewModel { Verb = "Edit", Icon = "glyphicon-edit", Type = "btn-default" };
        public static readonly ButtonViewModel Open = new ButtonViewModel { Verb = "Open", Icon = "glyphicon-new-window", Type = "btn-default" };
        public static readonly ButtonViewModel Save = new ButtonViewModel { Verb = "Save", Icon = "glyphicon-floppy-disk", Type = "btn-primary" };
        public static readonly ButtonViewModel Delete = new ButtonViewModel { Verb = "Delete", Icon = "glyphicon-trash", Type = "btn-danger" };
        public static readonly ButtonViewModel New = new ButtonViewModel { Verb = "New", Icon = "glyphicon-plus", Type = "btn-default" };

        public static ButtonViewModel Create(ButtonViewModel template, string name, string url)
        {
            return new ButtonViewModel
            {
                Name = name,
                Verb = template.Verb,
                Icon = template.Icon,
                Type = template.Type,
                Url = url
            };
        }
    }
}