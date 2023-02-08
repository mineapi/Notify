using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.DataTemplates
{
    internal class UpcomingDetails
    {
        public String AssignmentTitle { get; set; }
        public int UnixDue { get; set; }
        public String AssignmentDueDate
        {
            private set
            {

            }
            get
            {
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(UnixDue).ToLocalTime();

                return String.Format("{0} {1} at {2}", dateTime.Month.IntToMonthString(), dateTime.Day, dateTime.TimeTo12HourString(false));
            }
        }
    }
}
