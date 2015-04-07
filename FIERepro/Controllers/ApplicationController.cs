namespace FIERepro.Controllers
{
    using System;
    using System.Security.Principal;
    using System.Web.Mvc;

    public class ApplicationController : Controller
    {
        private readonly IQueryBus queryBus; 

        public ApplicationController(IQueryBus queryBus)
        {
            this.queryBus = queryBus;
        }

        public ActionResult Start()
        {
            if (!queryBus.Query(new ApplicationAllowed(User.Identity)))
            {
                return RedirectToAction("Index", "Home");
            }

            TermsAndConditions TsAndCs = queryBus.Query(new GetTermsAndConditions());

            if (TsAndCs == null)
            {
                throw new Exception();
            }

            return View("Start", TsAndCs);
        }
    }

    public class GetTermsAndConditions : IQuery<TermsAndConditions>
    {
    }

    public class TermsAndConditions
    {
    }

    public class ApplicationAllowed : IQuery<bool>
    {
        public ApplicationAllowed(IIdentity identity)
        {
        }
    }
}