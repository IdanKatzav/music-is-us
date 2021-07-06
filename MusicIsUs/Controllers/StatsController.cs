using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicIsUs.Models;
using Newtonsoft.Json;

namespace MusicIsUs.Controllers
{
    public class StatsController : Controller
    {
        private MusicIsUsContext db = new MusicIsUsContext();
        // GET: Stats
        public ActionResult Index()
        {
            var groupMusicByGenre = db.Vinyls.Select(g => new { genre = g.Genere })
                .GroupBy(r => r.genre)
                .Select(i => new { genre = i.Key, count = i.Count() })
                .ToList();
            var groupMusicByGenreJson = JsonConvert.SerializeObject(groupMusicByGenre);
            ViewBag.musicGenreJson = groupMusicByGenreJson;

            var mostPopularArtist = db.Vinyls.Where(r => r.LikedUsers.Count() != 0)
                .Select(i => new { name = i.ArtistName, count = i.LikedUsers.Count() })
                .GroupBy(r => r.name)
                .Select(i => new { name = i.Key.ToString(), count = i.Sum(s => s.count)})
                .ToList();
            var mostPopularArtistJson = JsonConvert.SerializeObject(mostPopularArtist);
            ViewBag.popularArtistsJson = mostPopularArtistJson;
            return View();
        }
    }
}