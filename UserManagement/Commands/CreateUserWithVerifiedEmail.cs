using NServiceBus;

namespace UserManagement.Commands
{
    public class CreateUserWithVerifiedEmail : ICommand
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
    }
}
