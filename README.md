# Amalia

## Requisites

You need a [Cohere AI account](https://cohere.ai/) and a [Twilio Sendgrid account](https://sendgrid.com/).
Also, a SQL Database.

Update the API keys and database connection string in the appsettings.json file.

## Using the platform.

1. Publish the web app in a hosting (Azure, AWS, etc.)
2. Configure Twilio Sendgrid for incoming messages for your own domain. Use [this article](https://www.twilio.com/blog/organize-email-attachments-with-csharp-aspnetcore-twilio-sendgrid-inbound-parse) for example.
2.1 In the webhook configuration, your webservice is: https://*base url*/IncomingEmail
3. Check this demo about how Amalia works: https://www.youtube.com/watch?v=FTIDIeFrf5M

## Demo

https://amalia.azurewebsites.net/
