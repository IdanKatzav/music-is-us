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
            ViewBag.Message = "MusicIsUs is the leading music rating site";
            //ViewBag.listOfBranchCities = dbConn.Branches.Select(b => b.City).Distinct();
            //ViewBag.listOfBranchStreets = dbConn.Branches.Select(b => b.Street);

            return View(dbConn.Branches.ToList()) ;
        }
        public ActionResult Search(string city = null,string street = null)
        {
            ViewBag.listOfBranchCities = dbConn.Branches.Select(b => b.City).Distinct();
            ViewBag.listOfBranchStreets = dbConn.Branches.Select(b => b.Street);
            var returnDataQuery = dbConn.Branches.Select(b => b);

            if (!string.IsNullOrEmpty(city))
            {
                returnDataQuery = returnDataQuery.Where(b => b.City.Equals(city));
            }
            if (!string.IsNullOrEmpty(street))
            {
                returnDataQuery = returnDataQuery.Where(b => b.Street.Equals(street));
            }
            return View("Contact", returnDataQuery.ToList());
        }
    }
}