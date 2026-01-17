using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han_fe.Services.State
{
    public class StringState
    {
        public string Capitalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }
    }
}