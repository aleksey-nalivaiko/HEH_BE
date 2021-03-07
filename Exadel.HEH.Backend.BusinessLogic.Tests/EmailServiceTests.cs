using System.Net.Mail;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Options;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class EmailServiceTests
    {
        private readonly EmailService _emailService;
        private readonly Mock<ISmtpClientWrapper> _smtpClient;

        public EmailServiceTests()
        {
            var emailOptions = new EmailOptions
            {
                Email = "email@mail.com",
                Name = "name",
                Password = "password"
            };
            var options = new OptionsWrapper<EmailOptions>(emailOptions);
            _smtpClient = new Mock<ISmtpClientWrapper>();

            _emailService = new EmailService(options, _smtpClient.Object);
        }

        [Fact]
        public async Task CanSendEmailAsync()
        {
            await _emailService.SendEmailAsync("abc@mail.com", "subject", "hi");
            _smtpClient.Verify(s => s.SendMailAsync(
                It.IsAny<MailMessage>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}