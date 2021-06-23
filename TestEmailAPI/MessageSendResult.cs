using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestEmailAPI
{
	public class MessageSendResult
	{
		public IActionResult Result { get; internal set; }
		public DateTime DateTimeSent { get; internal set; }
		public IEnumerable<string> Log { get; internal set; }
	}
}
