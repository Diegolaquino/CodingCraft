using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public abstract class Controller : System.Web.Mvc.Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {

        }
    }
}