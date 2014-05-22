using System.ServiceModel;
using WCF.Contracts.Data;

namespace WCF.Contracts.Service
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        TestResponse TestMe(TestRequest request);
    }
}