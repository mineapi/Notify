using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.DataTemplates
{
    public class CalendarEvent
    {
        /// <summary>
        /// The unix timestamp for the start date.
        /// </summary>
        public int StartTime { get; set; }
        /// <summary>
        /// The unix timestamp for the end date.
        /// </summary>
        public int EndTime { get; set; }

        /// <summary>
        /// Title of the event.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Location of the event.
        /// </summary>
        public string Location { get; set; }

        public string DateString => DateTime.UnixEpoch.AddSeconds(StartTime).ToLocalTime().FormatDate();
        public string TimeString => DateTime.UnixEpoch.AddSeconds(StartTime).ToLocalTime().TimeTo12HourString(true);
    }
}
