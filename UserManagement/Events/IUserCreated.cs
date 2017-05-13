using System;
using NServiceBus;

namespace UserManagement.Events
{
    public interface IUserCreated : IEvent
    {
        Guid UserId { get; set; }
        string EmailAddress { get; set; }
        string Name { get; set; }
    }
}
