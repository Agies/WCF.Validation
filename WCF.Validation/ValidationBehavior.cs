using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCF.Validation
{
    public class ValidationBehavior : IEndpointBehavior, IServiceBehavior
    {
        private readonly IRequestValidator _validator;

        public ValidationBehavior(IRequestValidator validator)
        {
            _validator = validator;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            foreach (DispatchOperation dispatchOperation in endpointDispatcher.DispatchRuntime.Operations)
            {
                dispatchOperation.ParameterInspectors.Add(new ParameterValidationInspector(_validator));
            }
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            foreach (ClientOperation clientOperation in clientRuntime.Operations)
            {
                clientOperation.ParameterInspectors.Add(new ParameterValidationInspector(_validator));
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>())
            {
                foreach (var endpointDispatcher in dispatcher.Endpoints)
                {
                    foreach (DispatchOperation dispatchOperation in endpointDispatcher.DispatchRuntime.Operations)
                    {
                        dispatchOperation.ParameterInspectors.Add(new ParameterValidationInspector(_validator));
                    }
                }
            }
        }
    }
}