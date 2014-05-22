using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Contracts.Data;
using WCF.Contracts.Service;

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
                Console.WriteLine("Request {0} contains the following errors\n", result.RequestId);
                foreach (var validationError in result.Errors)
                {
                    Console.WriteLine("\t Member {0} is mad because {1}", validationError.MemberName, validationError.Message);
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
