namespace _1._1CompanyImportTool.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Stocks : DbContext
    {
        public Stocks()
            : base("name=Stocks")
        {
            this.Database.CreateIfNotExists();
        }


        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
    }

}