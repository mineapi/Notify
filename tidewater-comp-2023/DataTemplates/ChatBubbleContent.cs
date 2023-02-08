using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tidewater_comp_2023.DataTemplates
{
    public class ChatBubbleContent
    {
        public string ChatBubbleText { get; set; }
        public double ChatBubbleTimestamp { get; set; }
        public string ChatBubbleImage { get; set; }
        public bool RecipientMessage { get; set; }
    }
}
