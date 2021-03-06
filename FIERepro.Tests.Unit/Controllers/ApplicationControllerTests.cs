﻿namespace FIERepro.Tests.Unit.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using FakeItEasy;
    using FIERepro.Controllers;
    using Xunit;

    public class ApplicationControllerTests
    {
        private ContextFactory contextFactory = null;

        private IQueryBus GetQueryBus()
        {
            return A.Fake<IQueryBus>();
        }


        [Fact]
        public void Start_NullIdentity_RedirectsToIndex()
        {
            var queryBus = GetQueryBus();
            A.CallTo(() => queryBus.Query(A<ApplicationAllowed>.Ignored)).Returns(false);

            var controller = new ApplicationController(queryBus);
            
            controller.ControllerContext = GetContextWithNullUser();

            var result = controller.Start() as RedirectToRouteResult;

            Assert.Equal("Index", result.RouteValues["action"]);
        }

        private ControllerContext GetContextWithNullUser()
        {
             var context = A.Fake<HttpContextBase>();
            A.CallTo(() => context.User.Identity).Returns(null);
            var controllerContext = A.Fake<ControllerContext>();
            A.CallTo(() => controllerContext.HttpContext).Returns(context);

            return controllerContext;
        }
    }
}
