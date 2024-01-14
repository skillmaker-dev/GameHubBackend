using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Email
{
    public interface IEmailService
    {
        Task Send(EmailMessageModel emailMessage);
    }
}
