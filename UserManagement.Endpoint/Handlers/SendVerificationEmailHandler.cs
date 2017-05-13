using NServiceBus;
using NServiceBus.Logging;
using UserManagement.Commands;

namespace UserManagement.Endpoint.Handlers
{
    public class SendVerificationEmailHandler: IHandleMessages<SendVerificationEmail>
	{
        private static readonly ILog log = LogManager.GetLogger(typeof(SendVerificationEmailHandler));

		public void Handle(SendVerificationEmail message)
		{
			if (message.IsReminder)
			{
				log.InfoFormat("Send reminder email to {0} at address {1} with verification code {2}",
					message.Name, message.EmailAddress, message.VerificationCode);
			}
			else
			{
				log.InfoFormat("Send original verification email to {0} at address {1} with verification code {2}",
					message.Name, message.EmailAddress, message.VerificationCode);
			}
		}
	}

}
