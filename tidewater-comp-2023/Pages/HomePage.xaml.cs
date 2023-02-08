using System.Collections.ObjectModel;
using tidewater_comp_2023.DataTemplates;
using tidewater_comp_2023.Pages;
using tidewater_comp_2023.Pane;
using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        GlobalPages.HomePage = this;

        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetBackButtonTitle(this, null);

        
        InitializeComponent();

        //Add handler when user presses announcement.
        AnnouncementPane.announcementList.SelectionChanged += PushAnnouncement;

        //Add placeholder announcements
        Collection<AnnouncementDetails> Announcements = new Collection<AnnouncementDetails>
        {
            new()
            {
                AnnouncementAuthor = "Admin",
                AnnouncementDate = new DateTime(2023, 1, 31).AltFormatDate(),
                AnnouncementDescription = "The third grading period begins!",
                AnnouncementTitle = "New Semester",
                ProfilePictureSource = "icon_user.svg",
            },
            new()
            {
                AnnouncementAuthor = "Admin",
                AnnouncementDate = new DateTime(2023, 2, 8).AltFormatDate(),
                AnnouncementDescription = "Have you applied for the membership in NTHS?\nThe National Technical Honor Society applications for the 22-23 school year are due no later than February 17th.  All parts of the application process must be completed by the deadline to be considered for membership during this school year.\nWhy be a member of NTHS?. NTHS believes in CTE students, the heartbeat of today's workforce! NTHS celebrates the accomplishments of today's career and technical education students, empowering them to know the value and impact of their career paths and trades.",
                AnnouncementTitle = "NTHS Membership",
                ProfilePictureSource = "icon_user.svg",
            },
            new()
            {
                AnnouncementAuthor = "Admin",
                AnnouncementDate = new DateTime(2023, 2, 10).AltFormatDate(),
                AnnouncementDescription = "NTHS members will have a meeting on February 14 (Tuesday).",
                AnnouncementTitle = "Upcoming NTHS Meeting",
                ProfilePictureSource = "icon_user.svg",
            },
        };

        AnnouncementPane.announcementList.ItemsSource = Announcements;

        //Add placeholder upcoming events
        Collection<UpcomingDetails> UpcomingDetails = new Collection<UpcomingDetails>
        {
            new()
            {
                AssignmentTitle = "Review Questions Due",
                UnixDue = 1699618800,
            },
            new()
            {
                AssignmentTitle = "Chapter 2 Test",
                UnixDue = 1699880400,
            }
        };

        ComingUpPane.comingUpList.ItemsSource = UpcomingDetails;

        //Show the absence menu if user is parent
        if (!GlobalPages.IsStudent)
        {
            Layout.Add(new Label
            {
                Text = "Absence",
                TextColor = Colors.Black,
                FontSize = 24,
                FontFamily = "SegoeUIBold",
            });
            Layout.Add(new AbscencePane());
        }

        //Change the "student" or "parent" text under name.
        RoleLabel.Text = GlobalPages.IsStudent ? "Student" : "Parent";
    }

    /// <summary>
    /// Display announcement details.
    /// </summary>
    async void PushAnnouncement(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as AnnouncementDetails;

        AnnouncementPane.announcementList.SelectedItem = null;

        if (item != null)
        {
            ExpandAnnouncement newPage = new ExpandAnnouncement(item);

            await Navigation.PushAsync(newPage, true);
        }
    }

    //Toggle flyout
    private void Btn_menu_OnClicked(object sender, EventArgs e)
    {
        GlobalPages.FlyoutPage.IsPresented = !GlobalPages.FlyoutPage.IsPresented;
    }
}