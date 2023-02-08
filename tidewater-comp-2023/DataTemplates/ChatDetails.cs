using tidewater_comp_2023.Utils;

namespace tidewater_comp_2023.DataTemplates
{
    public class ChatDetails
    {
        public String RecipientName { get; set; }

        public String RecipientEmail { get; set; }

        public String RecipientProfilePicture { get; set; }

        public String LastMessage => Messages.Length > 0 ? Messages[^1].ChatBubbleText : "";

        public int UnixDate { get; set; }
        public String ChatDate => 
            Messages.Length > 0 ?
                DateTime.UnixEpoch
                    .AddSeconds(Messages[^1].ChatBubbleTimestamp)
                    .ToLocalTime()
                    .TimeTo12HourString(false) :
                "";

        public ChatBubbleContent[] Messages { get; set; }
    }
}
