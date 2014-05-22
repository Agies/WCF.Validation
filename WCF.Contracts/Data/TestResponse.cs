using System.Runtime.Serialization;
using WCF.Validation;

namespace WCF.Contracts.Data
{
    [DataContract]
    public class TestResponse : ResponseBase
    {
        public TestResponse()
        {
            
        }

        public TestResponse(TestRequest request) : base(request)
        {

        }
    }
}