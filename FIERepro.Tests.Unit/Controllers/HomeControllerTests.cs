namespace FIERepro.Tests.Unit.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using FakeItEasy;
    using FIERepro.Controllers;
    using Xunit;

    public class HomeControllerTests
    {
        private readonly ContextFactory contextFactory = new ContextFactory();

        private IQueryBus GetQueryBus()
        {
            return A.Fake<IQueryBus>();
        }

        [Fact]
        public void Index_NotAuthenticated_Throws()
        {
            var queryBus = GetQueryBus();

            A.CallTo(() => queryBus.Query(A<PumpkinsByOwnerId>.Ignored)).Returns(null);

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(false);

            Assert.Throws<UnauthorizedAccessException>(() => controller.Index(null));
        }

        [Fact]
        public void Index_Authenticated_ReturnsIndexView()
        {
            var queryBus = GetQueryBus();

            A.CallTo(() => queryBus.Query(A<PumpkinsByOwnerId>.Ignored)).Returns(null);

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(true);

            var result = controller.Index(null) as ViewResult;

            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void Index_AuthenticatedWithIncorrectId_Throws()
        {
            var queryBus = GetQueryBus();

            A.CallTo(() => queryBus.Query(A<PumpkinsByOwnerId>.Ignored)).Returns(null);

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(true);

            Assert.Throws<NullReferenceException>(() => controller.Index(5));
        }

        [Fact]
        public void Index_AuthenticatedWithCorrectId_ReturnsIndexView()
        {
            var queryBus = GetQueryBus();

            A.CallTo(() => queryBus.Query(A<PumpkinsByOwnerId>.Ignored)).Returns(new List<Pumpkin>
            {
                new Pumpkin(1, (decimal)0.5, "orange"),
                new Pumpkin(2, (decimal)0.7, "reddish"),
                new Pumpkin(3, (decimal)5.6, "auburn")
            });

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(true);

            var result = controller.Index(5) as ViewResult;

            Assert.IsType<List<Pumpkin>>(result.Model);
            Assert.Equal(3, ((List<Pumpkin>)result.Model).Count);
        }

        [Fact]
        public void Index_AuthenticatedWithCorrectId_ReturnsIndexViewBlankList()
        {
            var queryBus = GetQueryBus();

            A.CallTo(() => queryBus.Query(A<PumpkinsByOwnerId>.Ignored)).Returns(new List<Pumpkin>());

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(true);

            var result = controller.Index(5) as ViewResult;

            Assert.IsType<List<Pumpkin>>(result.Model);
            Assert.Equal(0, ((List<Pumpkin>)result.Model).Count);
        }

        [Fact]
        public void Registration_Authenticated_Redirects()
        {
            var queryBus = GetQueryBus();

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(true);

            var result = controller.Register() as RedirectToRouteResult;

            Assert.Equal("Index", result.RouteValues["action"]);
        }

        [Fact]
        public void Registration_NotAuthenticatedNoRegistrationsNotAllowed_Throws()
        {
            var queryBus = GetQueryBus();

            A.CallTo(() => queryBus.Query(A<RegistrationOpen>.Ignored)).Returns(false);

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(false);

            Assert.Throws<UnauthorizedAccessException>(() => controller.Register());
        }

        [Fact]
        public void Registration_NotAuthenticatedRegistrationsAllowed_ReturnsRegistrationView()
        {
            var queryBus = GetQueryBus();

            A.CallTo(() => queryBus.Query(A<RegistrationOpen>.Ignored)).Returns(true);

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(false);

            var result = controller.Register() as ViewResult;

            Assert.Equal("Registration", result.ViewName);
        }

        [Fact]
        public void Registration_NotAuthenticatedRegistrationsAllowed_ReturnsNoModel()
        {
            var queryBus = GetQueryBus();

            A.CallTo(() => queryBus.Query(A<RegistrationOpen>.Ignored)).Returns(true);

            var controller = new HomeController(queryBus);

            controller.ControllerContext = contextFactory.GetFakedContext(false);

            var result = controller.Register() as ViewResult;

            Assert.Null(result.Model);
        }
    }
}
