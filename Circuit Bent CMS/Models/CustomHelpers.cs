using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CircuitBentCMS.Models;
using System.Text.RegularExpressions;

namespace CircuitBentCMS.Models
{
    public static class CustomHelpers
    {
        public static string ColorPicker(string colorPickerElement, string startColor, string outputElement)
        {
            string htmlOutput = "", rgb = "";
            string[] startColorRgb = startColor.Split(',');
            if (startColorRgb.Length == 3)
            {
                rgb = String.Format("{{r: {0}, g: {1}, b: {2}}}", startColorRgb[0], startColorRgb[1], startColorRgb[2]);
            }
            htmlOutput += String.Format(@"
                            <div id='{0}' class='colorSelector'><div style='background-color: rgb({1})'></div></div>
                            <input type='hidden' name='{3}' id='{3}' value='{1}' />
                            <script>                  
                            $('#{0}').ColorPicker({{
	                            color: {2},
	                            onShow: function (colpkr) {{
		                            $(colpkr).fadeIn(500);
		                            return false;
	                            }},
	                            onHide: function (colpkr) {{
		                            $(colpkr).fadeOut(500);
		                            return false;
	                            }},
	                            onChange: function (hsb, hex, rgb) {{
                                    var colorRgb = rgb.r + ',' + rgb.g + ',' + rgb.b;
		                            $('#{0} div').css('background-color', 'rgb(' + colorRgb + ')');
                                    $('#{3}').val(colorRgb);
                                    updateStyles();
	                            }}
                            }});
                            </script>", colorPickerElement, startColor, rgb, outputElement);

            return htmlOutput;
        }

        public static string GeneratePassword(int howLong)
        {
            string pwd = "";

            Random random = new Random();

            for (int i = 0; i < howLong; i++)
            {
                pwd += Convert.ToString(random.Next(1, 10));
            }

            return pwd;
        }

        public static string Truncate(string input = "", int length = 0, bool dots = true)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                if (dots == true)
                    return input.Substring(0, length) + "...";
                else
                    return input.Substring(0, length);
            }

        }

        // formatera datum
        // skriv ut idag, igår eller datumet som "14 april"
        public static string FormatDate(DateTime date, bool shortMonth)
        {
            string output = string.Empty;

            DateTime today = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "W. Europe Standard Time");
            if (date.Month == today.Month && date.Day == today.Day)
            {
                output = "today";
            }
            else if (date.Month == today.Month && (today.Day - date.Day) == 1)
            {
                output = "yesterday";
            }
            else
            {
                output = date.Day.ToString() + " ";

                if (shortMonth)
                    output += date.ToString("MMMM").Substring(0, 3);
                else
                    output += date.ToString("MMMM");
            }
            return output;
        }


        public static string CreateSlug(string userInput)
        {
            // to lower case and remove whitespace
            userInput = userInput.ToLower().Trim();

            // replace whitespace
            userInput = userInput.Replace(" ", "-");

            // remove ?
            userInput = userInput.Replace("?", "");

            // remove &
            userInput = userInput.Replace("&", "");

            // remove &
            userInput = userInput.Replace(".", "");

            // remove ' & "
            userInput = userInput.Replace("'", "");
            userInput = userInput.Replace("\"", "");

            // remove accents and stuff
            string from =   "åãàáäâẽèéëêìíïîõòóöôùúüûñç·/_,:;";
            string to =     "aaaaaaeeeeeiiiiooooouuuunc------";

            // run through all the characters and replace
            for (int i = 0; i < from.Length; i++)
            {
                userInput = userInput.Replace(from.Substring(i, 1), to.Substring(i, 1));
            }

            // remove invalid characters
            userInput = Regex.Replace(userInput, "/[^a-z0-9 -]/g", "");
            
            // replace line breaks
            userInput = Regex.Replace(userInput, "/\n+/g", "-");

            // collapse dashes
            userInput = Regex.Replace(userInput, "/-+/g", "-");

            return userInput;
        }

        public static string BrightenColor(string color)
        {
            // this code makes links a bit brighter when hovering
            string[] colorSeparate = color.Split(',');
            string colorHover = "";
            for (int i = 0; i < colorSeparate.Length; i++)
            {
                int colorNumeric = Convert.ToInt32(colorSeparate[i]);
                int colorIncrement = 30;
                if (colorNumeric > (255 - colorIncrement))
                {
                    colorNumeric = 255;
                }
                else
                {
                    colorNumeric += colorIncrement;
                }

                colorSeparate[i] = colorNumeric.ToString();
                if (i < 2)
                {
                    colorHover += colorSeparate[i] + ",";
                }
                else
                {
                    colorHover += colorSeparate[i];
                }
            }

            return colorHover;
        }

        public static string RemoveSwedishChars(string input)
        {
            input = input.ToLower().Replace("å", "a");
            input = input.ToLower().Replace("ä", "a");
            input = input.ToLower().Replace("ö", "o");
            return input;
        }

        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if (html == null || html == string.Empty)
                return string.Empty;

            if (allowHarmlessTags)
                return System.Text.RegularExpressions.Regex.Replace(html, "</?(?i:script|embed|object|frameset|frame|iframe|meta|link|style)(.|\\n)*?>", string.Empty);

            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
        }

    }
}