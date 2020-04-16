using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Helpers
{
    public static class TextHelper
    {
        public static string TextTransform(this string Value) => Value.Trim().ToLower();
    }
}