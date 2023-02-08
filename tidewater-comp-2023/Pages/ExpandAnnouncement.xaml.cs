namespace tidewater_comp_2023.Pages;

public partial class ExpandAnnouncement : ContentPage
{
    public ExpandAnnouncement(AnnouncementDetails details)
	{
        //Disable navigation bar.
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);

        InitializeComponent();

        //Update details.
        lbl_title.Text = details.AnnouncementTitle;
        lbl_description.Text = details.AnnouncementDescription;
    }

    //Back button.
    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(true);
    }
}