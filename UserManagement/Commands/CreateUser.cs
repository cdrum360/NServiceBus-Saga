using NServiceBus;

namespace UserManagement.Commands
{
    public class CreateUser : ICommand
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
    }
}
