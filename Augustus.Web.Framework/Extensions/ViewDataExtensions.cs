using System;
using System.Web.Mvc;

namespace Augustus.Web.Framework.Extensions
{
    public static class ViewDataExtensions
    {
        public static T CastAndCheckIsNotNull<T>(this ViewDataDictionary viewData, string key = null)
        {
            if (key == null) key = typeof(T).Name;

            T value = (T)viewData[key];

            if (value == null) throw new ArgumentNullException(string.Format("{0} meta data is required.", key));

            return value;
        }

        public static void AddItemOfType<T>(this ViewDataDictionary viewData, T value, string key = null)
        {
            if (key == null) key = typeof(T).Name;

            // Check if key exists?

            viewData[key] = value;
        }
    }
}