using System.Collections.ObjectModel;
using tidewater_comp_2023.DataTemplates;
using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.Pages;

public partial class ChatPage : ContentPage
{
    private ChatManager chatManager;

    public ChatPage()
    {
        //Disable navigation page bar.
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        //Initialize manager for saving chats.
        chatManager = new ChatManager();

        InitializeComponent();

        LoadChats();

        //Add handler for selecting chat from list.
        Pane.chatsList.SelectionChanged += ChatsListOnSelectionChanged;

        GlobalPages.ChatPage = this;

    }

    public void LoadChats()
    {
        //Create placeholder chat.
        chatManager.TryCreateNewConversation(new()
        {
            RecipientName = "Jim Halpert",
            RecipientEmail = "jhalpert@vbschools.com",
            RecipientProfilePicture = "icon_user.svg",
            UnixDate = 1674774840,
            Messages = new []
            {
                new ChatBubbleContent
                {
                    ChatBubbleText = "Hello",
                    ChatBubbleTimestamp = 1674774840,
                    RecipientMessage = false,
                },
            },
        });

        chatManager.RetrieveChatDetails();

        //Propagate chat list.
        Collection<ChatDetails> details = new Collection<ChatDetails>(chatManager.ChatDetails);

        Pane.chatsList.ItemsSource = details;
    }

    /// <summary>
    /// Open an expand chat based on what the user selected.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ChatsListOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as ChatDetails;

        Pane.chatsList.SelectedItem = null;

        if (item != null)
        {
            ExpandChat newPage = new ExpandChat(item, chatManager);

            await Navigation.PushAsync(newPage, true);
        }
    }

    //Toggle flyout.
    private void Btn_menu_OnClicked(object sender, EventArgs e)
    {
        GlobalPages.FlyoutPage.IsPresented = !GlobalPages.FlyoutPage.IsPresented;
    }
}