using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Events;

namespace UserManagement.Endpoint.Handlers
{
    public class UserCreatedHandler : IHandleMessages<IUserCreated>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (UserCreatedHandler));

        public void Handle(IUserCreated message)
        {
            log.InfoFormat("Sending welcome email to {0}", message.EmailAddress);
        }
    }
}
