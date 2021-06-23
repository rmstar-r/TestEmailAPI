# TestEmailAPI
.NET Core REST API for generating test emails.

This is currently a work in progress. My goal it so create a fairly abstract Messaging API for delivering Email content to one of many concrete classes that implement the IMessageSender interface. I'm hoping to create something that will allow me to perform more transparent Email testing within our staging/production environments.

Current Progress:
So far I've mainly been focusing on DTO validation and related unit tests. My plan for next steps is to begin writing tests that surround the first concrete implementation of IMessageSender. This will likely rely heavily on the System.Net.Mail namespace (hence the MailMessageAdapter).

Goals:
 - Implement an IMessageSender leverages System.Net.Mail.
 - Implement an IMessageSender that routes message send requests to an existing web service.
 - Implement an IMessageSender that routes message send requests to an existing service via RMQ.
 - Create an accompanying Node/React UI app for modifying message content.
 - Leverage this API to created automated Mojibake tests.

