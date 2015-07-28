using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EECS494.IntoTheDarkness
{
    static public class TextMaster
    {
        static string current_string = string.Empty;
        static public String DisplayString
        {
            get
            {
                return current_string;
            }
            set
            {
                current_string = value;
            }
        }
    }
}
