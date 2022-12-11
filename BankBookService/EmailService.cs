
using EasyAgendaBase.Enums;
using EasyAgendaBase.Model;
using EasyAgendaService.Contracts;
using EasyAgendaService.Data.Contracts;
using MailKit.Net.Smtp;
using MimeKit;

namespace EasyAgendaService
{
    public class EmailService: IEmailService
    {
        private readonly IMailConfigurationDAL _mailConfigurationRepository;

        public EmailService(IMailConfigurationDAL settingEmailRepository)
        {
            _mailConfigurationRepository = settingEmailRepository;
        }

        public async Task Send(RecipientMessage recipient, TypeMessage typeMessage)
        {
            var settingMail = await _mailConfigurationRepository.GetMailConfiguration();
            var message = await _mailConfigurationRepository.GetCustomizedMessage(recipient, (int)typeMessage);

            var mineMessage = new MimeMessage();
            
            if (settingMail == null)
                throw new Exception("There is no configuration for e-mail.");

            mineMessage.From.Add(new MailboxAddress(settingMail.CompanyName, settingMail.Email));

            switch (typeMessage)
            {
                case TypeMessage.NEW_APPOINTMENT_CLIENT:
                case TypeMessage.APPOINTMENT_CANCELLED_CLIENT:
                    mineMessage.To.Add(new MailboxAddress(recipient.NameClient, recipient.EmailClient));
                    break;

                case TypeMessage.NEW_APPOINTMENT_PROFESSIONAL:
                case TypeMessage.APPOINTMENT_CANCELLED_PROFESSIONAL:
                    mineMessage.To.Add(new MailboxAddress(recipient.NameProfessional, recipient.EmailProfessional));
                    break;
            }
           
            
            mineMessage.Subject = message.Subject;
            mineMessage.Body = new TextPart("plain")
            {
                Text = message.Description
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(settingMail.ServerSmtp, settingMail.PortSsl, true);
            await client.AuthenticateAsync(settingMail.Email, settingMail.Password);
            await client.SendAsync(mineMessage);
            await client.DisconnectAsync(true);
        }
    }
}