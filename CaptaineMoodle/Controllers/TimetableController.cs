using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Controllers
{
    public class TimetableController : Controller
    {
        // GET: TimetableController
        public ActionResult Index()
        {
            return View();
        }

    }
}
