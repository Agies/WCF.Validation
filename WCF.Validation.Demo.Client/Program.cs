using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCF.Contracts.Data;
using WCF.Contracts.Service;
using WCF.Validation.Contracts;

namespace WCF.Validation.Demo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var wcfClient = new TestServiceProxy();
                var request = new TestRequest();
                Console.WriteLine("Make Request {0}", request.MessageId);
                var result = wcfClient.ChannelFactory.CreateChannel().TestMe(request);
                Console.WriteLine("Request {0} contains the following errors", result.RequestId);
                foreach (var validationError in result.Errors)
                {
                    Console.WriteLine("\t Member '{0}' is mad because {1}", validationError.MemberName, validationError.Message);
                }

                var channel = wcfClient.ChannelFactory.CreateChannel();
                using (var scope = new OperationContextScope((IClientChannel)channel))
                {
                    var result2 = channel.TestMe2(request);
                    Console.WriteLine();
                    Console.WriteLine("Request '{0}' contains the following errors in the Header", result2);
                    var index = OperationContext.Current.IncomingMessageHeaders.FindHeader(ParameterValidationInspector.ErrorHeader, "http://WCF.Validation");
                    var header = OperationContext.Current.IncomingMessageHeaders.GetHeader<List<ValidationError>>(index);
                    foreach (var validationError in header)
                    {
                        Console.WriteLine("\t Member '{0}' is mad because {1}", validationError.MemberName,
                            validationError.Message);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Press 'Enter' to exit");
            Console.ReadLine();
        }
    }

    public class TestServiceProxy : ClientBase<ITestService>
    {
        
    }
}
