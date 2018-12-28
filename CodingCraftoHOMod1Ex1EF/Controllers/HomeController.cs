using CodingCraftoHOMod1Ex1EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hangfire;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() => View();
    }
}