using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using MimeKit;
using MimeKit.Text;
using Onboarding_Backend.Onboarding_Entities;
using Newtonsoft.Json;
namespace Onboarding_Backend.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private string _secret;
        private string _senderEmail;
        private string _senderEmailPassword;

        public EmailNotificationService()
        {
            this._secret = "Th!$!s$ecret";
            this._senderEmail = "ba3415def3c32dbd7782f2e64cd05e59";
            this._senderEmailPassword = "d7c24763947b3e4b11af5eecd8864f3f";
        }


        public string GenerateToken(Entities entities)
        {

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var Token = encoder.Encode(entities, this._secret);
            return Token;
        }

        public void SendEmail(Entities entities)
        {
            var token = GenerateToken(entities);

            // Instantiate mimemessage()
            var message = new MimeMessage();

            // from.. sender's email address. 
            message.From.Add(new MailboxAddress("Team Proflo", "teamproflo2019@gmail.com"));

            // To..  Reciever's Email address.
            message.To.Add(new MailboxAddress(entities.Email));

            // subject of the mail.
            message.Subject = "This is the verification mail.";

            // body of email.
            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = $@"Click on the link to confirm 
                your email account http://onboarding.proflo.cgi-wave7.stackroute.io/signup?token={token}"
            };

            // Configure and send verification email.
            using (var Client = new MailKit.Net.Smtp.SmtpClient())
            {
                // connecting to server.
                Client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                Client.Connect("in-v3.mailjet.com", 587, false);
                

                // getting sender's authentication.
                Client.Authenticate(this._senderEmail, this._senderEmailPassword);

                // send mail.
                Client.Send(message);

                // disconnect connection  after sending an email.
                Client.Disconnect(true);
            }
        }

        public Entities VerifyAndDecodeEntities(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var json = decoder.Decode(token, this._secret, verify: true);
            Entities entities= JsonConvert.DeserializeObject<Entities>(json);
            return entities;
        }
    }
}
