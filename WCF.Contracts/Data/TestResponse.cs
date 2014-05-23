using System.Runtime.Serialization;
using WCF.Validation;
using WCF.Validation.Contracts;

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