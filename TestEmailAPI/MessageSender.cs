using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEmailAPI.Models;

namespace TestEmailAPI
{
	public interface IMessageSender
	{
		MessageSendResult Send(Message message);
	}
	public class MessageSender : IMessageSender
	{
		public MessageSendResult Send(Message message)
		{
			return new MessageSendResult();
		}
	}
}
