using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RollingRides.WebApp.Components.BusinessLogic.Common
{
    public class EmailerFactory
    {
        public static Emailer NewDefaultInstance()
        {
            return new Emailer(ConfigurationManager.AppSettings["SmtpServer"], ConfigurationManager.AppSettings["SmtpUsername"], ConfigurationManager.AppSettings["SmtpPassword"]);
        }
    }
}