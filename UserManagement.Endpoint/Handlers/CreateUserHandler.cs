using System;
using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Commands;
using UserManagement.Events;

namespace UserManagement.Endpoint.Handlers
{
    public class CreateUserHandler : IHandleMessages<CreateUserWithVerifiedEmail>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateUserHandler));

        public IBus Bus { get; set; }

        public void Handle(CreateUserWithVerifiedEmail message)
        {
            log.InfoFormat("Creating user '{0}' with email '{1}'",
                message.Name,
                message.EmailAddress);

            
            Bus.Publish<IUserCreated>(evt =>
            {
                evt.UserId = Guid.NewGuid();
                evt.Name = message.Name;
                evt.EmailAddress = message.EmailAddress;
            });


        }
    }
}
