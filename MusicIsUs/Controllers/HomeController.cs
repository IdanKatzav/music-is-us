using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicIsUs.Models;

namespace MusicIsUs.Controllers
{
    public class HomeController : Controller
    {
        private MusicIsUsContext dbConn = new MusicIsUsContext();
        public ActionResult Index()
        {
            top5();
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
            ViewBag.listOfBranchCities = dbConn.Branches.Select(b => b.City).Distinct();
            ViewBag.listOfBranchStreets = dbConn.Branches.Select(b => b.Street).Distinct();
          
            return View(dbConn.Branches.ToList()) ;
        }
        public ActionResult Search(string city = null,string street = null)
        {
            ViewBag.listOfBranchCities = dbConn.Branches.Select(b => b.City).Distinct();
            ViewBag.listOfBranchStreets = dbConn.Branches.Select(b => b.Street).Distinct();
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

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult top5()
        {
            var topVinyls = dbConn.Vinyls.OrderByDescending(str => str.LikedUsers.Count()).Take(5).ToList();
            var topInstruments = dbConn.Instruments.OrderByDescending(inst => inst.LikedUsers.Count()).Take(5).ToList();
            ViewBag.topVinyls = topVinyls;
            ViewBag.topInstruments = topInstruments;

            return View();
        }

        public ActionResult findUserRecomendations()
        {
            var currentUser = ((Users)System.Web.HttpContext.Current.Session["user"]);

            if (currentUser != null)
            {
                int numberOfRecords = 5;
                
                // Vinyls

                // Get all the vinyls that current user likes
                IEnumerable<int> likedVinyls = dbConn.Users.Where(u => u.Id == currentUser.Id).Select(p => p.LikedVinyls.Select(vin => vin.Id)).SingleOrDefault().ToList();

                // Get all users that like at least one vinyl in common with current user
                IEnumerable<int> usersVinyls = dbConn.Users.Where(u => u.LikedVinyls.Select(vin => vin.Id).Any(vinyl => likedVinyls.Contains(vinyl))
                                         && u.Id != currentUser.Id).Select(uid => uid.Id).ToList();

                // Get all the stores that they like but current user didn't
                var recommVinyls = dbConn.Vinyls.Where(vin => vin.LikedUsers.Any(liked => usersVinyls.Contains(liked.Id))
                                && !vin.LikedUsers.Select(vinid => vinid.Id).Contains((int)currentUser.Id)).Take(numberOfRecords).ToList();

                if (recommVinyls.Count() == 0)
                {
                    recommVinyls = dbConn.Vinyls.OrderByDescending(str => str.LikedUsers.Count()).Take(1).ToList();
                }

                ViewBag.recommVinyls = recommVinyls;

                // Instruments

                // Get all the vinyls that current user likes
                IEnumerable<int> likedInstrumrnts = dbConn.Users.Where(u => u.Id == currentUser.Id).Select(p => p.LikedInstruments.Select(ins => ins.Id)).SingleOrDefault().ToList();

                // Get all users that like at least one vinyl in common with current user
                IEnumerable<int> usersInstruments = dbConn.Users.Where(u => u.LikedVinyls.Select(inst => inst.Id).Any(instrument => likedInstrumrnts.Contains(instrument))
                                         && u.Id != currentUser.Id).Select(uid => uid.Id).ToList();

                // Get all the stores that they like but current user didn't
                var recommInstruments = dbConn.Instruments.Where(inst => inst.LikedUsers.Any(liked => usersInstruments.Contains(liked.Id))
                                && !inst.LikedUsers.Select(instId => instId.Id).Contains((int)currentUser.Id)).Take(numberOfRecords).ToList();

                if (recommInstruments.Count() == 0)
                {
                    recommInstruments = dbConn.Instruments.OrderByDescending(inst => inst.LikedUsers.Count()).Take(1).ToList();
                }

                ViewBag.recommInstruments = recommVinyls; 
            }

            return View();
        }
    }
}