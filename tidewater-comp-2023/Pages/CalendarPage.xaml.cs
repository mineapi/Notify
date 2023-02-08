using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tidewater_comp_2023.Pages;
using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023;

public partial class CalendarPage : ContentPage
{
    public static CalendarManager Manager;

    public CalendarPage()
    {
        //Disable navigation page bar.
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetBackButtonTitle(this, null);

        //Initialize manager for saving calendar events.
        Manager = new CalendarManager();

        InitializeComponent();

        CalendarPane.EventPane = EventPane;

        //Create a placeholder event.
        Manager.TryCreateNewEvent(new ()
        {
            StartTime = 1675918799,
            EndTime = 1675918800,
            Location = "",
            Title = "Review Questions Due"
        });
    }

    private void Btn_menu_OnClicked(object sender, EventArgs e) //Toggle the sidebar
    {
        GlobalPages.FlyoutPage.IsPresented = !GlobalPages.FlyoutPage.IsPresented;
    }

    private async void NewEventButton_OnClicked(object sender, EventArgs e) //Open the new event activity.
    {
        await Navigation.PushAsync(new NewEventPage(Manager, CalendarPane), true);
    }
}