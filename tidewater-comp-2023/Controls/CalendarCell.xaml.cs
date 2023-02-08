using Microsoft.Maui.Controls.Shapes;

namespace tidewater_comp_2023.Controls;

public partial class CalendarCell : Frame
{
    public DateTime Day;
    public bool AltDay { get; set; }

    public bool HasEvent = false;

    public Ellipse? Indicator;

    public CalendarCell(string calendarNumber, bool altDay, DateTime day)
	{
		InitializeComponent();

        AltDay = altDay;
        Day = day;

        Number.Text = calendarNumber;
        Number.TextColor = AltDay ? Colors.Gray : Colors.Black;
    }

    /// <summary>
    /// Highlights the cell in blue and changes the color of the text.
    /// </summary>
    public void SelectCell(CalendarCell cell)
    {
        if (this != cell)
        {
            DeselectCell();
            return;
        }

        BackgroundColor = Color.FromArgb("3f48cc");
        Number.TextColor = Colors.White;
        Number.Margin = 10;

        if (Indicator != null) StackLayout.Remove(Indicator);
    }

    /// <summary>
    /// Makes the cell transparent and changes the color of the text.
    /// </summary>
    public void DeselectCell()
    {
        BackgroundColor = Colors.Transparent;
        Number.TextColor = AltDay ? Colors.Gray : Colors.Black;
        Number.Margin = 4;
        if (HasEvent)
            DisplayIndicator();
    }

    public void DisplayIndicator()
    {
        Indicator = new Ellipse
        {
            WidthRequest = 10,
            HeightRequest = 10,
            Fill = Color.FromArgb("0064fe"),
        };

        StackLayout.Add(Indicator);
    }

    public void AddClick(EventHandler e)
    {
        TapGestureRecognizer.Tapped += e;
    }
}