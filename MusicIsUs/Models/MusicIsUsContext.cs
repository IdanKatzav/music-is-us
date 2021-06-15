using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MusicIsUs.Models
{
    public class MusicIsUsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MusicIsUsContext() : base("name=MusicIsUsContext")
        {
            Database.SetInitializer<MusicIsUsContext>(null);
        }

        public System.Data.Entity.DbSet<MusicIsUs.Models.Branches> Branches { get; set; }

        public System.Data.Entity.DbSet<MusicIsUs.Models.Users> Users { get; set; }

        public System.Data.Entity.DbSet<MusicIsUs.Models.Vinyls> Vinyls { get; set; }

        public System.Data.Entity.DbSet<MusicIsUs.Models.Instruments> Instruments { get; set; }
    }
}
