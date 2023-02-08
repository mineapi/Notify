using CommunityToolkit.Maui.Views;
using tidewater_comp_2023.Controls;
using tidewater_comp_2023.DataTemplates;
using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.Pane;

public partial class CalendarPane : ContentView
{
    private List<CalendarCell> CalendarCells;

    private DateTime CurrentDay;

    public CalendarEventPane EventPane;

    private CalendarManager manager;

	public CalendarPane()
	{
		InitializeComponent();

        CalendarCells = new List<CalendarCell>();
        CurrentDay = DateTime.Today;

        manager = CalendarPage.Manager;

        UpdateCalendar(CurrentDay.Month, CurrentDay.Year);
    }

    /// <summary>
    /// Add days to the top of calendar.
    /// </summary>
    public void PropagateWeek()
    {
        Grid.Clear();

        char[] days = { 'S', 'M', 'T', 'W', 'T', 'F', 'S' };

        //Loop thru each column and put the day at the top.
        for (int i = 0; i < 7; i++)
        {
            Grid.Add(new Frame
            {
                Background = new Color(231, 240, 255),
                BorderColor = new Color(0, 0, 0, 0),
                CornerRadius = 0,
                Content = new Label
                {
                    Text = days[i].ToString(),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                },
                Padding = 10,
            }, i, 0);
        }
    }

    public void PropagateDays(int month, int year) //Place the right number in each calendar cell.
    {
        DateTime thisMonth = new DateTime(year, month, 1); //Create an instance of the DateTime class with the provided month and year, this is to account for leap year.

        int firstDay = (int) thisMonth.DayOfWeek; //We need a variable for the day of the week the first day of the month is.
        int day = DateTime.DaysInMonth(month - 1 == 0 ? year - 1 : year, month - 1 == 0 ? 12 : month - 1) - firstDay; //This variable is changed based on iteration through calendar cells, we set it to the last day of the last month minus the first day of the current month.
        int daysInMonth = DateTime.DaysInMonth(year, month); //We need a variable for the amount of days in the current month, obviously not every month has the same # of days.
        int overFlowDays = 0; //We need a variable to account for the days on the calendar after the last day of the month.
        bool reachedEnd = false; //This variable lets us know if we reached the end of the month and need to start counting the days of the next month.

        for (int i = 1; i < 7; i++) //We will apply each label row by row, a calendar has 7 rows.
        {
            for (int j = 0; j < 7; j++) //Each row (or week) has 7 days, so we loop thru each.
            {
                if (day == daysInMonth && i != 1) //If we aren't on the first week and the day is the same day as the last day of the month, we have reached the true last day of the month, we can start counting the days after. 
                    reachedEnd = true;

                day = j == firstDay && i == 1 ? 1 : reachedEnd ? ++overFlowDays : day + 1; //Set the day to 1 if we landed on the day of the week that is the first day of the month, this is so we stop counting the days of the previous month if the month did not start on a Sunday.

                DateTime buttonDay = CurrentDay;
                buttonDay = buttonDay.AddMonths(i == 1 && day > 7 ? -1 : reachedEnd ? 1 : 0);
                buttonDay = buttonDay.AddDays(-buttonDay.Day + day);

                CalendarCell calendarCell = new CalendarCell(day + "", 
                    (i == 1 && day > 7) || reachedEnd,
                    buttonDay);

                //dayOfButton = (i == 1 && day > 7) ? dayOfButton.AddMonths(-1) : reachedEnd ? dayOfButton.AddMonths(1) : dayOfButton;

                calendarCell.AddClick((obj, _) =>
                {
                    VerticalStackLayout vertStackLayout = (VerticalStackLayout)obj;
                    CalendarCell self = (CalendarCell)vertStackLayout.Parent;

                    foreach (CalendarCell cell in CalendarCells)
                    {
                        cell.SelectCell(self);
                    }

                    UpdateEventsList(self.Day);
                });

                CalendarCells.Add(calendarCell);

                Grid.Add(calendarCell, j, i); //Add the label to the grid.
            }
        }
    }

