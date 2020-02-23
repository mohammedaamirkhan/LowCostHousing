using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LowCostHousing.Services
{
    public static class EmailService
    {
        public static void Send(string ToAddress, string Subject, string Body)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("aamirkhan19270891@gmail.com", "--YourPassword--"),
                    EnableSsl = true
                };
                client.Send("info@soultech.com.au", ToAddress, Subject, Body);
            }
            catch( Exception ex)
            {
                throw ex;
            }
        }
    }
}
