using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using tidewater_comp_2023.DataTemplates;
using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.Pages;

public partial class HelpPage : ContentPage
{
    public HelpPage()
    {
        //Disable navigation bar
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        InitializeComponent();
    }

    //Toggle flyout
    private void Btn_menu_OnClicked(object sender, EventArgs e)
    {
        GlobalPages.FlyoutPage.IsPresented = !GlobalPages.FlyoutPage.IsPresented;
    }

    /// <summary>
    /// Submit
    /// </summary>
    private void SubmitButton_OnClicked(object sender, EventArgs e)
    {
        //Throw error if no issue is selected
        if (IssueTypePicker.SelectedItem == null)
        {
            this.ShowPopup(Utils.Utils.InformationPopup("Incomplete Form", "Please select an issue type."));
            return;
        }

        //Throw error if user inputted invalid text
        if (DescriptionEditor.Text == null || DescriptionEditor.Text.Replace(" ", "") == "")
        {
            this.ShowPopup(Utils.Utils.InformationPopup("Incomplete Form", "You must add a description to this report."));
            return;
        }

        //Reset
        IssueTypePicker.SelectedItem = null;
        DescriptionEditor.Text = "";

        //Show success
        this.ShowPopup(Utils.Utils.InformationPopup("Thank You", "Your issue report has been submitted and received."));
    }
}