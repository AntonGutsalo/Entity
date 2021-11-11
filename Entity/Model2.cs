namespace Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<vwInsuredEventsOfClient> vwInsuredEventsOfClients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vwInsuredEventsOfClient>()
                .Property(e => e.Sum)
                .HasPrecision(19, 4);
        }
    }
}
