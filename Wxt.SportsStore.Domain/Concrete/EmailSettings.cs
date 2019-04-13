using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wxt.SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "username@sdf.com";
        public string MailFromAddress = "from@sina.com";
        public bool UseSsl = true;
        public string Username = "username";
        public string Password = "password";
        public string ServerName = "smtp.server.com";
        public int ServerPort = 25;
        public bool WriteAsFile = false;
        public string FileLocation = @"d:\sports_store_emails";
    }
}
