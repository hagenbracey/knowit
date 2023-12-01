using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    public class KnowitThemes
    {
        public Color primary;
        public Color primaryDark;
        public Color secondary;
        public Color tertiary;
        public string themeName;

        public KnowitThemes()
        {
            primary = Color.FromArgb("#065143");
            primaryDark = Color.FromArgb("#05493c");
            secondary = Color.FromArgb("#D9FFEE");
            tertiary = Color.FromArgb("#708D81");
            
        }

        public KnowitThemes(string _themeName)
        {
            themeName = _themeName;

            if (themeName == "blue")
            {
                primary = Color.FromArgb("#063951");
                primaryDark = Color.FromArgb("#053449");
                secondary = Color.FromArgb("#D9FDFF");
                tertiary = Color.FromArgb("#708B8D");
            }
            else if (themeName == "red")
            {
                
                primary = Color.FromArgb("#510614");
                primaryDark = Color.FromArgb("#491A05");
                secondary = Color.FromArgb("#FFDBD9");
                tertiary = Color.FromArgb("#8D707C");

            }
            else
            {

                primary = Color.FromArgb("#065143");
                primaryDark = Color.FromArgb("#05493c");
                secondary = Color.FromArgb("#D9FFEE");
                tertiary = Color.FromArgb("#708D81");
            }
        }
    }
}
