using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TestEmailAPI.Models;

namespace TestEmailAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MessageController : ControllerBase
	{
		public MessageController(IMessageSender messageSender)
		{
			_messageSender = messageSender;
		}

		[HttpPost]
		public IActionResult Send(Message message)
		{
			return _messageSender.Send(message).Result;
		}

		private readonly IMessageSender _messageSender;
	}
}
