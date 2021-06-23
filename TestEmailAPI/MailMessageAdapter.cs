using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TestEmailAPI.Models;

namespace TestEmailAPI
{
	public interface IMailMessageAdapter
	{
		MailMessage GetMailMessage(Message message);
	}
	public class MailMessageAdapter : IMailMessageAdapter
	{
		public MailMessage GetMailMessage(Message message)
		{
			var mailMessage = new MailMessage(message.Sender, String.Join(",",message.To))
			{
				Body = message.Body.Text,
				BodyEncoding = Encoding.GetEncoding(message.Body.ContentEncoding),
				DeliveryNotificationOptions = DeliveryNotificationOptions.None,
				HeadersEncoding = Encoding.GetEncoding(message.HeaderEncoding),
				IsBodyHtml = message.Body.IsHtml,
				Priority = (MailPriority)message.Priority,
				Subject = message.Subject,
				SubjectEncoding = Encoding.GetEncoding(message.SubjectEncoding)
			};

			foreach (var ccEmail in message.CC)
			{
				mailMessage.CC.Add(ccEmail);
			}

			foreach (var bccEmail in message.BCC)
			{
				mailMessage.Bcc.Add(bccEmail);	
			}

			foreach (var header in message.Headers)
			{
				mailMessage.Headers.Add(header.Name, header.Value);
			}

			foreach (var alternativeBodyPart in message.AlternativeBodyParts)
			{
				mailMessage.AlternateViews.Add(
					AlternateView.CreateAlternateViewFromString(alternativeBodyPart.Text, Encoding.GetEncoding(alternativeBodyPart.ContentEncoding), alternativeBodyPart.IsHtml ? "text/html" : "text/plain"));
			}
			
			return mailMessage;
		}
	}
}
