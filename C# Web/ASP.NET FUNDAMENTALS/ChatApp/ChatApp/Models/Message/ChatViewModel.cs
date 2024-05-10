namespace ChatApp.Models.Message
{
    public class ChatViewModel
    {
        public MessageViewModel CurrentMessage { get; set; }

        public List<MessageViewModel> Messages { get; set; }
    }
}
