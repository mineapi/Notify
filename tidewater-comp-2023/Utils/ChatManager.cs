using System.Text.Json;
using tidewater_comp_2023.DataTemplates;

namespace tidewater_comp_2023.Utils
{
    public class ChatManager
    {
        private string ChatFilePath;

        public List<ChatDetails> ChatDetails;

        /// <summary>
        /// Initialize a chat manager and load from the chat file.
        /// </summary>
        public ChatManager()
        {
            string cachePath = FileSystem.Current.CacheDirectory;

            ChatFilePath = cachePath + (GlobalPages.IsStudent ? "/StudentChat.notify" : "/ParentChat.notify");

            if (!File.Exists(ChatFilePath))
                File.Create(cachePath + (GlobalPages.IsStudent ? "/StudentChat.notify" : "/ParentChat.notify")).Close();

            RetrieveChatDetails();
        }

        /// <summary>
        /// Serializes the ChatDetails into the json file.
        /// </summary>
        public void WriteChatDetails()
        {
            File.WriteAllText(ChatFilePath, JsonSerializer.Serialize(ChatDetails.ToArray()));
        }

        /// <summary>
        /// Updates the ChatDetails property with the latest information in the chat file.
        /// </summary>
        public void RetrieveChatDetails() 
        {
            string fileContents = File.ReadAllLines(ChatFilePath).MergeArray();

            try
            {
                ChatDetails = JsonSerializer.Deserialize<ChatDetails[]>(fileContents).ToList();
            }
            catch
            {
                ChatDetails = new List<ChatDetails>();
            }
        }
        /// <summary>
        /// Add a new conversation to the file if it doesn't already exist.
        /// </summary>
        /// <param name="details">The details of the conversation.</param>
        public void TryCreateNewConversation(ChatDetails details)
        {
            bool matchFound = false;

            foreach (ChatDetails detail in ChatDetails)
            {
                matchFound = detail.RecipientName == details.RecipientName;
            }

            if (!matchFound)
                ChatDetails.Add(details);

            WriteChatDetails();
        }

        /// <summary>
        /// Remove a conversation if it exists.
        /// </summary>
        /// <param name="details">The details of the conversation.</param>
        public void TryDeleteConversation(ChatDetails details)
        {
            Predicate<ChatDetails> matcher = chatDetails => chatDetails == details;

            if (ChatDetails.Find(matcher) == null)
                ChatDetails.Remove(details);

            WriteChatDetails();
        }

        /// <summary>
        /// Update the messages in an existing conversation.
        /// </summary>
        /// <param name="details">The target conversation.</param>
        /// <param name="messages">The new messages.</param>
        public void UpdateMessages(ChatDetails details, ChatBubbleContent[] messages)
        {
            Predicate<ChatDetails> matcher = chatDetails => chatDetails == details;

            ChatDetails.Find(matcher).Messages = messages;
        }
    }
}