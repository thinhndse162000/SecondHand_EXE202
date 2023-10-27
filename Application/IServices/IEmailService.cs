using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IEmailService
    {
        Task SendForgotPassword(string recipientEmail);
    }
}
