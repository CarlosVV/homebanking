using System.ServiceModel;
using System.ServiceModel.Channels;

namespace DinersClubOnline.Services.Client.Common
{
    public class SeguridadServiceClientBase : SeguridadService.SeguridadClient
    {
        private static OperationContextScope Scope { get; set; }

        public SeguridadServiceClientBase()
        {
            var scope = new OperationContextScope(InnerChannel);
            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Security.AutenticationHeader;
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}
