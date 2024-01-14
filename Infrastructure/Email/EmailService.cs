using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using FluentEmail.Core;
using Common.Models;

namespace Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IFluentEmail _fluentEmail;

        public EmailService(ILogger<EmailService> logger,IFluentEmail fluentEmail)
        {
            _logger = logger;
            _fluentEmail = fluentEmail;
        }

        public async Task Send(EmailMessageModel emailMessageModel)
        {
            _logger.LogInformation("Sending email");
            await _fluentEmail.To(emailMessageModel.ToAddress)
                .Subject(emailMessageModel.Subject)
                .Body(emailMessageModel.Body,true)
                .SendAsync();
        }
    }
}
