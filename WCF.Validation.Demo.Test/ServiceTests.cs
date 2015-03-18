using System;
using System.Reflection;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WCF.Contracts.Data;
using WCF.Validation.Demo.Web.Services;

namespace WCF.Validation.Demo.Test
{
    [TestClass]
    public class ServiceTests
    {
        private TestService target;
        private ModelState _state;

        [TestInitialize]
        public void Setup()
        {
            var mockProvider = new Mock<IModelStateProvider>();
            _state = new ModelState {};
            mockProvider.Setup(t => t.Instance).Returns(_state);
            target = new TestService(mockProvider.Object);
        }

        [TestMethod]
        public void Service_should_be_able_to_add_errors()
        {
            var response = target.TestMe(new TestRequest());
            Assert.AreEqual(1, _state.Errors.Count);
        }
    }

    public class OperationContextWrapper
    {
    }
}
