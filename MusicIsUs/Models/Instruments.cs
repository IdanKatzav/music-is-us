using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIsUs.Models
{
    public class Instruments
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Instrument name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Instrument Type")]
        [Display(Name = "Instrument Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please enter made in country")]
        public string MadeInCountry { get; set; }

        [Required(ErrorMessage = "Please enter brand")]
        public string Brand { get; set; }

        public virtual ICollection<Users> LikedUsers { get; set; }
    }
}