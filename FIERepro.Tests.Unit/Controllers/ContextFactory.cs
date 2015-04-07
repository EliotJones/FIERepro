namespace FIERepro.Tests.Unit.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using FakeItEasy;

    public class ContextFactory
    {
        internal ControllerContext GetFakedContext(bool userAuthenticated)
        {
            var context = A.Fake<HttpContextBase>();

            A.CallTo(() => context.User.Identity.IsAuthenticated).Returns(userAuthenticated);

            var controllerContext = A.Fake<ControllerContext>();
            A.CallTo(() => controllerContext.HttpContext).Returns(context);

            return controllerContext;
        }
    }
}