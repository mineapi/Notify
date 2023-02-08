using tidewater_comp_2023.DataTemplates;
using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.Pane;

public partial class ChatBubblePane : ContentView
{
	public ChatBubblePane(ChatBubbleContent content)
	{
		InitializeComponent();

        //Propagate bubble with data, self explanatory code.

        TextLabel.Text = content.ChatBubbleText;

        DateTime dateTime = DateTime.UnixEpoch.ToLocalTime().AddSeconds(content.ChatBubbleTimestamp);

        DateLabel.Text = dateTime.TimeTo12HourString(false);

        Image.Source = content.ChatBubbleImage;

        bool hasImage = content.ChatBubbleImage != null;

        Bubble.BackgroundColor = content.RecipientMessage ? Color.FromArgb("e7f0ff") : Color.FromArgb("ebebeb");
        Bubble.HorizontalOptions = content.RecipientMessage ? LayoutOptions.End : LayoutOptions.Start;

        Bubble.Padding = hasImage ? 0 : Bubble.Padding;

        //Add image if this is an image bubble.
        if (hasImage)
        {
            DateLabel.Margin = new Thickness(0, 10, 10, 10);
            TextLabel.HeightRequest = 0;
            Bubble.CornerRadius = 20;
        }
        else
            Image.HeightRequest = 0;
    }
}