using NServiceBus;

namespace UserManagement.Commands
{
    public class VerifyUserEmail : ICommand
    {
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
    }
}