    private async void CalendarLeft_OnClicked(object sender, EventArgs e)
    {
        //Remove month.
        CurrentDay = CurrentDay.AddMonths(-1);

        //Animate and show indicator.

        double startX = Grid.X;
        double startY = 0;

        ActivityIndicator indicator = new ActivityIndicator
        {
            IsRunning = true,
            HeightRequest = 30,
            HorizontalOptions = LayoutOptions.Center,
        };

        TopGrid.Add(indicator, 0, 1);
        TopGrid.SetColumnSpan(indicator, 4);
        await Grid.TranslateTo(1000, startY, 250, Easing.SinOut);
        await UpdateCalendar(CurrentDay.Month, CurrentDay.Year);
        await Grid.TranslateTo(-1000, startY, 1, null);
        TopGrid.Remove(indicator);
        await Grid.TranslateTo(startX, startY, 250, Easing.SinIn);
    }

    private async void CalendarRight_OnClicked(object sender, EventArgs e)
    {
        //Add month
        CurrentDay = CurrentDay.AddMonths(1);

        //Animate and show indicator.
        double startX = Grid.X;
        double startY = 0;

        ActivityIndicator indicator = new ActivityIndicator
        {
            IsRunning = true,
            HeightRequest = 30,
            HorizontalOptions = LayoutOptions.Center,
        };
        TopGrid.Add(indicator, 0, 1);
        TopGrid.SetColumnSpan(indicator, 4);
        await Grid.TranslateTo(-1000, startY, 250, Easing.SinOut);
        await UpdateCalendar(CurrentDay.Month, CurrentDay.Year);
        await Grid.TranslateTo(1000, startY, 1, null);
        TopGrid.Remove(indicator);
        await Grid.TranslateTo(startX, startY, 250, Easing.SinIn);

    }

    /// <summary>
    /// Update calendar and propagate days.
    /// </summary>
    /// <param name="month">Current month displayed.</param>
    /// <param name="year">Current year displayed.</param>
    /// <returns></returns>
    public Task UpdateCalendar(int month, int year)
    {
        //Show month label
        string[] months =
        {
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November",
            "December"
        };

        lbl_MonthYearLabel.Text = String.Format("{0} {1}", months[CurrentDay.Month-1], CurrentDay.Year); 

        //Update grid.
        Grid.Clear();

        PropagateWeek();
        PropagateDays(month, year);
        
        //Add blue dots on days with events.
        UpdateEventIndicators();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Show blue dots on days with events.
    /// </summary>
    public void UpdateEventIndicators()
    {
        //Go thru each cell and match date, if an event exists on that day, add a blue dot.
        foreach (CalendarCell cell in CalendarCells)
        {
            foreach (CalendarEvent ev in manager.CalendarEvents)
            {
                DateTime a = cell.Day;
                DateTime b = DateTime.UnixEpoch.AddSeconds(ev.StartTime).ToLocalTime();

                bool equal = a.Month == b.Month && a.Day == b.Day && a.Year == b.Year;

                if (equal)
                {
                    cell.HasEvent = true;
                    cell.DisplayIndicator();
                }
            }
        }
    }

    /// <summary>
    /// Update list on selected day
    /// </summary>
    /// <param name="SelectedDay">The day the user selected</param>
    public void UpdateEventsList(DateTime SelectedDay)
    {
        List<CalendarEvent> CurrentDayEvents = new List<CalendarEvent>();

        //Go thru each event and match the day with the selected day and add to the list.
        foreach (CalendarEvent ev in manager.CalendarEvents)
        {
            DateTime a = SelectedDay;
            DateTime b = DateTime.UnixEpoch.AddSeconds(ev.StartTime).ToLocalTime();

            bool equal = a.Month == b.Month && a.Day == b.Day && a.Year == b.Year;

            if (equal)
                CurrentDayEvents.Add(ev);

            EventPane.UpdatePane(CurrentDayEvents.ToArray());
        }
    }
}