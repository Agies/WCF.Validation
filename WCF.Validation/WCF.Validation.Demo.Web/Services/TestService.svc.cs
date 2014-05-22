using WCF.Contracts.Data;
using WCF.Contracts.Service;

namespace WCF.Validation.Demo.Web.Services
{
    public class TestService : ITestService
    {

        public TestResponse TestMe(TestRequest request)
        {
            var response = new TestResponse(request);
            return response;
        }
    }
}
