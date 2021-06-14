using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace MusicIsUs.Controllers
{
    public class HomeController : Controller
    {
        private MusicIsUsContext dbConn = new MusicIsUsContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.listOfBranchCities = dbConn.Branches.Select(b => b.City).Distinct();
            ViewBag.listOfBranchStreets = dbConn.Branches.Select(b => b.Street);

            return View(dbConn.Branches.ToList()) ;
        }
    }
}