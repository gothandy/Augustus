﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Augustus.Web.Framework.Helpers
{
    public class CurrencyHelper
    {
        public static string Format(decimal? currency)
        {
            if (!currency.HasValue) return null;
            if (currency.Value == 0) return null;

            return string.Format("{0:n2}", currency);
        }
    }
}