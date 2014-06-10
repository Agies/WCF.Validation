using WCF.Contracts.Data;
using WCF.Contracts.Service;

namespace WCF.Validation.Demo.Web.Services
{
    public class TestService : ValidatingServiceBase, ITestService
    {
        //Should be fixed with DI
        public TestService() : this(new OperationContextModelStateProvider())
        {
            
        }

        public TestService(IModelStateProvider modelStateProvider)
            : base(modelStateProvider)
        {

        }

        public TestResponse TestMe(TestRequest request)
        {
            var response = new TestResponse(request);
            ModelState.AddModelError("", "I love validation");
            return response;
        }

        public string TestMe2(TestRequest request)
        {
            return "help";
        }
    }
}
