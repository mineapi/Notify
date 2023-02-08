using CommunityToolkit.Maui.Views;

namespace tidewater_comp_2023.Pane;

public partial class AbscencePane : ContentView
{
	public AbscencePane()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Submit
    /// </summary>
    private void SubmitButton_Clicked(object sender, EventArgs e)
    {
        //Throw error if id is invalid
        if (IDEntry.Text == null || IDEntry.Text.Replace(" ", "") == "")
        {
            ((ContentPage)this.Parent.Parent.Parent.Parent).ShowPopup(Utils.Utils.InformationPopup("Incomplete Form",
                "Please fill out the student ID field!"));
            return;
        }

        //Reset
        IDEntry.Text = "";
        DatePicker.Date = DateTime.Today;
        TimePicker.Time = TimeSpan.FromMilliseconds(0);

        //Show success.
        ((ContentPage)this.Parent.Parent.Parent.Parent).ShowPopup(Utils.Utils.InformationPopup("Absence Submitted", "Your absence report has been submitted and recorded."));
    }
}