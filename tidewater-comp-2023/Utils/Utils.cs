using CommunityToolkit.Maui.Views;

namespace tidewater_comp_2023.Utils
{
    public static class Utils
    {
        private static readonly string[] MONTHS = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        /// <summary>
        /// Convert a time into a string
        /// </summary>
        /// <param name="time">Input time</param>
        /// <param name="alt">If alternative format is used</param>
        /// <returns>Formats in HH:MM PM/AM or pm/am if alt format is used.</returns>
        public static string TimeTo12HourString(this DateTime time, bool alt)
        {
            int hour = time.Hour;
            int minute = time.Minute;
            bool pm = 12 - hour < 0;

            hour = 12 - hour < 0 ? hour - 12 : hour;

            if (alt)
                return $"{hour}:{minute.ToString("00")} {(pm ? "PM" : "AM")}";
            
            return $"{hour}:{minute.ToString("00")}{(pm ? "pm" : "am")}";
        }

        public static string IntToMonthString(this int month) =>
            MONTHS[month - 1];

        /// <summary>
        /// Format date
        /// </summary>
        /// <param name="date">Input</param>
        /// <returns>Returns in format DD \n MM</returns>
        public static string FormatDate(this DateTime date) =>
            $"{date.Day}\n{date.Month.IntToMonthString()}";

        /// <summary>
        /// Format date
        /// </summary>
        /// <param name="date">Input</param>
        /// <returns>Returns in format of MM DD</returns>
        public static string AltFormatDate(this DateTime date) =>
            $"{date.Month.IntToMonthString()} {date.Day}";

        /// <summary>
        /// For managers -> Merge an array of strings into one line.
        /// </summary>
        /// <param name="lines">Input array</param>
        /// <returns>A single string from all ines.</returns>
        public static string MergeArray(this string[] lines)
        {
            string output = "";

            foreach (String s in lines)
            {
                output += s;
            }

            return output;
        }

        /// <summary>
        /// Display a popup with a header and details.
        /// </summary>
        /// <param name="header">The header text of the popup.</param>
        /// <param name="text">The details text of the popup.</param>
        /// <returns>Popup</returns>
        public static Popup InformationPopup(string header, string text)
        {
            Button okButton = new Button()
            {
                Text = "OK",
                WidthRequest = 50,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 10, 0, 0),
                TextColor = Color.FromArgb("2540d9"),
                BackgroundColor = Colors.Transparent,
                FontFamily = "SegoeUIBold",
            };

            Popup errorPopup = new Popup()
            {
                Content = new Frame()
                {
                    Content = new VerticalStackLayout()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Text = header,
                                FontFamily = "SegoeUIBold",
                                FontSize = 16,
                                Margin = new Thickness(0, 0, 0, 10)
                            },
                            new Label()
                            {
                                Text = text,
                                TextColor = Colors.DarkGray,
                                HorizontalOptions = LayoutOptions.Start,
                            },
                            okButton,
                        }
                    },
                },
                Color = Colors.Transparent
            };

            okButton.Clicked += (_, _) => errorPopup.Close();

            return errorPopup;
        }
    }
}
