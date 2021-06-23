# TestEmailAPI
.NET Core REST API for generating test emails.

This is currently a work in progress. My goal it so create a fairly abstract Messaging API for delivering Email content to one of many concrete classes that implement the IMessageSender interface. I'm hoping to create something that will allow me to perform more transparent Email testing within a production environment.

Current Progress:
So far I've mainly been focusing on DTO validation and related unit tests. My plan for next steps is to begin writing tests that surround the first concrete implementation of IMessageSender. 
