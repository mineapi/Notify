using CommunityToolkit.Maui.Views;
using tidewater_comp_2023.Pane;
using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.Pages;

public partial class NewEventPage : ContentPage
{
    private CalendarManager manager;
    private CalendarPane pane;

    public NewEventPage(CalendarManager manager, CalendarPane pane)
	{
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

		InitializeComponent();

        Date.Date = DateTime.Now;
        this.manager = manager;
        this.pane = pane;
    }

    //Back button
    private async void BackButton_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(true);
    }

    /// <summary>
    /// Submit event
    /// </summary>
    private async void SubmitButton_OnClicked(object sender, EventArgs e)
    {
        //Check if user provided title.
        if (TitleEntry.Text == null || TitleEntry.Text.Replace(" ", "").Length < 1)
        {
            this.ShowPopup(Utils.Utils.InformationPopup("Incomplete Form", "You must provide a title for this event."));
            return;
        }

        //Create new event.
        bool result = manager.TryCreateNewEvent(new()
        {
            StartTime = (int)((DateTimeOffset)Date.Date.AddSeconds(StartTime.Time.TotalSeconds)).ToUnixTimeSeconds(),
            EndTime = (int)((DateTimeOffset)Date.Date.AddSeconds(EndTime.Time.TotalSeconds)).ToUnixTimeSeconds(),
            Location = LocationEntry.Text ?? "",
            Title = TitleEntry.Text
        });

        //If event could be created, go back, if not, show error.
        if (result)
        {
            await Navigation.PopAsync(true);
            await pane.UpdateCalendar(Date.Date.Month, Date.Date.Year);
        }
        else
            this.ShowPopup(Utils.Utils.InformationPopup("Cannot Create Event", "An event with that name has already been created."));
    }
}