using System.ServiceModel;
using System.ServiceModel.Channels;

namespace DinersClubOnline.Services.Client.Common
{
    public class TarjetaServiceClientBase : TarjetaService.TarjetaClient
    {
        private static OperationContextScope Scope { get; set; }

        public TarjetaServiceClientBase()
        {
            Scope = new OperationContextScope(InnerChannel);
            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = Security.AutenticationHeader;
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}