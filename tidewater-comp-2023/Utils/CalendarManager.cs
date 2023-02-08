using System.Text.Json;
using tidewater_comp_2023.DataTemplates;

namespace tidewater_comp_2023.Utils
{
    public class CalendarManager
    {
        private string CalendarFilePath;

        public List<CalendarEvent> CalendarEvents;

        /// <summary>
        /// Initialize a chat manager and load from the chat file.
        /// </summary>
        public CalendarManager()
        {
            string cachePath = FileSystem.Current.CacheDirectory;

            CalendarFilePath = cachePath + "/calendar.notify";

            if (!File.Exists(CalendarFilePath))
                File.Create(cachePath + "/calendar.notify").Close();

            RetrieveCalendarEvent();
        }

        /// <summary>
        /// Serializes the CalendarEvent into the json file.
        /// </summary>
        public void WriteCalendarEvent()
        {
            File.WriteAllText(CalendarFilePath, JsonSerializer.Serialize(CalendarEvents.ToArray()));
        }

        /// <summary>
        /// Updates the CalendarEvents property with the latest information in the chat file.
        /// </summary>
        public void RetrieveCalendarEvent() 
        {
            string fileContents = File.ReadAllLines(CalendarFilePath).MergeArray();

            try
            {
                CalendarEvents = JsonSerializer.Deserialize<CalendarEvent[]>(fileContents).ToList();
            }
            catch
            {
                CalendarEvents = new List<CalendarEvent>();
            }
        }
        /// <summary>
        /// Add a new event to the file if it doesn't already exist.
        /// </summary>
        /// <param name="e">The event details.</param>
        public bool TryCreateNewEvent(CalendarEvent e)
        {
            bool matchFound = false;

            foreach (CalendarEvent ev in CalendarEvents)
            {
                matchFound = ev.Title == e.Title;
            }

            if (!matchFound)
                CalendarEvents.Add(e);

            WriteCalendarEvent();

            return !matchFound;
        }

        /// <summary>
        /// Remove a conversation if it exists.
        /// </summary>
        /// <param name="e">The e of the conversation.</param>
        public void TryDeleteEvent(CalendarEvent e)
        {
            Predicate<CalendarEvent> matcher = CalendarEvent => CalendarEvent == e;

            if (CalendarEvents.Find(matcher) == null)
                CalendarEvents.Remove(e);

            WriteCalendarEvent();
        }
    }
}