using tidewater_comp_2023.Pages;

namespace tidewater_comp_2023;

public partial class RoleSelectionPage : ContentPage
{
    public RoleSelectionPage()
	{
		InitializeComponent();

        //Hide navigation bar
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetBackButtonTitle(this, null);
    }

    //Selected parent
    private async void Btn_parent_OnClicked(object sender, EventArgs e)
    {
        GlobalPages.IsStudent = false;
        Page page = (Page)Activator.CreateInstance(typeof(StudentView));
        await Navigation.PushAsync(page);
    }

    //Selected student
    private async void Btn_student_OnClicked(object sender, EventArgs e)
    {
        GlobalPages.IsStudent = true;
        Page page = (Page) Activator.CreateInstance(typeof(StudentView));
        await Navigation.PushAsync(page);
    }

    //Pressed terms of use button.
    private async void TermsOfUseButton_OnClicked(object sender, EventArgs e)
    {
        //Make fake announcement with terms of use (cheeky :>)
        await Navigation.PushAsync(new ExpandAnnouncement(new AnnouncementDetails()
        {
            AnnouncementTitle = "Terms of Use",
            AnnouncementDescription =
                "Welcome to Notify!\n\nThese terms and conditions outline the rules and regulations for the use of Notify.\n\nBy accessing this app, we assume you accept these terms and conditions. Do not continue to use Notify if you do not agree to take all of the terms and conditions stated on this page.\n\nThe following terminology applies to these Terms and Conditions, Privacy Statement and Disclaimer Notice and all Agreements: \"Client\", \"You\" and \"Your\" refers to you, the person log on this app and compliant to the Company’s terms and conditions. \"The Company\", \"Ourselves\", \"We\", \"Our\" and \"Us\", refers to our Company. \"Party\", \"Parties\", or \"Us\", refers to both the Client and ourselves. All terms refer to the offer, acceptance and consideration of payment necessary to undertake the process of our assistance to the Client in the most appropriate manner for the express purpose of meeting the Client’s needs in respect of provision of the Company’s stated services, in accordance with and subject to, prevailing law of Netherlands. Any use of the above terminology or other words in the singular, plural, capitalization and/or he/she or they, are taken as interchangeable and therefore as referring to the same.\n\nLicense\n\nUnless otherwise stated, Notify and/or its licensors own the intellectual property rights for all material on Notify. All intellectual property rights are reserved. You may access this from Notify for your own personal use subjected to restrictions set in these terms and conditions.\n\nYou must not:\n\nRepublish material from Notify\n\nSell, rent or sub-license material from Notify\n\nReproduce, duplicate or copy material from Notify\n\nRedistribute content from Notify\n\nThis agreement shall begin on the date hereof. Our Terms and Conditions were created with the help of the Free Terms and Conditions Generator."
        }), true);
    }
}