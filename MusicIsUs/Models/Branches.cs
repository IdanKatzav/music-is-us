using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIsUs.Models
{
    public class Branches
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You need to enter a name of a city")]
        [Display (Name = "City")]
        [DataType (DataType.Text)]
        public string City { get; set; }
        [Required(ErrorMessage = "You need to enter a name of a Street")]
        [Display(Name = "Street")]
        [DataType(DataType.Text)]
        public string Street { get; set; }
    }
}