namespace FIERepro.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private readonly IQueryBus queryBus;

        public HomeController(IQueryBus queryBus)
        {
            this.queryBus = queryBus;
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException();
            }

            IList<Pumpkin> pumpkins = null;

            if (id != null)
            {
                pumpkins = queryBus.Query(new PumpkinsByOwnerId((int)id));

                if (pumpkins == null)
                {
                    throw new NullReferenceException();
                }
            }

            return View("Index", pumpkins);
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            bool registrationOpen = queryBus.Query(new RegistrationOpen());

            if (registrationOpen)
            {
                return View("Registration");
            }

            throw new UnauthorizedAccessException();
        }
    }
}