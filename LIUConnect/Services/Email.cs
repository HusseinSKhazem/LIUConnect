using MailKit.Net.Smtp;
using MimeKit;


namespace LIUConnect.Services
{
    public class Email
    {
        public Email() { }
        public void SendEmail(string to, string body)
        {
            try
            {
                using (var smtp = new SmtpClient())
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("Hipstore57@gmail.com"));
                    email.To.Add(MailboxAddress.Parse(to));
                    email.Subject = "Recruiter Approved";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("hipstore57@gmail.com", "uvpnekxxldkujkms");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
