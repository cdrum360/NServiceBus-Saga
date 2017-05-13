using NServiceBus;

namespace UserManagement.Commands
{
    public class SendVerificationEmail : ICommand
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
        public bool IsReminder { get; set; }
    }
}
