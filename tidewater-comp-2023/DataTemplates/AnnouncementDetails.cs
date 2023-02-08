namespace tidewater_comp_2023
{
    public class AnnouncementDetails
    {
        public String AnnouncementTitle { get; set; }
        public String AnnouncementDescription { get; set; }

        public String ShortAnnouncementDescription => AnnouncementDescription.Substring(0, 26) + "...";

        public String AnnouncementDate { get; set; }
        public String AnnouncementAuthor { get; set; }
        public String ProfilePictureSource { get; set; }
    }
}
