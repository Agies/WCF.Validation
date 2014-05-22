using WCF.Contracts.Data;
using WCF.Contracts.Service;

namespace WCF.Validation.Demo.Web.Services
{
    public class TestService : ServiceBase, ITestService
    {

        public TestResponse TestMe(TestRequest request)
        {
            var response = new TestResponse(request);
            ModelState.AddModelError("", "I love validation");
            return response;
        }
    }

    public class ServiceBase
    {
        public ModelState ModelState
        {
            get { return Validation.ModelState.Current; }
        }
    }
}
