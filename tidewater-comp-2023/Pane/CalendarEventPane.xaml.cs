using System.Collections.ObjectModel;
using tidewater_comp_2023.DataTemplates;

namespace tidewater_comp_2023.Pane;

public partial class CalendarEventPane : ContentView
{
    public CalendarEventPane()
	{
		InitializeComponent();
    }

    //Refresh pane with new events.
    public void UpdatePane(CalendarEvent[] events)
    {
        Collection<CalendarEvent> collection = new Collection<CalendarEvent>(events.ToList());

        eventsList.ItemsSource = collection;
    }
}