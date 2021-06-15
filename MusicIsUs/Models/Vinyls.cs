using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIsUs.Models
{
    public class Vinyls
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Vinyl name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter artist name")]
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        [Required(ErrorMessage = "Please enter valied publish date")]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Please enter Genere")]
        [Display(Name = "Genere")]
        public string Genere { get; set; }

        [Required(ErrorMessage = "Please enter origin country")]
        [Display(Name = "Origin Country")]
        public string OriginCountry { get; set; }

        public virtual ICollection<Users> LikedUsers { get; set; }
    }
}