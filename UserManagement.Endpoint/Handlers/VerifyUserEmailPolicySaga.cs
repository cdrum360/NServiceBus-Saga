using System;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using UserManagement.Commands;

namespace UserManagement.Endpoint.Handlers
{
    public class VerifyUserEmailPolicySaga : Saga<VerifyUserEmailPolicyData>,
		IAmStartedByMessages<CreateUser>,
		IHandleMessages<VerifyUserEmail>,
		IHandleTimeouts<VerifyUserEmailReminderTimeout>,
		IHandleTimeouts<VerifyUserEmailExpiredTimeout>
	{
		private static readonly ILog log = LogManager.GetLogger(typeof (VerifyUserEmailPolicySaga));

		protected override void ConfigureHowToFindSaga(SagaPropertyMapper<VerifyUserEmailPolicyData> mapper)
		{
			mapper.ConfigureMapping<CreateUser>(msg => msg.EmailAddress).ToSaga(saga => saga.EmailAddress);
			mapper.ConfigureMapping<VerifyUserEmail>(msg => msg.EmailAddress).ToSaga(saga => saga.EmailAddress);
		}

		public void Handle(CreateUser message)
		{
			this.Data.Name = message.Name;
			this.Data.EmailAddress = message.EmailAddress;
			this.Data.VerificationCode = Guid.NewGuid().ToString("n").Substring(0, 4);

			Bus.Send(new SendVerificationEmail
			{
				Name = message.Name,
				EmailAddress = message.EmailAddress,
				VerificationCode = Data.VerificationCode,
				IsReminder = false
			});

			this.RequestTimeout<VerifyUserEmailReminderTimeout>(TimeSpan.FromDays(2));
			this.RequestTimeout<VerifyUserEmailExpiredTimeout>(TimeSpan.FromDays(7));
		}

		public void Handle(VerifyUserEmail message)
		{
			if (message.VerificationCode == this.Data.VerificationCode)
			{
				Bus.Send(new CreateUserWithVerifiedEmail
				{
					EmailAddress = this.Data.EmailAddress,
					Name = this.Data.Name
				});

				this.MarkAsComplete();
			}
		}

		public void Timeout(VerifyUserEmailReminderTimeout state)
		{
			Bus.Send(new SendVerificationEmail
			{
				Name = Data.Name,
				EmailAddress = Data.EmailAddress,
				VerificationCode = Data.VerificationCode,
				IsReminder = true
			});

		}

		public void Timeout(VerifyUserEmailExpiredTimeout state)
		{
			this.MarkAsComplete();
		}
	}

	public class VerifyUserEmailPolicyData : ContainSagaData
	{
		public virtual string Name { get; set; }
		[Unique]
		public virtual string EmailAddress { get; set; }
		public virtual string VerificationCode { get; set; }
	}

	public class VerifyUserEmailReminderTimeout
	{
	}

	public class VerifyUserEmailExpiredTimeout
	{
	}
}


