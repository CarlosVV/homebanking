using System;
using System.ServiceModel.Channels;
using System.Configuration;

namespace DinersClubOnline.Services.Client.Common
{
    public class Security
    {
        static Lazy<HttpRequestMessageProperty> AutenticationRequestMessage
        {
            get
            {
                var requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers["username"] = ConfigurationManager.AppSettings["DinersUserName"];
                requestMessage.Headers["password"] = ConfigurationManager.AppSettings["DinersPassword"];

                return new Lazy<HttpRequestMessageProperty>(() => requestMessage);
            }
        }

        public static HttpRequestMessageProperty AutenticationHeader
        {
            get
            {
                return AutenticationRequestMessage.Value;
            }
        }
    }
}
