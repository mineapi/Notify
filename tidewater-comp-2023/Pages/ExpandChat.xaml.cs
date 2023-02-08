using tidewater_comp_2023.DataTemplates;
using tidewater_comp_2023.Pane;
using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.Pages;

public partial class ExpandChat : ContentPage
{
    private ChatManager chatManager;

    private ChatDetails details;
    private List<ChatBubbleContent> messages;

    public ExpandChat(ChatDetails details, ChatManager chatManager)
	{
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        InitializeComponent();

        //Propagate chat details at top.
        ProfilePictureImage.Source = details.RecipientProfilePicture;

        NameLabel.Text = details.RecipientName;
        EmailLabel.Text = details.RecipientEmail;
        
        //Add messages
        messages = details.Messages.ToList();

        //When the enter key is pressed, send message.
        Chatbox.ReturnCommand = new Command(SendMessage);

        this.details = details;
        this.chatManager = chatManager;

        UpdateMessages();
    }

    /// <summary>
    /// Redraw chat messages
    /// </summary>
    private void UpdateMessages()
    {
        //Clear the area
        ChatArea.Clear();

        //Add messages
        foreach (ChatBubbleContent c in messages)
        {
            ChatArea.Add(new ChatBubblePane(c));
        }

        //Scroll to bottom
        ScrollView.ScrollToAsync(ChatArea, ScrollToPosition.End, true);
    }

    /// <summary>
    /// Send message
    /// </summary>
    private void SendButton_OnClicked(object sender, EventArgs e) => SendMessage();

    /// <summary>
    /// Send message
    /// </summary>
    private void SendMessage()
    {
        //Add a new message with the chat text.
        messages.Add(new ChatBubbleContent()
        {
            ChatBubbleText = Chatbox.Text,
            ChatBubbleTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            RecipientMessage = true,
        });

        //Trick the keyboard into disappearing. https://stackoverflow.com/a/74771913
        Chatbox.IsEnabled = false;
        Chatbox.IsEnabled = true;

        //Reset text in input box.
        Chatbox.Text = "";

        //Update all messages in manager.
        chatManager.UpdateMessages(details, messages.ToArray());
        //Write to file.
        chatManager.WriteChatDetails();

        //Redraw
        UpdateMessages();
    }

    private void SendImage(String image)
    {
        //Add a new message with an image.
        messages.Add(new ChatBubbleContent()
        {
            ChatBubbleText = "",
            ChatBubbleImage = image,
            ChatBubbleTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            RecipientMessage = true,
        });

        //Trick the keyboard into disappearing. https://stackoverflow.com/a/74771913
        Chatbox.IsEnabled = false;
        Chatbox.IsEnabled = true;

        //Reset text in input box.
        Chatbox.Text = "";

        //Update all messages in manager.
        chatManager.UpdateMessages(details, messages.ToArray());
        //Write to file
        chatManager.WriteChatDetails();

        //Redraw
        UpdateMessages();
    }

    //Back button
    private async void BackButton_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(true);
        GlobalPages.ChatPage.LoadChats();
    }

    //Open file picker when new image button is pressed.
    private async void ImageButton_OnClicked(object sender, EventArgs e)
    {
        //Create file picker.
        var result = await FilePicker.PickAsync(new PickOptions()
        {
            PickerTitle = "Select an Image to Send",
            FileTypes = FilePickerFileType.Images,
        });

        //Return if user picked nothing
        if (result == null)
            return;

        //Read image
        var stream = await result.OpenReadAsync();

        //Send the image in the chat.
        SendImage(result.FullPath);
    }
}