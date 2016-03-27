namespace Augustus.CRM.Attributes
{
    public static class StringAttribute
    {
        public static string GetAttributeString(this BaseEntity entity, string attributeLogicalName)
        {
            return entity.GetAttributeValue<string>(attributeLogicalName);
        }

        public static void SetAttributeString(this BaseEntity entity, string attributeLogicalName, string value)
        {
            if (value != null)
            {
                entity.SetBaseAttributeValue(attributeLogicalName, value);
            }
        }
    }
}
